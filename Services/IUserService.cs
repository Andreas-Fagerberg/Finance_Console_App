using Npgsql;

namespace FinanceApp_Databaser;

public interface IUserService
{
    Task<string> RegisterUser(string username, string password);
    Task<User?> Login(string username, string password);
    void Logout();
    Task<User?> GetLoggedInUser();
    Task<bool> CheckUserExists(string username);
}
