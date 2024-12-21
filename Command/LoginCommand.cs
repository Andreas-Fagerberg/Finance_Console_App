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

            _menuService.SetMenu(new MainMenu(_menuService, userService, _transactionService));
            return;
        }
    }
}
