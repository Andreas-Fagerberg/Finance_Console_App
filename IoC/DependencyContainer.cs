namespace FinanceApp_Databaser;

using Npgsql;

public class DependencyContainer
{
    // Expose services as properties
    private readonly NpgsqlConnection? _connection;
    public IUserService UserService { get; }
    public ITransactionService TransactionService { get; }
    public IMenuService MenuService { get; }

    public DependencyContainer(NpgsqlConnection connection)
    {
        // Now services are created internally
        _connection = connection;
        UserService = ServiceFactory.CreateUserService(_connection);
        TransactionService = ServiceFactory.CreateTransactionService(_connection);
        MenuService = ServiceFactory.CreateMenuService();
    }
}
