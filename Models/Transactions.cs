namespace BankingApp.Models;

public class Transaction
{
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public DateTime Timestamp { get; }
    public string? Description { get; }

    public Transaction(TransactionType type, decimal amount, string? description = null)
    {
        if (amount < 0)
            throw new ArgumentException("Transaction amuont cannot be negative.", nameof(amount));

        Type = type;
        Amount = amount;
        Timestamp = DateTime.UtcNow;
        Description = description;
    }

    public override string ToString() => 
        $"{Timestamp: yyyy-MM-dd HH:mm:ss} | {Type,-10} | {Amount,10:C} | {Description}";

}