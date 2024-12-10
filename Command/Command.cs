namespace FinanceApp_Databaser;

public abstract class Command
{
    public ConsoleKey TriggerKey { get; init; }
    protected IUserService userService;
    protected ITransactionService transactionService;
    protected IMenuService menuService;

    public Command(ConsoleKey triggerKey, DependencyContainer dependencyContainer)
    {
        this.TriggerKey = triggerKey;
        this.userService = dependencyContainer.UserService;
        this.transactionService = dependencyContainer.TransactionService;
        this.menuService = dependencyContainer.MenuService;
    }

    public abstract void Execute(ConsoleKey TriggerKey);
}
