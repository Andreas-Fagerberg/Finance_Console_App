namespace FinanceApp_Databaser;

public class RegisterUserCommand : Command
{
    public RegisterUserCommand(ConsoleKey triggerKey, IUserService userService)
        : base(triggerKey, userService) { }

    public override async Task Execute()
    {
        while (true)
        {
            SubMenu.Display(SubMenuType.REGISTERUSER);
            ConsoleKey input = Console.ReadKey(true).Key;
            if (input.Equals(ConsoleKey.D2))
            {
                return;
            }
            Console.Clear();
            Console.WriteLine("| REGISTER USER |\n");

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

            if (await userService.CheckUserExists(username))
            {
                Utilities.WaitForKeyAny("A user with that name already exists");
                continue;
            }
            else
            {
                string loggedInUser = await userService.RegisterUser(username, password);

                Utilities.WaitForKeyAny("The user: " + loggedInUser + " was created successfully.");
                return;
            }
        }
    }
}
