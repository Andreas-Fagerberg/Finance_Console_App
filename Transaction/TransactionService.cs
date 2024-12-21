namespace FinanceApp_Databaser;

public interface ITransactionService
{
    Task<List<Transaction>?> Load(DateType dateType, List<string> dateInput);
    Task Save(Transaction transaction);
    Task<decimal?> GetBalance(User user);
}
