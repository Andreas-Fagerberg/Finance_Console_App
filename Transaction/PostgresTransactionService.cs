using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresTransactionService : ITransactionService
{
    private IUserService userService;
    private NpgsqlConnection connection;
    public PostgresTransactionService(IUserService userService, NpgsqlConnection connection)
    {
        this.userService = userService;
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
