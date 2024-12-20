namespace FinanceApp_Databaser;

public class InitialMenu : Menu
{
    public InitialMenu(
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
    {
        // csharpier-ignore-start
        AddCommand(new RegisterUserCommand(ConsoleKey.D1, userService, menuService, transactionService));
        AddCommand(new LoginCommand(ConsoleKey.D2, userService, menuService, transactionService));
        // csharpier-ignore-end
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
