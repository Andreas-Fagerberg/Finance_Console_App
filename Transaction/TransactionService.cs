namespace FinanceApp_Databaser;

public interface ITransactionService
{
    Transaction Load();
    Transaction Save();
}
