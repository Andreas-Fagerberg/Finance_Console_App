namespace FinanceApp_Databaser;

public class DeleteTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;
    private readonly IMenuService _menuService;
    private readonly List<string> _menuContent;
    public static bool removing = false;

    public DeleteTransactionCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _menuService = menuService;
        _transactionService = transactionService;
        _menuContent = new List<string>
        {
            "Continue to remove transactions.",
            "Return to previous menu.",
        };
    }

    public override async Task Execute()
    {
        while (true)
        {
            Console.Clear();
            SubMenu.Display("|  TRANSACTION DISPLAY  |", _menuContent);
            ConsoleKey inputKey = Console.ReadKey(true).Key;
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    removing = true;
                    break;
                case ConsoleKey.D2:
                    return;
                default:
                    continue;
            }

            // Executes DisplayTransacionCommand
            await _menuService.GetMenu().ExecuteCommand(ConsoleKey.D4);

            List<Transaction>? transactions = await _transactionService.GetCurrentTransactions();

            if (transactions == null || transactions.Count <= 0)
            {
                continue;
            }
            Console.Write("\nPlease enter the ID of the transaction you wish to remove: ");
            string? id = Console.ReadLine();
            if (!ValidationHelper.ValidateNotEmpty(id, "ID cannot be empty or whitespace."))
            {
                continue;
            }
            if (!int.TryParse(id, out int parsedId))
            {
                Utilities.WaitForKeyAny("Please enter only numbers.");
                continue;
            }
            foreach (Transaction transaction in transactions)
            {
                if (transaction.RefId.Equals(parsedId))
                {
                    await _transactionService.Delete(transaction);

                    Utilities.WaitForKeyAny("Successfully deleted the transaction.");
                }
            }
            break;
        }
        removing = false;
    }
}
