namespace FinanceApp_Databaser;

public class LoginCommand : Command
{
    public LoginCommand(DependencyContainer dependencyContainer)
        : base(ConsoleKey.D3, dependencyContainer) { }

    public override void Execute(ConsoleKey name)
    {
        throw new NotImplementedException();
    }
}
