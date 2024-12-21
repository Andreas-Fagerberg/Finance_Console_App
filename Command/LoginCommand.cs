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
            string username = InputHelper.GetUsername();
            string password = InputHelper.GetPassword();

            if (
                !ValidationHelper.ValidateNotEmpty(
                    username,
                    "Username cannot be empty or whitespace."
                )
                || !ValidationHelper.ValidateNotEmpty(
                    password,
                    "Password cannot be empty or whitespace."
                )
            )
            {
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
