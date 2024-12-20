using System.Runtime.InteropServices;

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

    public override async void Execute()
    {
        User? user = await userService.GetLoggedInUser();
        if (user == null)
        {
            Utilities.WaitForKeyAny("No user detected, please login before checking balance.");
            return;
        }

        decimal? balance = await _transactionService.GetBalance(user);
        if (balance == null)
        {
            Utilities.WaitForKeyAny("Current balance: 0");
            return;
        }
        Utilities.WaitForKeyAny("Current balance: " + balance);
    }
}
