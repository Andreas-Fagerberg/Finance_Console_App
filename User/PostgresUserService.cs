using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresUserService : IUserService
{
    private NpgsqlConnection connection;

    private Guid? loggedInUser = null;

    public PostgresUserService(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    public async Task<User?> GetLoggedInUser()
    {
        // Om ingen är inlogged så kan vi returnera null eftersom det inte finns något att hämta från databasen.
        if (loggedInUser == null)
        {
            return null;
        }

        // Försök att leta upp användaren med matchande id
        var sql = @"SELECT * FROM users WHERE user_id = @user_id";
        using var cmd = new NpgsqlCommand(sql, connection);

        cmd.Parameters.AddWithValue("@user_id", loggedInUser);

        using var reader = await cmd.ExecuteReaderAsync();
        // Om ingen användare matchade - returnera null
        if (!reader.Read())
        {
            return null;
        }

        var user = new User
        {
            UserId = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2),
        };

        return user;
    }

    public async Task<User?> Login(string username, string password)
    {
        var sql = "SELECT * FROM users WHERE name = @username AND password = @password";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
        {
            return null;
        }
        var user = new User
        {
            UserId = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2),
        };
        return user;
    }

    public void Logout()
    {
        loggedInUser = null;
    }

    public async Task<string> RegisterUser(string username, string password)
    {
        User user = new User
        {
            UserId = Guid.NewGuid(),
            Name = username,
            Password = password,
        };

        var sql =
            @"INSERT INTO users (user_id, name, password) VAlUES
        (
            @user_id,
            @name,
            @password
        )";
        using var cmd = new NpgsqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@user_id", user.UserId);
        cmd.Parameters.AddWithValue("@name", user.Name);
        cmd.Parameters.AddWithValue("@password", user.Password);

        await cmd.ExecuteNonQueryAsync();

        loggedInUser = user.UserId;
        return user.Name;
    }

    public async Task<bool> CheckUserExists(string username)
    {
        var sql = @"SELECT * FROM users WHERE name = @name";
        using var cmd = new NpgsqlCommand(sql, connection);

        cmd.Parameters.AddWithValue("@name", username);

        using var reader = await cmd.ExecuteReaderAsync();
        // Om ingen användare matchade - returnera null
        if (!reader.Read())
        {
            return false;
        }

        return true;
    }
}
