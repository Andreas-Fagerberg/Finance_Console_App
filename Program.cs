namespace FinanceApp_Databaser;

class Program
{
    static async Task Main(string[] args)
    {
        var databaseService = new PostgresDatabaseService();
        using var connection = await databaseService.SetupDatabase();
        DependencyContainer container = new DependencyContainer(connection);

        var startup = new ApplicationStartup(container);
        startup.Initialize();

        IMenuService menuService = container.MenuService;

        while (true)
        {
            Console.Clear();
            menuService.GetMenu().Display();
            ConsoleKey inputCommand = Console.ReadKey(true).Key;

            if (inputCommand.Equals(ConsoleKey.Escape))
            {
                Utilities.WaitForKeyAny("Thank you for using our finance app!");
                break;
            }

            try
            {
                await menuService.GetMenu().ExecuteCommand(inputCommand);
            }
            catch (Exception ex)
            {
                string message = string.IsNullOrEmpty(ex.Message)
                    ? "Something went wrong, please try again."
                    : ex.Message;
                Utilities.WaitForKeyAny(message);
            }
        }
    }
}
