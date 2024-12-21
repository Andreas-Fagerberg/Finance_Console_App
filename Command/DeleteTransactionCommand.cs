namespace FinanceApp_Databaser;

public class DeleteTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;

    public DeleteTransactionCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _transactionService = transactionService;
    }
}
