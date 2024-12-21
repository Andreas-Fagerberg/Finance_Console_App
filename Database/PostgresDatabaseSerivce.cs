using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresDatabaseService : DatabaseService<NpgsqlConnection>
{
    private static readonly string _connectionString =
        "Host=localhost;Username=postgres;Password=LpgKjff152_;Database=finance_app";

    public override async Task<NpgsqlConnection> SetupDatabase()
    {
        // Create a new connection without 'using' statement so it stays alive
        var connection = new NpgsqlConnection(_connectionString);

        try
        {
            // Open the connection
            await connection.OpenAsync();
            LogDatabaseSetup("PostgreSQL");

            // Define our table creation SQL
            var createTablesSql =
                @"
                CREATE TABLE IF NOT EXISTS users (
                    user_id UUID PRIMARY KEY,
                    name TEXT,
                    password TEXT
                );
       
                CREATE TABLE IF NOT EXISTS transactions (
                    transaction_id UUID PRIMARY KEY,
                    user_id UUID REFERENCES users(user_id) ON DELETE CASCADE,
                    description TEXT,
                    amount DECIMAL,
                    transfer_date TIMESTAMP WITH TIME ZONE
                )";

            // Create and execute the command
            await using var createTableCmd = new NpgsqlCommand(createTablesSql, connection);
            await createTableCmd.ExecuteNonQueryAsync();

            // Return the open connection for further use
            return connection;
        }
        catch (Exception)
        {
            // If anything goes wrong during setup, make sure we clean up
            await connection.DisposeAsync();
            throw new Exception("Error setting up database");
        }
    }
}
