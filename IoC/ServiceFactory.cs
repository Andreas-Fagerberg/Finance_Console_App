namespace FinanceApp_Databaser;

using Npgsql;

public class ServiceFactory
{
    public static IUserService CreateUserService(NpgsqlConnection connection)
    {
        return new PostgresUserService(connection);
    }

    public static ITransactionService CreateTransactionService(NpgsqlConnection connection)
    {
        return new PostgresTransactionService(connection);
    }

    public static IMenuService CreateMenuService()
    {
        return new AppMenuService();
    }
}
