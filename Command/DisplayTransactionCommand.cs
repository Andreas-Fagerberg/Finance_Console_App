namespace FinanceApp_Databaser;

public class DisplayTransactionCommand : Command
{
    private readonly IMenuService _menuService;
    private readonly ITransactionService _transactionService;

    public DisplayTransactionCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _menuService = menuService;
        _transactionService = transactionService;
    }

    public override async Task Execute()
    {
        throw new NotImplementedException();
    }
}
