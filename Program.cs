using Npgsql;

namespace FinanceApp_Databaser;

class Program
{
    static async Task Main(string[] args)
    {
        DatabaseService<NpgsqlConnection> postgresDatabaseService = new PostgresDatabaseService();

        NpgsqlConnection connection = await postgresDatabaseService.SetupDatabase();

        DependencyContainer dependencyContainer = new DependencyContainer(
            userServiceFactory: () => new PostgresUserService(connection),
            transactionServiceFactory: () => new PostgresTransactionService(connection),
            menuServiceFactory: () => new AppMenuService()
        );
        IMenuService menuService = dependencyContainer.MenuService;

        // TODO: Implement TransactionService and PostgresTransactionService

        // TODO: Implement MenuService and Create Menus.
        // IMenuService menuService =
        // new SimpleMenuService();
        Menu startMenu = new LoginMenu(dependencyContainer);
        menuService.SetMenu(startMenu);

        while (true)
        {
            WaitForKey.Any();
            ConsoleKey inputCommand = Console.ReadKey().Key;

            menuService.GetMenu().ExecuteCommand(inputCommand);
        }
    }
}
