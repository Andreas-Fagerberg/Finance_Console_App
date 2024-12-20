namespace FinanceApp_Databaser;

public interface ITransactionService
{
    Task<List<Transaction>?> Load(string dateType, string dateFilter);
    void Save(Transaction transaction);
    Task<decimal?> GetBalance(User user);
}
