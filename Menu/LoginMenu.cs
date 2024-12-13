namespace FinanceApp_Databaser;

public class LoginMenu : Menu
{
    public LoginMenu(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
    {
        AddCommand(new LoginCommand(ConsoleKey.D1, userService, menuService, transactionService));
    }

    public override void Display()
    {
        Console.WriteLine(
            """
                    Welcome to login menu
            """
        );
    }
}
