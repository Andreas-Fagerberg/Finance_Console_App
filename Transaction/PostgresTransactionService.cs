using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresTransactionService : ITransactionService
{
    private IUserService? userService;
    private NpgsqlConnection connection;

    public PostgresTransactionService(NpgsqlConnection connection, IUserService userService)
    {
        this.connection = connection;
        this.userService = userService;
    }

    public Transaction Load()
    {
        throw new NotImplementedException();
    }

    public Transaction Save()
    {
        // Since the insertion is for all columns there is no need to type out 
        // all columns but for clarity they will be written out. 
        var sql = @"INSERT INTO transactions (transaction_id, user_id, description)"
    }
}
