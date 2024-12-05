using Npgsql;

namespace FinanceApp_Databaser;

public interface IUserService
{
    User RegisterUser(string username, string password);
    User? Login(string username, string password);
    void Logout();
    User? GetLoggedInUser();
}
