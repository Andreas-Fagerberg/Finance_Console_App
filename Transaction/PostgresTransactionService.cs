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
        throw new NotImplementedException();
    }
}
