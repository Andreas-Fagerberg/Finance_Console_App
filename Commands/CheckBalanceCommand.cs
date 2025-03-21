namespace FinanceApp_Databaser;

public class CheckBalanceCommand : Command
{
    private readonly ITransactionService _transactionService;

    public CheckBalanceCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _transactionService = transactionService;
    }

    public override async Task Execute()
    {
        User? user = await userService.GetLoggedInUser();
        if (user == null)
        {
            Utilities.WaitForKeyAny("No user detected, please log in before checking balance.");
            return;
        }

        decimal? balance = await _transactionService.GetBalance(user);
        if (balance == null)
        {
            balance = 0;
        }
        Utilities.WaitForKeyAny("Current balance: " + balance);
    }
}
