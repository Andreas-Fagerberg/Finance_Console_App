namespace FinanceApp_Databaser;

public class Transaction
{
    public Guid TransactionId { get; set; } = new Guid();
    public Guid UserId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int RefId { get; set; } // Used as reference when selecting wich transactions to delete.
}
