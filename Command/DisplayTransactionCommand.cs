using System.Data.Common;

namespace FinanceApp_Databaser;

public class DisplayTransactionCommand : Command
{
    private readonly IMenuService _menuService;
    private readonly ITransactionService _transactionService;

    public DisplayTransactionCommand(
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
        string dateType = string.Empty;

        bool running = true;
        while (running)
        {
            ConsoleKey input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    dateType = "YEAR";
                    running = false;
                    break;
                case ConsoleKey.D2:
                    dateType = "MONTH";
                    running = false;
                    break;
                case ConsoleKey.D3:
                    dateType = "WEEK";
                    running = false;
                    break;
                case ConsoleKey.D4:
                    dateType = "DATE";
                    running = false;
                    break;
                default:
                    continue;
            }
        }
        string? dateFilter = Console.ReadLine();
        if (string.IsNullOrEmpty(dateFilter))
        {
            return;
        }

        List<Transaction>? transactions = await _transactionService.Load(dateType, dateFilter, sql);
        if (transactions == null)
        {
            await Utilities.WaitForKeyAny("No user detected.");
            _menuService.SetMenu(new InitialMenu(userService, _menuService, _transactionService));
            return;
        }
        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
        Console.ReadKey();
    }
}
