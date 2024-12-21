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

    public override async Task Execute()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("| LOGIN |\n");
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            Console.Write("\nPassword: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Utilities.WaitForKeyAny("Username or password input cannot be empty.");
                continue;
            }

            User? user = await userService.Login(username, password);
            if (user is null)
            {
                Utilities.WaitForKeyAny("No user found with those credentials.");
                continue;
            }

            menuService.SetMenu(new MainMenu(menuService, userService, transactionService));
            return;
        }
    }
}
