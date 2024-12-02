namespace FinanceApp_Databaser;

public class Transaction
{
    public string? Title { get; set; }
    public decimal Amount { get; set; }
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public byte Id { get; set; }
}
