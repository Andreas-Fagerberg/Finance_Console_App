namespace FinanceApp_Databaser;

public class DeleteTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;
    private readonly IMenuService _menuService;
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
    }

    public override async Task Execute()
    {
        while (true)
        {
            SubMenu.Display(SubMenuType.DELETETRANSACTION);
            ConsoleKey inputKey = Console.ReadKey().Key;
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
                    if (!await _transactionService.Delete(transaction))
                    {
                        Utilities.WaitForKeyAny("Failed to delete the transaction.");
                        return;
                    }
                    Utilities.WaitForKeyAny("Successfully deleted the transaction.");
                }
            }

            Utilities.WaitForKeyAny("Hello!");
            break;
        }
        removing = false;
    }
}
