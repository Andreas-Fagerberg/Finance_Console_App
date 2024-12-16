namespace FinanceApp_Databaser;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public string? Title { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public int Ref_Id { get; set; }
}
