namespace FinanceApp_Databaser;

public class Transaction
{
    public Guid TransactionId { get; set; }
    public Guid UserId { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int RefId { get; set; } // Used as reference when selecting wich transactions to delete.

    public Transaction()
    {
        TransactionId = Guid.NewGuid();
    }

    public string StringConversion()
    {
        string transactionString = string.Empty;
        if (!string.IsNullOrEmpty(Description))
        {
            transactionString =
                "  "
                + RefId
                + new string(' ', 4 - RefId.ToString().Length)
                + "|  "
                + Description
                + new string(' ', 35 - Description.Length)
                + "|  "
                + Amount
                + new string(' ', 16 - Amount.ToString().Length)
                + " kr |  "
                + Date.ToString("yyyy-MM-dd HH:mm");
        }
        return transactionString;
    }

    public static void PrintHeader()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Clear();
        Console.WriteLine("Matched transactions \n");
        Console.WriteLine(
            " ID:  | Description:                        | Amount:              |Date: "
        );
        Console.ForegroundColor = ConsoleColor.White;
    }
}
