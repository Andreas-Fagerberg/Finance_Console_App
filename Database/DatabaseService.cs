namespace FinanceApp_Databaser;

// Using interface and abstract class
public interface IDatabaseService<TConnection>
    where TConnection : class
{
    Task<TConnection> SetupDatabase();
}

public abstract class DatabaseService<TConnection> : IDatabaseService<TConnection>
    where TConnection : class
{
    public abstract Task<TConnection> SetupDatabase();

    public void LogDatabaseSetup(string databaseType)
    {
        Console.WriteLine($"Setting up {databaseType} database at {DateTime.Now}");
    }
}
