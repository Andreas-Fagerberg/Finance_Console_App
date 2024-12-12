namespace FinanceApp_Databaser;

public class LoginCommand : Command
{
    public LoginCommand(DependencyContainer dependencyContainer)
        : base(ConsoleKey.D3, dependencyContainer) { }

    public override void Execute(ConsoleKey name)
    {
        Console.WriteLine("Login");
        Console.Write("Username: ");
        string? username = Console.ReadLine();
        Console.Write("\nPassword: ");
        string? password = Console.ReadLine();

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            Console.WriteLine("Username and password cannot be empty. Please try again.");
            return;
        }

        userService.Login(username, password);
        menuService.SetMenu(new MainMenu());
    }
}
