namespace FinanceApp_Databaser;

public class MainMenu : Menu
{
    public MainMenu(
        IMenuService menuService,
        IUserService userService,
        ITransactionService transactionService
    )
    {
        // Add commands with their specific dependencies
        AddCommand(new LogoutCommand(ConsoleKey.D5, userService, menuService));
        AddCommand(new AddTransactionCommand(ConsoleKey.D2, userService, transactionService));
    }

    public override void Display()
    {
        Console.WriteLine("Main Menu:");
        Console.WriteLine("2. Process Transaction");
    }
}
