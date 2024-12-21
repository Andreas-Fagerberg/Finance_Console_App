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
        var dateInput = string.Empty;

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
                    Utilities.WaitForKeyAny("Invalid input, please try again.");
                    continue;
            }
        }
        switch (dateType)
    {
        case DateType.YEAR:
            dateInput = await InputHelper.GetYear();  // Get year input
            break;
        case DateType.MONTH:
            dateInput = await InputHelper.GetMonth(); // Get month input
            break;
        case DateType.WEEK:
            dateInput = await InputHelper.GetWeek();  // Get week input
            break;
        case DateType.DATE:
            dateInput = await InputHelper.GetDate();  // Get date input
            break;
        case DateType.NONE:
            Utilities.WaitForKeyAny("No date type selected.");
            return;
        default:
            Utilities.WaitForKeyAny("Unknown date type.");
            return;
    }

    // Output the final input (formatted date)
    Console.WriteLine($"You selected: {dateType} with the value: {dateInput}");
}

        List<Transaction>? transactions = await _transactionService.Load(dateType, date);
        if (transactions == null)
        {
            Utilities.WaitForKeyAny("No user detected.");
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
