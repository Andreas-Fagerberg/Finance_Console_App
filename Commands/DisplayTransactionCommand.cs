namespace FinanceApp_Databaser;

public class DisplayTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;
    private readonly DisplayTransactionHandler _displayHandler;

    public DisplayTransactionCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _transactionService = transactionService;
        _displayHandler = new DisplayTransactionHandler();
    }

    public override async Task Execute()
    {
        var user = await userService.GetLoggedInUser();
        if (user == null)
        {
            Utilities.WaitForKeyAny("No user detected, please log in before checking balance.");
            return;
        }

        // The handler takes care of menu display and input processing
        var dateSelection = await _displayHandler.HandleDateSelection(
            DeleteTransactionCommand.removing
        );
        if (dateSelection.ShouldExit)
            return;

        // Load transactions using the selected criteria
        var transactions = await _transactionService.Load(
            dateSelection.DateType,
            dateSelection.DateInput
        );

        // Display the results
        await _displayHandler.DisplayTransactions(transactions!, DeleteTransactionCommand.removing);
    }
}
