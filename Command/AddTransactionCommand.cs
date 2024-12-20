namespace FinanceApp_Databaser;

public class AddTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;

    public AddTransactionCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _transactionService = transactionService;
    }

    public override void Execute(ConsoleKey name) { }
}
