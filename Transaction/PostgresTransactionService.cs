using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresTransactionService : ITransactionService
{
    private IUserService userService;
    private NpgsqlConnection connection;

    public PostgresTransactionService(NpgsqlConnection connection, IUserService userService)
    {
        this.connection = connection;
        this.userService = userService;
    }

    public async Task<List<Transaction>?> Load(DateType dateType, List<string> inputDate)
    {
        User? user = await userService.GetLoggedInUser();
        if (user == null)
        {
            return null;
        }

        List<Transaction>? transactions = new List<Transaction>();
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
        return transactions;
    }

    public async Task Save(Transaction transaction)
    {
        /* Since the insertion is for all columns there is no need to type out
        all columns but for clarity they will be written out. */
        var sql =
            @"INSERT INTO transactions (transaction_id, user_id, amount, description, transfer_date)
        VALUES
        (
            @transaction_id,
            @user_id,
            @amount,
            @description,
            @transfer_date
        )
        ";

        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@transaction_id", transaction.TransactionId);
        cmd.Parameters.AddWithValue("@user_id", transaction.UserId);
        cmd.Parameters.AddWithValue("@description", transaction.Description!);
        cmd.Parameters.AddWithValue("@amount", transaction.Amount);
        cmd.Parameters.AddWithValue("@transfer_date", DateTime.Now);

        await cmd.ExecuteNonQueryAsync();
        return;
    }

    public async Task<decimal?> GetBalance(User user)
    {
        var sql =
            @"SELECT SUM(amount) FROM transactions 
          INNER JOIN users ON transactions.user_id = users.user_id
          WHERE users.user_id = @user_id";

        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@user_id", user.UserId);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read() || reader.IsDBNull(0))
        {
            return null;
        }

        decimal balance = reader.GetDecimal(0);
        return balance;
    }
}
