namespace FinanceApp_Databaser;

using Npgsql;

public class ServiceFactory
{
    public static IUserService CreateUserService(NpgsqlConnection connection)
    {
        return new PostgresUserService(connection);
    }

    public static ITransactionService CreateTransactionService(
        NpgsqlConnection connection,
        IUserService userService
    )
    {
        return new PostgresTransactionService(connection, userService);
    }

    public static IMenuService CreateMenuService()
    {
        return new AppMenuService();
    }
}
