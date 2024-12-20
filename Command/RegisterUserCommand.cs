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

    public override async Task Execute()
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
                await Utilities.WaitForKeyAny("Username or password input cannot be empty.");
                continue;
            }

            bool userExists = await userService.CheckUserExists(username);

            if (userExists)
            {
                await Utilities.WaitForKeyAny("A user with that name already exists");
                continue;
            }
            else
            {
                string loggedInUser = await userService.RegisterUser(username, password);

                await Utilities.WaitForKeyAny(
                    "The user: " + loggedInUser + " was created successfully."
                );
                return;
            }
        }
    }
}
