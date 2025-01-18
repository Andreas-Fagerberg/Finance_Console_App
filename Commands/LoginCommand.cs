namespace FinanceApp_Databaser;

public class LoginCommand : Command
{
    private readonly IMenuService _menuService;
    private readonly ITransactionService _transactionService;

    public LoginCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _menuService = menuService;
        _transactionService = transactionService;
    }

    public override async Task Execute()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\n                  |  LOGIN  |\n");

            // Get credentials from user
            string username = InputHelper.GetUsername();
            string password = InputHelper.GetPassword();

            // Validate input
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
                return;
            }

            // The userService will now handle password verification internally
            User? user = await userService.Login(username, password);

            if (user is null)
            {
                // Use a generic error message for security
                Utilities.WaitForKeyAny("Invalid username or password.");
                continue;
            }

            _menuService.SetMenu(new MainMenu(_menuService, userService, _transactionService));
            return;
        }
    }
}
