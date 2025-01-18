using System.Text;

namespace FinanceApp_Databaser;

public static class InputHelper
{
    public static string GetUsername()
    {
        Console.Write("Username: ");
        string? username = Console.ReadLine();
        return username ?? string.Empty;
    }

    public static string GetPassword()
    {
        Console.Write("\nPassword: ");
        StringBuilder password = new StringBuilder();
        while (true)
        {
            var input = Console.ReadKey(true);

            //
            if (input.Key == ConsoleKey.Enter)
            {
                break;
            }
            else if (input.Key == ConsoleKey.Backspace && password.Length > 0)
            {
                password.Remove(password.Length - 1, 1);
            }
            else
            {
                password.Append(input.KeyChar);
                Console.Write("*");
            }
        }
        Console.WriteLine();
        return password.ToString();
    }

    public static string GetDescription()
    {
        Console.Write("Enter a description: ");
        string? description = Console.ReadLine();
        return description ?? string.Empty;
    }

    public static string GetAmount()
    {
        Console.Write("\nEnter an amount: ");
        string? username = Console.ReadLine();
        return username ?? string.Empty;
    }

    public static string GetYear()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("\nEnter a year (YYYY): ");
            if (
                !int.TryParse(Console.ReadLine(), out int year)
                || year < 1
                || year > DateTime.Now.Year
            )
            {
                Utilities.WaitForKeyAny(
                    $"Invalid input. Please enter a valid year between 1 and {DateTime.Now.Year}."
                );
                continue;
            }

            return year.ToString();
        }
    }

    public static string GetMonth()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("\nEnter month (1-12): ");
            if (!int.TryParse(Console.ReadLine(), out int month) || month < 1 || month > 12)
            {
                Utilities.WaitForKeyAny("Invalid month. Please enter a valid month (1-12).");
                continue;
            }

            return month.ToString();
        }
    }

    public static string GetWeek()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("\nEnter week number (1-53): ");
            if (!int.TryParse(Console.ReadLine(), out int week) || week < 1 | week > 53)
            {
                Utilities.WaitForKeyAny(
                    "Invalid week number. Please enter a valid week number (1-53)."
                );
                continue;
            }

            return week.ToString();
        }
    }

    public static string GetDate()
    {
        while (true)
        {
            Console.Clear();
            Console.Write("\nEnter date (YYYY-MM-DD): ");
            if (
                !DateTime.TryParseExact(
                    Console.ReadLine(),
                    "yyyy-MM-dd",
                    null,
                    System.Globalization.DateTimeStyles.None,
                    out DateTime date
                )
            )
            {
                Utilities.WaitForKeyAny(
                    "Invalid date format. Please enter the date in YYYY-MM-DD format."
                );
                continue;
            }
            return date.ToString("yyyy-MM-dd");
        }
    }
}
