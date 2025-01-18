namespace FinanceApp_Databaser;

using Npgsql;

public class DependencyContainer
{
    // Expose services as properties
    private readonly NpgsqlConnection? _connection;
    public IUserService UserService { get; init; }
    public ITransactionService TransactionService { get; init; }
    public IMenuService MenuService { get; init; }

    public DependencyContainer(NpgsqlConnection connection)
    {
        // Now services are created internally
        _connection = connection;
        UserService = ServiceFactory.CreateUserService(_connection);
        TransactionService = ServiceFactory.CreateTransactionService(_connection, UserService);
        MenuService = ServiceFactory.CreateMenuService();
    }
}
