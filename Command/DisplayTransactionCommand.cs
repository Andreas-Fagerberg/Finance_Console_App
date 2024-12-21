namespace FinanceApp_Databaser;

public class DisplayTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;

    public DisplayTransactionCommand(
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
        DateType dateType = DateType.NONE;
        List<string> dateInput = new List<string>();

        bool running = true;

        while (running)
        {
            ConsoleKey input;
            // Helps keep track of if user is removing a transaction and displays submenu only if false.
            if (!DeleteTransactionCommand.removing)
            {
                SubMenu.Display(SubMenuType.DISPLAYTRANSACTION1);
                input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.D1:
                        break;
                    case ConsoleKey.D2:
                        return;
                    default:
                        continue;
                }
            }

            Console.Clear();
            SubMenu.Display(SubMenuType.DISPLAYTRANSACTION2);
            input = Console.ReadKey().Key;
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
                Utilities.WaitForKeyAny("Year was selected.");
                break;
            case DateType.MONTH:
                dateInput.Add(InputHelper.GetYear());
                dateInput.Add(InputHelper.GetMonth());
                Utilities.WaitForKeyAny("Month was selected.");
                break;
            case DateType.WEEK:
                dateInput.Add(InputHelper.GetYear());
                dateInput.Add(InputHelper.GetWeek());
                Utilities.WaitForKeyAny("Week was selected.");
                break;
            case DateType.DATE:
                dateInput.Add(InputHelper.GetDate());
                Utilities.WaitForKeyAny("Date was selected.");
                break;
            case DateType.NONE:
                Utilities.WaitForKeyAny("All was selected.");
                break;
            default:
                Utilities.WaitForKeyAny("Unknown date type.");
                return;
        }

        List<Transaction>? transactions = await _transactionService.Load(dateType, dateInput);

        Console.WriteLine();
        Transaction.PrintHeader();
        if (transactions == null || transactions.Count < 1)
        {
            Utilities.WaitForKeyAny("No transactions found.");
            return;
        }

        foreach (Transaction transaction in transactions)
        {
            Console.WriteLine(transaction.StringConversion());
        }

        // Waits for user input here only if not removing transactions.
        if (!DeleteTransactionCommand.removing)
        {
            Utilities.WaitForKeyAny();
        }
    }
}
