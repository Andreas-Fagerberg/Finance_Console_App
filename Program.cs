using Npgsql;

namespace FinanceApp_Databaser;


class Program
{
    static async Task Main(string[] args)
    {
        
        DatabaseService<NpgsqlConnection> postgresDatabaseService = 
        new PostgresDatabaseService();
        
        NpgsqlConnection connection = 
        await postgresDatabaseService.SetupDatabase();

        IUserService userService = 
        new PostgresUserService(connection);

        // TODO: Implement TransactionService and PostgresTransactionService
        ITransactionService transactionService = 
        new PostgresTransactionService(userService, connection);

        // TODO: Implement MenuService and Create Menus.
        // IMenuService menuService = 
        // new SimpleMenuService();
        
        

    }
}
