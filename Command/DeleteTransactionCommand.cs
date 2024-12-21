namespace FinanceApp_Databaser;

public class DeleteTransactionCommand : Command
{
    private readonly ITransactionService _transactionService;
    private readonly IMenuService _menuService;
    public static bool removing = false;

    public DeleteTransactionCommand(
        ConsoleKey triggerKey,
        IUserService userService,
        IMenuService menuService,
        ITransactionService transactionService
    )
        : base(triggerKey, userService)
    {
        _menuService = menuService;
        _transactionService = transactionService;
    }

    public override async Task Execute()
    {
        while (true)
        {
            SubMenu.Display(SubMenuType.DELETETRANSACTION);
            ConsoleKey inputKey = Console.ReadKey().Key;
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    removing = true;
                    break;
                case ConsoleKey.D2:
                    return;
                default:
                    continue;
            }

            await _menuService.GetMenu().ExecuteCommand(ConsoleKey.D4);
            Console.WriteLine("Hello!");
            break;
        }
        removing = false;
    }
}
