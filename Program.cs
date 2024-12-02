namespace FinanceApp_Databaser;


class Program
{
    static async Task Main(string[] args)
    {
        await DatabaseService.InitializeDatabaseSchema();
        Console.WriteLine("FINANCE");      
    }
}
