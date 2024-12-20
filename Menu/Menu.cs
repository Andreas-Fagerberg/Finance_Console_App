namespace FinanceApp_Databaser;

public abstract class Menu
{
    private List<Command> commands = new List<Command>();

    public void AddCommand(Command command)
    {
        this.commands.Add(command);
    }

    public void ExecuteCommand(ConsoleKey inputCommand)
    {
        try
        {
            if (inputCommand.Equals(ConsoleKey.D5)) { }
            foreach (Command command in commands)
            {
                if (command.TriggerKey.Equals(inputCommand))
                {
                    command.Execute();
                    return;
                }
            }
        }
        catch { }
        throw new ArgumentException("Command not found.");
    }

    public abstract void Display();
}
