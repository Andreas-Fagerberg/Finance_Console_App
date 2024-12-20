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
        // AddCommand(new LogoutCommand(ConsoleKey.D5, userService, menuService));
        AddCommand(
            new AddTransactionCommand(ConsoleKey.D2, userService, transactionService, menuService)
        );
        AddCommand(new CheckBalanceCommand(ConsoleKey.D3, userService, transactionService));
        AddCommand(new LogoutCommand(ConsoleKey.D5, userService, menuService, transactionService));
    }

    public override void Display()
    {
        Console.Write(
            $"""
                            
                              |  MAIN MENU  |
                            
              [1]  - Add a new transaction to your account.
              [2]  - Remove an existing transaction from your account.
              [3]  - Display your current account balance.
              [4]  - Display your transactions.
              
              [5]  - Log out and return to the login screen.
              [ESC]  - Exit the application.

            Command: 
            """
        );
    }
}
