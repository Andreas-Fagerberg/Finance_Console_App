namespace FinanceApp_Databaser;

public class SubMenu
{
    public static void Display(string header, List<string> options)
    {
        Console.WriteLine($"\n                  {header}\n");
        for (int i = 0; i < options.Count; i++)
        {
            Console.WriteLine($"  [{i + 1}] - {options[i]}.");
        }
    }
}
