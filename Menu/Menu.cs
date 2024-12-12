namespace FinanceApp_Databaser;

public abstract class Menu
{
    private List<Command> commands = new List<Command>();

    protected DependencyContainer? dependencyContainer;

    public void AddCommand(Command command)
    {
        this.commands.Add(command);
    }

    public void ExecuteCommand(ConsoleKey inputCommand)
    {
        foreach (Command command in commands)
        {
            if (command.TriggerKey.Equals(inputCommand))
            {
                command.Execute(inputCommand);
                return;
            }
        }

        throw new ArgumentException("Command not found.");
    }

    public abstract void Display();
}
