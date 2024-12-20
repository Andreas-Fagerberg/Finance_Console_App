namespace FinanceApp_Databaser;

public abstract class Menu
{
    private List<Command> commands = new List<Command>();

    public void AddCommand(Command command)
    {
        this.commands.Add(command);
    }

    public async Task ExecuteCommand(ConsoleKey inputCommand)
    {
        if (inputCommand.Equals(ConsoleKey.D5)) { }
        foreach (Command command in commands)
        {
            if (command.TriggerKey.Equals(inputCommand))
            {
                await command.Execute();
                return;
            }
        }
        throw new Exception("Command not found.");
    }

    public abstract void Display();
}
