namespace FinanceApp_Databaser;

public class LogoutCommand : Command
{
    private readonly IMenuService _menuService;
    private readonly ITransactionService _transactionService;
    private readonly List<string> _menuContent;

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
        _menuContent = new List<string> { "Log out current user.", "Return to previous menu" };
    }

    public override Task Execute()
    {
        while (true)
        {
            Console.Clear();
            SubMenu.Display("|  LOG OUT  |", _menuContent);
            ConsoleKey inputKey = Console.ReadKey(true).Key;
            switch (inputKey)
            {
                case ConsoleKey.D1:
                    _menuService.SetMenu(
                        new InitialMenu(userService, _menuService, _transactionService)
                    );
                    return Task.CompletedTask;

                case ConsoleKey.D2:
                    return Task.CompletedTask;

                default:
                    Utilities.WaitForKeyAny("Please enter a valid option");
                    break;
            }
        }
    }
}
