namespace FinanceApp_Databaser;

public class Utilities
{
    /// <summary>
    /// Takes in a string, prints it and waits for user to press a key. (Default message: 'Press any key to continue...').
    /// </summary>
    public static void WaitForKeyAny(string message = "Press any key to continue...")
    {
        Console.WriteLine(message);
        Console.ReadKey();
    }
}
