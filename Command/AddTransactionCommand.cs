﻿namespace FinanceApp_Databaser;

public class AddTransactionCommand : Command
{
    public AddTransactionCommand(DependencyContainer dependencyContainer)
        : base(ConsoleKey.D3, dependencyContainer) { }

    public override void Execute(ConsoleKey name)
    {
        throw new NotImplementedException();
    }
}
