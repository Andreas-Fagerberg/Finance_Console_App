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

    // Default to async Task (but will be overridden in sync commands)
    /*
    Sync commands need to return Task.CompletedTask with this method.
    But allows for using the same interface for all commands.
    */
    public virtual Task Execute()
    {
        return Task.CompletedTask;
    }
}
