namespace FinanceApp_Databaser;

public class Utilities
{
    /// <summary>
    /// Prints out the entered string and waits for user to press a key. (Default message: 'Press any key to continue...').
    /// </summary>
    public static void WaitForKeyAny(string message = "Press any key to continue...")
    {
        Console.Write(message);
        Console.ReadKey();
    }
}
