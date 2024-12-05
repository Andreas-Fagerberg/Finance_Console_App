namespace FinanceApp_Databaser;

public abstract class Command
{
    public ConsoleKey Name { get; init; }

    protected IUserService userService;
    protected ITransactionService transactionService;
    public Command (ConsoleKey name, IUserService userService, ITransactionService transactionService, IMenuService menuService)
    {
    }
    public abstract void Execute(ConsoleKey name);
}
