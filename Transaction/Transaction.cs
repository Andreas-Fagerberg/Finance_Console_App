﻿namespace FinanceApp_Databaser;

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
                + new string((char)32, 4 - RefId.ToString().Length)
                + "|  "
                + Description
                + new string((char)32, 30 - Description.Length)
                + "|  "
                + Amount
                + new string((char)32, 20 - Amount.ToString().Length)
                + "|  "
                + Date.ToString("yyyy-MM-dd HH:mm");
        }
        return transactionString;
    }

    public static void PrintHeader()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.Clear();
        Console.WriteLine("Matched transactions \n");
        Console.WriteLine(" ID:  | Description:                   | Amount:              |Date: ");
        Console.ForegroundColor = ConsoleColor.White;
    }
}
