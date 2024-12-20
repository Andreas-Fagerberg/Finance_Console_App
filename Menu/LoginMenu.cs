namespace FinanceApp_Databaser;

public class LoginMenu : Menu
{
    public LoginMenu(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
    {
        AddCommand(
            new RegisterUserCommand(ConsoleKey.D1, userService, menuService, transactionService)
        );
        AddCommand(new LoginCommand(ConsoleKey.D2, userService, menuService, transactionService));
    }

    public override void Display()
    {
        Console.Write(
            $"""

            $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ 
            $                                                               $
            $           $$$ The Non Descript Finance App $$$                $
            $                                                               $
            $  The number 1 Finance app for at least some financial needs!  $
            $                                                               $
            $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $ $

               [1]   - Create a new user.
               [2]   - Select an existing user.

               [ESC]  - Exit the application.

            command: 
            """
        );
    }
}
