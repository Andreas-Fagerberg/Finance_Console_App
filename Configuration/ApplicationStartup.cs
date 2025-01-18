namespace FinanceApp_Databaser;

public class ApplicationStartup
{
    private readonly DependencyContainer _container;

    public ApplicationStartup(DependencyContainer container)
    {
        _container = container;
    }

    public void Initialize()
    {
        // Create initial menu with all necessary dependencies
        var initialMenu = new InitialMenu(
            _container.UserService,
            _container.MenuService,
            _container.TransactionService
        );

        // Set the initial menu
        _container.MenuService.SetMenu(initialMenu);
    }
}
