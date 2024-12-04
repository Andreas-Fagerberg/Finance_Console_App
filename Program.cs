using Npgsql;

namespace FinanceApp_Databaser;


class Program
{
    static async Task Main(string[] args)
    {
        var postgresDatabaseService = new PostgresDatabaseService();
        NpgsqlConnection connection = await postgresDatabaseService.SetupDatabase();
         
    }
}
