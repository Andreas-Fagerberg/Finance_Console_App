namespace FinanceApp_Databaser;

public interface ITransactionService
{
    Task<List<Transaction>?> Load(DateType dateType, List<string> dateInput);
    Task Save(Transaction transaction);
    Task<bool> Delete(Transaction transaction);
    Task<decimal?> GetBalance(User user);
    Task<List<Transaction>?> GetCurrentTransactions();
}
