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
                Console.WriteLine("Username and password cannot be empty. Please try again.");
                continue;
            }

            if (userService.Login(username, password) is null)
            {
                Console.WriteLine("No user found with those credentials. Please try again.");
                Utilities.WaitForKeyAny();
                continue;
            }

            menuService.SetMenu(new MainMenu(menuService, userService, transactionService));
            return;
        }
    }
}
