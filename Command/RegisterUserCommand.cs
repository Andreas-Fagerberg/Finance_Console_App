namespace FinanceApp_Databaser;

public class RegisterUserCommand : Command
{
    private readonly IMenuService _menuService;
    private readonly ITransactionService _transactionService;

    public RegisterUserCommand(
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

    public override async void Execute()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("| REGISTER |\n");
            Console.Write("Username: ");
            string? username = Console.ReadLine();
            Console.Write("\nPassword: ");
            string? password = Console.ReadLine();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                Utilities.WaitForKeyAny("Username or password input cannot be empty.");
                continue;
            }
            if (await userService.CheckUserExists(username))
            {
                Utilities.WaitForKeyAny("A user with that name already exists");
            }
            string loggedInUser = await userService.RegisterUser(username, password);

            Utilities.WaitForKeyAny("The user: " + loggedInUser + " was created successfully.");
        }
    }
}
