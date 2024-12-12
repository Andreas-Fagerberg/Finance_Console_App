using Npgsql;

namespace FinanceApp_Databaser;

public class PostgresUserService : IUserService
{
    private NpgsqlConnection connection;

    public PostgresUserService(NpgsqlConnection connection)
    {
        this.connection = connection;
    }

    public User? GetLoggedInUser()
    {
        throw new NotImplementedException();
    }

    public async Task<User?> Login(string username, string password)
    {
        var sql = "SELECT * FROM users WHERE name = @username AND password = @password";
        using var cmd = new NpgsqlCommand(sql, this.connection);
        cmd.Parameters.AddWithValue("@username", username);
        cmd.Parameters.AddWithValue("@password", password);

        using var reader = await cmd.ExecuteReaderAsync();

        if (!reader.Read())
        {
            return null;
        }
        var user = new User
        {
            Id = reader.GetGuid(0),
            Name = reader.GetString(1),
            Password = reader.GetString(2),
        };
        return user;
    }

    public void Logout()
    {
        throw new NotImplementedException();
    }

    public User RegisterUser(string username, string password)
    {
        throw new NotImplementedException();
    }
}
