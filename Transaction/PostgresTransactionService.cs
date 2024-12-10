using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresTransactionService : ITransactionService
{
    private IUserService? userService;
    private NpgsqlConnection connection;

    public PostgresTransactionService(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    public Transaction Load()
    {
        throw new NotImplementedException();
    }

    public Transaction Save()
    {
        throw new NotImplementedException();
    }
}
