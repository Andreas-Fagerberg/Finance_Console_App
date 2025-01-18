using System.Linq.Expressions;

namespace FinanceApp_Databaser;

public class DisplayTransactionHandler
{
    public DisplayTransactionHandler() { }

    public async Task<DateSelectionResult> HandleDateSelection(bool isRemovalMode)
    {
        // If not in removal mode, show initial menu
        if (!isRemovalMode)
        {
            var continueOperation = await ShowInitialMenu();
            if (!continueOperation)
            {
                return new DateSelectionResult { ShouldExit = true };
            }
        }

        return await ShowDateSelectionMenu();
    }

    private async Task<bool> ShowInitialMenu()
    {
        var initialOptions = new List<string>
        {
            "Continue to transaction display",
            "Return to previous menu",
        };

        while (true)
        {
            Console.Clear();
            SubMenu.Display(" | Transaction Display | ", initialOptions);

            ConsoleKey inputKey = Console.ReadKey(true).Key;
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    return true;
                case ConsoleKey.D2:
                    return false;
                default:
                    continue;
            }
        }
    }

    private async Task<DateSelectionResult> ShowDateSelectionMenu()
    {
        var dateOptions = new List<string>
        {
            "Filter by Year",
            "Filter by Month",
            "Filter by Week",
            "Filter by specific Date",
            "Show all transactions",
        };
        Console.Clear();
        SubMenu.Display("Select Time Period", dateOptions);

        var input = Console.ReadKey().Key;
        return await ProcessDateTypeSelection(input);
    }

    private async Task<DateSelectionResult> ProcessDateTypeSelection(ConsoleKey input)
    {
        var result = new DateSelectionResult();

        switch (input)
        {
            case ConsoleKey.D1:
                result.DateType = DateType.YEAR;
                result.DateInput.Add(InputHelper.GetYear());
                break;
            case ConsoleKey.D2:
                result.DateType = DateType.MONTH;
                result.DateInput.Add(InputHelper.GetYear());
                result.DateInput.Add(InputHelper.GetMonth());
                break;
            case ConsoleKey.D3:
                result.DateType = DateType.WEEK;
                result.DateInput.Add(InputHelper.GetYear());
                result.DateInput.Add(InputHelper.GetWeek());
                break;
            case ConsoleKey.D4:
                result.DateType = DateType.WEEK;
                result.DateInput.Add(InputHelper.GetDate());
                break;
            case ConsoleKey.D5:
                result.DateType = DateType.NONE;
                break;
            default:
                Utilities.WaitForKeyAny("Unknown date type.");
                result.ShouldExit = true;
                break;
        }

        return result;
    }

    public async Task DisplayTransactions(List<Transaction> transactions, bool isRemovalMode)
    {
        if (transactions == null || !transactions.Any())
        {
            Utilities.WaitForKeyAny("No transactions found.");
            return;
        }

        Console.WriteLine();
        Transaction.PrintHeader();

        foreach (var transaction in transactions)
        {
            Console.WriteLine(transaction.StringConversion());
        }

        if (!isRemovalMode)
        {
            Utilities.WaitForKeyAny();
        }
    }
}
