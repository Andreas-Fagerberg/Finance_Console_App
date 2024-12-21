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

    public static int GetYear()
    {
        while (true)
        {
            Console.Write("\nEnter a year (YYYY): ");
            if (!int.TryParse(Console.ReadLine(), out int year)|| year <= 0 || year > DateTime.Now.Year)
            {
                Console.WriteLine($"Invalid input. Please enter a valid year between 1 and {DateTime.Now.Year}.");
                continue;
            }
            
            return year;
            
        }
    }
}
    // Method to get a year input (intended for date-based queries)
    

    // Method to get a month input (1-12)
    public static int GetMonth()
    {
        while (true)
        {
            Console.Write("\nEnter month (1-12): ");
            if (int.TryParse(Console.ReadLine(), out int month) && month >= 1 && month <= 12)
            {
                return month;
            }
            else
            {
                Console.WriteLine("Invalid month. Please enter a valid month (1-12).");
            }
        }
    }

    // Method to get a specific date input (DD/MM/YYYY format)
    public static DateTime GetDate()
    {
        while (true)
        {
            Console.Write("\nEnter date (DD/MM/YYYY): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime date))
            {
                return date;
            }
            else
            {
                Console.WriteLine("Invalid date format. Please enter the date in DD/MM/YYYY format.");
            }
        }
    }

    // Method to get a week number input (1-53)
    public static int GetWeekNumber()
    {
        while (true)
        {
            Console.Write("\nEnter week number (1-53): ");
            if (int.TryParse(Console.ReadLine(), out int week) && week >= 1 && week <= 53)
            {
                return week;
            }
            else
            {
                Console.WriteLine("Invalid week number. Please enter a valid week number (1-53).");
            }
        }
    }
}
