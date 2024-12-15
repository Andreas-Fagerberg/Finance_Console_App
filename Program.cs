using Npgsql;

namespace FinanceApp_Databaser;

class Program
{
    static async Task Main(string[] args)
    {
        DatabaseService<NpgsqlConnection> postgresDatabaseService = new PostgresDatabaseService();

        NpgsqlConnection connection = await postgresDatabaseService.SetupDatabase();

        DependencyContainer container = new DependencyContainer(connection);

        IMenuService menuService = container.MenuService;

        Menu startMenu = new LoginMenu(
            container.UserService,
            container.MenuService,
            container.TransactionService
        );
        menuService.SetMenu(startMenu);

        while (true)
        {
            Utilities.WaitForKeyAny();
            ConsoleKey inputCommand = Console.ReadKey().Key;

            menuService.GetMenu().ExecuteCommand(inputCommand);
        }
    }
}
