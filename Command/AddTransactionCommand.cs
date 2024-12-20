namespace FinanceApp_Databaser;

public class AddTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;
    private readonly IMenuService _menuService;

    public AddTransactionCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        ITransactionService transactionService,
        IMenuService menuService
    )
        : base(triggerKey, userService)
    {
        _transactionService = transactionService;
        _menuService = menuService;
    }

    public override async Task Execute()
    {
        User? user = await userService.GetLoggedInUser();
        while (true)
        {
            if (user == null)
            {
                await Utilities.WaitForKeyAny("No user detected, returning to login menu");
                _menuService.SetMenu(new LoginMenu(userService, _menuService, _transactionService));
                return;
            }

            Transaction transaction = new Transaction { UserId = user.UserId };

            Console.Write("Enter a description: ");
            string? description = Console.ReadLine();
            if (string.IsNullOrEmpty(description))
            {
                await Utilities.WaitForKeyAny("Please enter a description for your transaction.");
                continue;
            }
            transaction.Description = description;

            Console.Write("\nEnter an amount: ");
            decimal amount;
            if (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                await Utilities.WaitForKeyAny("Please enter an amount for your transaction");
                continue;
            }
            transaction.Amount = amount;
            try
            {
                _transactionService.Save(transaction);
            }
            catch
            {
                await Utilities.WaitForKeyAny("An error occured while saving the transaction");
                continue;
            }
        }
    }
}
