namespace FinanceApp_Databaser;

public class RegisterUserCommand : Command
{
    private readonly List<string> _menuContent;

    public RegisterUserCommand(ConsoleKey triggerKey, IUserService userService)
        : base(triggerKey, userService)
    {
        _menuContent = new List<string>
        {
            "Continue to register user.",
            "Return to previous menu.",
        };
    }

    public override async Task Execute()
    {
        while (true)
        {
            Console.Clear();
            SubMenu.Display("|  REGISTER USER  |", _menuContent);

            ConsoleKey inputKey = Console.ReadKey(true).Key;
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    return;
                default:
                    continue;
            }

            Console.Clear();
            Console.WriteLine("\n                  |  REGISTER USER  |\n");

            // Get user input
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
                continue;
            }

            if (!ValidatePasswordStrength(password))
            {
                Utilities.WaitForKeyAny(
                    "Password must be at least 8 characters long and contain a mix of letters and numbers."
                );
                continue;
            }

            if (await userService.CheckUserExists(username))
            {
                Utilities.WaitForKeyAny("A user with that name already exists");
                continue;
            }

            string loggedInUser = await userService.RegisterUser(username, password);
            Utilities.WaitForKeyAny($"The user: {loggedInUser} was created successfully.");
            return;
        }
    }

    private bool ValidatePasswordStrength(string password)
    {
        return password.Length >= 8 && password.Any(char.IsLetter) && password.Any(char.IsDigit);
    }
}
