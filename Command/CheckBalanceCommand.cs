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

    public override async Task Execute()
    {
        User? user = await userService.GetLoggedInUser();
        if (user == null)
        {
            await Utilities.WaitForKeyAny(
                "No user detected, please login before checking balance."
            );
            return;
        }

        decimal? balance = await _transactionService.GetBalance(user);
        if (balance == null)
        {
            await Utilities.WaitForKeyAny("Current balance: 0");
            return;
        }
        await Utilities.WaitForKeyAny("Current balance: " + balance);
    }
}
