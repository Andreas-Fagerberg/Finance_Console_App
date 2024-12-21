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
                Utilities.WaitForKeyAny("No user detected, returning to login menu");
                _menuService.SetMenu(
                    new InitialMenu(userService, _menuService, _transactionService)
                );
                return;
            }

            SubMenu.Display(SubMenuType.ADDTRANSACTION);
            ConsoleKey input = Console.ReadKey().Key;
            if (input.Equals(ConsoleKey.D2))
            {
                return;
            }
            Transaction transaction = new Transaction { UserId = user.UserId };

            Console.WriteLine("| ADD TRANSACTION |");
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
