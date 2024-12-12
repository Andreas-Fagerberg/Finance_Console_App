namespace FinanceApp_Databaser;

using Npgsql;
using Npgsql.Replication;

public class ServiceFactory
{
    public static IUserService CreateUserService(NpgsqlConnection connection)
    {
        return new PostgresUserService(connection);
    }

    public static ITransactionService CreateTransactionService(
        NpgsqlConnection connection,
        DependencyContainer dependencyContainer
    )
    {
        return new PostgresTransactionService(connection, dependencyContainer);
    }

    public static IMenuService CreateMenuService()
    {
        return new AppMenuService();
    }
}
