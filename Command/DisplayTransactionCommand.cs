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
        DateType dateType = DateType.NONE;

        bool running = true;

        while (running)
        {
            ConsoleKey input = Console.ReadKey().Key;
            switch (input)
            {
                case ConsoleKey.D1:
                    dateType = DateType.YEAR;
                    running = false;
                    break;
                case ConsoleKey.D2:
                    dateType = DateType.MONTH;
                    running = false;
                    break;
                case ConsoleKey.D3:
                    dateType = DateType.WEEK;
                    running = false;
                    break;
                case ConsoleKey.D4:
                    dateType = DateType.DATE;
                    running = false;
                    break;
                case ConsoleKey.D5:
                    dateType = DateType.NONE;
                    break;
                default:
                    await Utilities.WaitForKeyAny("Invalid input, please try again.");
                    continue;
            }
        }

        List<Transaction>? transactions = await _transactionService.Load(dateType, date);
        if (transactions == null)
        {
            await Utilities.WaitForKeyAny("No user detected.");
            _menuService.SetMenu(
                new InitialMenu(userService, _menuService, _transactionService)
            );
            return;
        }
        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine(transaction);
        }
        Console.ReadKey();
        }
    }
}
