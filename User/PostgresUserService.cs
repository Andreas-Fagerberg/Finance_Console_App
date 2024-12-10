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

    public User? Login(string username, string password)
    {
        throw new NotImplementedException();
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
