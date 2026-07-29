using BankingApp.Enums;

namespace BankingApp.Models;

public class Transaction
{
    public TransactionType Type { get; }
    public decimal Amount { get; }
    public DateTime Timestamp { get; }

    public Transaction(TransactionType type, decimal amount)
    {
        Type = type;
        Amount = amount;
        Timestamp = DateTime.Now;
    }
}