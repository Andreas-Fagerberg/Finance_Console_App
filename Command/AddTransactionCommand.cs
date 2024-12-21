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

    public override async Task Execute()
    {
        User? user = await userService.GetLoggedInUser();
        if (user == null)
        {
            Utilities.WaitForKeyAny("No user detected, please log in before checking balance.");
            return;
        }
        while (true)
        {
            SubMenu.Display(SubMenuType.ADDTRANSACTION);
            ConsoleKey input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    break;
                case ConsoleKey.D2:
                    return;
                default:
                    continue;
            }
            Transaction transaction = new Transaction { UserId = user.UserId };

            Console.Clear();
            Console.WriteLine("| ADD TRANSACTION |\n");
            Console.Write("Enter a description: ");
            string? description = Console.ReadLine();
            if (string.IsNullOrEmpty(description))
            {
                Utilities.WaitForKeyAny("Please enter a description for your transaction.");
                continue;
            }
            transaction.Description = description;

            Console.Write("\nEnter an amount: ");
            decimal amount;
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Utilities.WaitForKeyAny("Please enter an amount for your transaction");
                continue;
            }
            transaction.Amount = amount;
            try
            {
                await _transactionService.Save(transaction);
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
