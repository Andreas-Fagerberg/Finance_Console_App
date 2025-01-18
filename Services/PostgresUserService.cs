using BCrypt.Net;
using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresUserService : IUserService
{
    private readonly NpgsqlConnection _connection;
    private Guid? _loggedInUser = null;

    // WorkFactor determines the computational complexity of the hash
    // 12 provides a good balance between security and performance as of 2024
    private const int WorkFactor = 12;

    public PostgresUserService(NpgsqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<User?> GetLoggedInUser()
    {
        if (_loggedInUser == null)
        {
            return null;
        }

        var sql = @"SELECT user_id, name, password FROM users WHERE user_id = @user_id";
        using var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@user_id", _loggedInUser);

        using var reader = await cmd.ExecuteReaderAsync();
        if (!reader.Read())
        {
            return null;
        }

        var user = new User
        {
            UserId = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2), // This is now storing the hashed password
        };
        return user;
    }

    public async Task<User?> Login(string username, string password)
    {
        // First, retrieve the user by username only (not by password anymore)
        var sql = "SELECT user_id, name, password FROM users WHERE name = @username";
        using var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@username", username);

        using var reader = await cmd.ExecuteReaderAsync();
        if (!reader.Read())
        {
            return null;
        }

        var storedHash = reader.GetString(2);

        // Verify the provided password against the stored hash
        try
        {
            if (!BCrypt.Net.BCrypt.Verify(password, storedHash))
            {
                return null; // Password doesn't match
            }
        }
        catch
        {
            return null; // If any error occurs during verification, fail securely
        }

        // If we get here, the password was correct
        var user = new User
        {
            UserId = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = storedHash, // Store the hash, not the plain password
        };

        _loggedInUser = user.UserId;
        return user;
    }

    public void Logout()
    {
        _loggedInUser = null;
    }

    public async Task<string> RegisterUser(string username, string password)
    {
        // Hash the password before storing it
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);

        User user = new User
        {
            UserId = Guid.NewGuid(),
            Name = username,
            Password = hashedPassword, // Store the hash, not the plain password
        };

        var sql =
            @"
            INSERT INTO users (user_id, name, password)
            VALUES (@user_id, @name, @password)";

        using var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@user_id", user.UserId);
        cmd.Parameters.AddWithValue("@name", user.Name);
        cmd.Parameters.AddWithValue("@password", user.Password);

        await cmd.ExecuteNonQueryAsync();
        _loggedInUser = user.UserId;
        return user.Name;
    }

    public async Task<bool> CheckUserExists(string username)
    {
        var sql = @"SELECT 1 FROM users WHERE name = @name";
        using var cmd = new NpgsqlCommand(sql, _connection);
        cmd.Parameters.AddWithValue("@name", username);

        using var reader = await cmd.ExecuteReaderAsync();
        return reader.Read(); // Returns true if user exists, false otherwise
    }
}
