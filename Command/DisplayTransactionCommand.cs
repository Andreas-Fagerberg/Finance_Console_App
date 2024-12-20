

namespace FinanceApp_Databaser;

public class DisplayTransactionCommand : Command
{
    public DisplayTransactionCommand(ConsoleKey triggerKey, IUserService userService, ITransactionService transactionService, IMenuService menuService) : base(triggerKey, userService)
    {
        men
    }

    public override Task Execute()
    {
        throw new NotImplementedException();
    }
}
