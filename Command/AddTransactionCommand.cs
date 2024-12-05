
namespace FinanceApp_Databaser;

public class AddTransactionCommand : Command
{
    public AddTransactionCommand(
        IUserService userService, 
        ITransactionService transactionService, 
        IMenuService menuService) 
    : base(ConsoleKey.D3, userService, transactionService, menuService)
    {
        
    }
    public override void Execute(ConsoleKey name)
    {
        throw new NotImplementedException();
    }
}
