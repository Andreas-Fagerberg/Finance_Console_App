namespace FinanceApp_Databaser;

public interface ITransactionService
{
    Task<List<Transaction>?> Load(string date, string sql);
    Task Save(Transaction transaction);
    Task<decimal?> GetBalance(User user);
}
