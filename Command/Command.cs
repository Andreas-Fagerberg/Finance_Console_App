namespace FinanceApp_Databaser;

public abstract class Command
{
    public ConsoleKey TriggerKey { get; init; }
    protected IUserService userService;

    public Command(ConsoleKey triggerKey, IUserService userService)
    {
        this.TriggerKey = triggerKey;
        this.userService = userService;
    }

    public abstract void Execute(ConsoleKey TriggerKey);
}
