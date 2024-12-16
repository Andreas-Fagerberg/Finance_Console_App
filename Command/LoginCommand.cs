namespace FinanceApp_Databaser;

public class LoginCommand : Command
{
    private readonly IMenuService menuService;
    private readonly ITransactionService transactionService;

    public LoginCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        this.menuService = menuService;
        this.transactionService = transactionService;
    }

    public override void Execute(ConsoleKey name)
    {
        while (true)
        {
            Console.WriteLine("Login");
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            Console.Write("\nPassword: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Utilities.WaitForKeyAny(
                    """
                    Username and password cannot be empty.
                    Press any key to continue...
                    """
                );
                continue;
            }

            if (userService.Login(username, password) is null)
            {
                Utilities.WaitForKeyAny(
                    """
                    No user found with those credentials. 
                    Press any key to continue...
                    """
                );
                continue;
            }

            menuService.SetMenu(new MainMenu(menuService, userService, transactionService));
            return;
        }
    }
}
