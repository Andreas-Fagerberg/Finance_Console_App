namespace FinanceApp_Databaser;

public interface IDependencyContainer
{
    IUserService UserService { get; }
    ITransactionService TransactionService { get; }
    IMenuService MenuService { get; }
}

public class DependencyContainer : IDependencyContainer
{
    // Lazily initialized services to ensure efficient resource use
    private Lazy<IUserService> _userService;
    private Lazy<ITransactionService> _transactionService;
    private Lazy<IMenuService> _menuService;

    public DependencyContainer(
        Func<IUserService> userServiceFactory,
        Func<ITransactionService> transactionServiceFactory,
        Func<IMenuService> menuServiceFactory
    )
    {
        // Use lazy initialization to create services only when first accessed
        _userService = new Lazy<IUserService>(userServiceFactory);
        _transactionService = new Lazy<ITransactionService>(transactionServiceFactory);
        _menuService = new Lazy<IMenuService>(menuServiceFactory);
    }

    // Implement the dependency container interface
    public IUserService UserService => _userService.Value;
    public ITransactionService TransactionService => _transactionService.Value;
    public IMenuService MenuService => _menuService.Value;
}
