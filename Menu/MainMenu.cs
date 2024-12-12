namespace FinanceApp_Databaser;

public class MainMenu : Menu
{
    public MainMenu(DependencyContainer dependencyContainer)
    {
        AddCommand(new LoginCommand(dependencyContainer));
    }

    public override void Display()
    {
        Console.WriteLine(
            """
                    Welcome to Main Menu

                    [1] 
                    [2]
                    [3]
                    [4]
                    [5]
            """
        );
    }
}
