using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresManager
{
    public NpgsqlConnection Connection { get; private set; }  
    public async void ConnectAsync()
    {
        string connectionString = "Host=localhost;Username=postgres;Password=password;Database=FinanceApp";
        Connection = new NpgsqlConnection(connectionString);
        await Connection.OpenAsync();

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

        await using var createTableCmd = new NpgsqlCommand(createTablesSql, Connection);
        await createTableCmd.ExecuteNonQueryAsync();
    }
}
