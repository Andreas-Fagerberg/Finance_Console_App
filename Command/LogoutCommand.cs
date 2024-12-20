namespace FinanceApp_Databaser;

public class LogoutCommand : Command
{
    private readonly IMenuService _menuService;
    private readonly ITransactionService _transactionService;

    public LogoutCommand(
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
            SubMenu.Display(SubMenuType.LogOut);
            ConsoleKeyInfo input = Console.ReadKey();
            switch (input.Key)
            {
                case ConsoleKey.D1:
                    _menuService.SetMenu(
                        new LoginMenu(userService, _menuService, _transactionService)
                    );
                    break;
                case ConsoleKey.D2:
                    return;
                default:
                    await Utilities.WaitForKeyAny("Please enter a valid option");
                    break;
            }
        }
    }
}
