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

    public async Task<List<Transaction>?> Load(string date, string sql)
    {
        List<Transaction>? transactions = new List<Transaction>();
        User? user = await userService.GetLoggedInUser();
        if (user == null)
        {
            return null;
        }

        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@user_id", user.UserId);
        if (int.TryParse(date, out var filterValue))
        {
            cmd.Parameters.AddWithValue("@date", filterValue); // Assuming the dateFilter is an integer
        }
        else
        {
            // Handle invalid dateFilter or throw an exception
            throw new ArgumentException("Invalid dateFilter value");
        }
        // Handle invalid dateFilter or throw an exception


        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
        {
            return null;
        }

        int refId = 1;
        while (reader.Read())
        {
            Transaction transaction = new Transaction
            {
                TransactionId = reader.GetGuid(0),
                UserId = reader.GetGuid(1),
                Description = reader.GetString(2),
                Amount = reader.GetDecimal(3),
                Date = reader.GetDateTime(4),
                RefId = refId,
            };
            refId++;
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

        if (!reader.Read())
        {
            return null;
        }
        decimal balance = reader.GetDecimal(0);
        return balance;
    }
}
