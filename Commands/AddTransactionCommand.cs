namespace FinanceApp_Databaser;

public class AddTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;
    private readonly List<string> _menuContent;

    public AddTransactionCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _transactionService = transactionService;
        _menuContent = new List<string>
        {
            "Continue to add transactions.",
            "Return to previous menu.",
        };
    }

    public override async Task Execute()
    {
        User? user = await userService.GetLoggedInUser();
        if (user == null)
        {
            Utilities.WaitForKeyAny("No user detected, please log in before checking balance.");
            return;
        }
        Transaction transaction = new Transaction { UserId = user.UserId };
        while (true)
        {
            Console.Clear();
            SubMenu.Display("|  ADD TRANSACTION  |", _menuContent);
            ConsoleKey inputKey = Console.ReadKey(true).Key;
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    return;
                default:
                    continue;
            }

            Console.Clear();
            Console.WriteLine("| ADD TRANSACTION |\n");
            string? description = InputHelper.GetDescription();

            if (string.IsNullOrWhiteSpace(description) || description.Length > 35)
            {
                Utilities.WaitForKeyAny(
                    "Description must be between 1-35 characters long, please try again."
                );
                continue;
            }

            string? tempAmount = InputHelper.GetAmount();

            if (
                string.IsNullOrWhiteSpace(tempAmount)
                || tempAmount.Length > 20
                || !decimal.TryParse(tempAmount, out decimal amount)
            )
            {
                Utilities.WaitForKeyAny(
                    "Amount must be between 1-20 characters long and only consist of an integer or decimal"
                );
                continue;
            }

            transaction.Description = description;
            transaction.Amount = amount;
            try
            {
                await _transactionService.Save(transaction);
                Utilities.WaitForKeyAny("Transaction successfully saved");
            }
            catch (Exception ex)
            {
                Utilities.WaitForKeyAny(
                    "An error occured while saving the transaction" + ex.Message
                );
                continue;
            }
        }
    }
}
