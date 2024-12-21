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
        List<string> dateInput = new List<string>();

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
                    running = false;
                    break;
                default:
                    Utilities.WaitForKeyAny("Invalid input, please try again.");
                    continue;
            }
        }

        switch (dateType)
        {
            case DateType.YEAR:
                dateInput.Add(InputHelper.GetYear());
                break;
            case DateType.MONTH:
                dateInput.Add(InputHelper.GetYear());
                dateInput.Add(InputHelper.GetMonth());
                break;
            case DateType.WEEK:
                dateInput.Add(InputHelper.GetYear());
                dateInput.Add(InputHelper.GetWeek());
                break;
            case DateType.DATE:
                dateInput.Add(InputHelper.GetDate());
                break;
            case DateType.NONE:
                Utilities.WaitForKeyAny("No date type selected.");
                break;
            default:
                Utilities.WaitForKeyAny("Unknown date type.");
                return;
        }

        List<Transaction>? transactions = await _transactionService.Load(dateType, dateInput);
        if (transactions == null)
        {
            Utilities.WaitForKeyAny("No user detected.");
            _menuService.SetMenu(new InitialMenu(userService, _menuService, _transactionService));
            return;
        }
        if (transactions.Count < 1)
        {
            Utilities.WaitForKeyAny("No transactions found.");
            return;
        }

        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine(transaction.Description);
            Console.WriteLine(transaction.RefId);
        }
        Console.ReadKey();
    }
}
