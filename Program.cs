using Npgsql;

namespace FinanceApp_Databaser;

class Program
{
    static async Task Main(string[] args)
    {
        await using var connection = await new PostgresDatabaseService().SetupDatabase();

        DependencyContainer container = new DependencyContainer(connection);

        var startup = new ApplicationStartup(container);
        startup.Initialize();

        IMenuService menuService = container.MenuService;

        while (true)
        {
            Utilities.WaitForKeyAny();
            ConsoleKey inputCommand = Console.ReadKey().Key;

            menuService.GetMenu().ExecuteCommand(inputCommand);
        }
    }
}
