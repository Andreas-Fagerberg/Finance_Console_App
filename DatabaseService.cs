namespace FinanceApp_Databaser;

using Npgsql;
// Using interface and abstract class
public interface IDatabaseService<TConnection> where TConnection : class
{
    Task<TConnection> SetupDatabase();
}
public abstract class DatabaseService<TConnection> : IDatabaseService<TConnection> where TConnection : class
{
    public abstract Task<TConnection> SetupDatabase();
    protected void LogDatabaseSetup(string databaseType)
    {
        Console.WriteLine($"Setting up {databaseType} database at {DateTime.Now}");
    }
        
}
public class PostgresDatabaseService : DatabaseService<NpgsqlConnection> {
    
    private static readonly string _connectionString = "Host=localhost;Username=postgres;Password=MhobBhgh606;Database=finance_app";
    public override async Task<NpgsqlConnection> SetupDatabase()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(); 

        var createTablesSql = @"
            CREATE TABLE IF NOT EXISTS users (
                user_id UUID PRIMARY KEY,
                name TEXT,
                password TEXT
            );
            CREATE TABLE IF NOT EXISTS transactions (
                transaction_id UUID PRIMARY KEY,
                user_id UUID REFERENCES users(user_id),
                title TEXT,
                amount DECIMAL,
                description TEXT,
                date DATE
            )";

        await using var createTableCmd = new NpgsqlCommand(createTablesSql, connection);
        await createTableCmd.ExecuteNonQueryAsync();
        return connection;
    }
}

