namespace FinanceApp_Databaser;

public class Utilities
{
    /// <summary>
    /// Takes in a string, prints it and waits for the user to press a key.
    /// (Default message: 'Press any key to continue...').
    /// </summary>
    public static void WaitForKeyAny(string message = "")
    {
        Console.Write("\n\n" + message + "\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
    }
}
