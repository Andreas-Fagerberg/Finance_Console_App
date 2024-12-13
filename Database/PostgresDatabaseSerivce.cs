using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresDatabaseService : DatabaseService<NpgsqlConnection>
{
    private static readonly string _connectionString =
        "Host=localhost;Username=postgres;Password=MhobBhgh606;Database=finance_app";

    // Include Error Detail=true;

    public override async Task<NpgsqlConnection> SetupDatabase()
    {
        using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        LogDatabaseSetup("PostgreSQL");
        var createTablesSql =
            @"
            CREATE TABLE IF NOT EXISTS users (
                user_id UUID PRIMARY KEY,
                name TEXT,
                password TEXT
            );
            CREATE TABLE IF NOT EXISTS accounts (
                account_id UUID PRIMARY KEY,
                user_id UUID REFERENCES users(user_id)
                type TEXT CHECK (type IN ('savings account', 'checking accounts')
            )
            CREATE TABLE IF NOT EXISTS transactions (
                transaction_id UUID PRIMARY KEY,
                user_id UUID REFERENCES users(user_id),
                title TEXT,
                amount DECIMAL,
                description TEXT,
                date TIMESTAMP WITH TIME ZONE
            )";

        await using var createTableCmd = new NpgsqlCommand(createTablesSql, connection);
        await createTableCmd.ExecuteNonQueryAsync();
        return connection;
    }
}
