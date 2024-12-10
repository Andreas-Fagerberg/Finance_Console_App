namespace FinanceApp_Databaser;

public class LoginMenu : Menu
{
    public LoginMenu(DependencyContainer dependencyContainer)
    {
        AddCommand(new LoginCommand(dependencyContainer));
    }

    public override void Display()
    {
        // throw new NotImplementedException();
    }
}
