using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresTransactionService : ITransactionService
{
    private IUserService userService;
    private NpgsqlConnection connection;
    private List<Transaction>? transactions = new List<Transaction>();

    public PostgresTransactionService(NpgsqlConnection connection, IUserService userService)
    {
        this.connection = connection;
        this.userService = userService;
    }

    public async Task<List<Transaction>?> Load(DateType dateType, List<string> inputDate)
    {
        transactions = new List<Transaction>();
        User? user = await userService.GetLoggedInUser();

        if (user == null)
        {
            return null;
        }

        string sql = string.Empty;

        switch (dateType)
        {
            case DateType.YEAR:
                // Requires parameters: @user_id and @year
                sql = SqlQueries.GetTransactionsByUserIdAndYear;
                break;
            case DateType.MONTH:
                // Requires parameters: @user_id, @year and @month
                sql = SqlQueries.GetTransactionsByUserIdAndMonth;
                break;
            case DateType.WEEK:
                // Requires parameters: @user_id, @year and @week
                sql = SqlQueries.GetTransactionsByUserIdAndWeek;
                break;
            case DateType.DATE:
                // Requires parameters: @user_id and @date
                sql = SqlQueries.GetTransactionsByUserIdAndDate;
                break;
            case DateType.NONE:
                // Requires parameters: @user_id
                sql = SqlQueries.GetTransactionsByUserId;
                break;
        }

        using var cmd = new NpgsqlCommand(sql, connection);

        // inputDate index: '0' = year/date   | '1' = month/week
        switch (dateType)
        {
            case DateType.YEAR:
                // Requires parameters: @user_id and @year
                cmd.Parameters.AddWithValue("@user_id", user.UserId);
                cmd.Parameters.AddWithValue("@year", int.Parse(inputDate[0]));
                break;
            case DateType.MONTH:
                // Requires parameters: @user_id, @year and @month
                cmd.Parameters.AddWithValue("@user_id", user.UserId);
                cmd.Parameters.AddWithValue("@year", int.Parse(inputDate[0]));
                cmd.Parameters.AddWithValue("@month", int.Parse(inputDate[1]));
                break;
            case DateType.WEEK:
                // Requires parameters: @user_id, @year and @week
                cmd.Parameters.AddWithValue("@user_id", user.UserId);
                cmd.Parameters.AddWithValue("@year", int.Parse(inputDate[0]));
                cmd.Parameters.AddWithValue("@week", int.Parse(inputDate[1]));
                break;
            case DateType.DATE:
                // Requires parameters: @user_id and @date
                cmd.Parameters.AddWithValue("@user_id", user.UserId);
                cmd.Parameters.AddWithValue(
                    "@date",
                    DateTime.ParseExact(inputDate[0], "yyyy-MM-dd", null)
                );
                break;
            case DateType.NONE:
                // Requires parameters: @user_id
                cmd.Parameters.AddWithValue("@user_id", user.UserId);
                break;
        }

        try
        {
            using var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                Transaction transaction = new Transaction
                {
                    TransactionId = reader.GetGuid(0),
                    UserId = reader.GetGuid(1),
                    Description = reader.GetString(2),
                    Amount = reader.GetDecimal(3),
                    Date = reader.GetDateTime(4),
                    RefId = transactions.Count + 1,
                };
                transactions.Add(transaction);
            }
            List<Transaction>? tempTransactions = transactions.ToList();
            return transactions;
        }
        catch
        {
            Utilities.WaitForKeyAny(
                "An error occurred while loading the transactions. Please try again later."
            );
        }
        return null;
    }

    public async Task Save(Transaction transaction)
    {
        using var dbTransaction = await connection.BeginTransactionAsync();
        try
        {
            string sql = SqlQueries.InsertTransaction;

            using var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@transaction_id", transaction.TransactionId);
            cmd.Parameters.AddWithValue("@user_id", transaction.UserId);
            cmd.Parameters.AddWithValue("@description", transaction.Description!);
            cmd.Parameters.AddWithValue("@amount", transaction.Amount);
            cmd.Parameters.AddWithValue("@transfer_date", DateTime.UtcNow);

            await cmd.ExecuteNonQueryAsync();
            dbTransaction.Commit();
        }
        catch (PostgresException ex)
        {
            await dbTransaction.RollbackAsync();
            Console.Clear();
            Console.WriteLine("Failed to Save transaction due to database error");
            Thread.Sleep(1000);
            throw new Exception("Failed to save transaction", ex);
        }
    }

    public async Task Delete(Transaction transaction)
    {
        using var dbTransaction = await connection.BeginTransactionAsync();
        try
        {
            string sql = SqlQueries.DeleteTransactionByUuid;

            using var cmd = new NpgsqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@user_id", transaction.UserId);
            cmd.Parameters.AddWithValue("@transaction_id", transaction.TransactionId);

            await cmd.ExecuteNonQueryAsync();
            await dbTransaction.CommitAsync();
        }
        catch (PostgresException ex)
        {
            await dbTransaction.RollbackAsync();
            Console.Clear();
            Console.WriteLine("Failed to Delete transaction due to database error");
            Thread.Sleep(1000);
            throw new Exception("Failed to delete transaction", ex);
        }
    }

    public async Task<decimal?> GetBalance(User user)
    {
        string sql = SqlQueries.GetBalanceByUserId;

        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@user_id", user.UserId);

        try
        {
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.Read() || reader.IsDBNull(0))
            {
                return null;
            }

            decimal balance = reader.GetDecimal(0);
            return balance;
        }
        catch
        {
            Utilities.WaitForKeyAny(
                "An error occurred while gettin the current balance. Please try again later."
            );
        }
        return null;
    }

    public Task<List<Transaction>?> GetCurrentTransactions()
    {
        return Task.FromResult(transactions);
    }
}
