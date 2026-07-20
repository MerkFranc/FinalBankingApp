using System.Data;

namespace BankingApp.Models;

public abstract class Account
{
    private decimal _balance;
    private readonly List<Transaction> _transactions = new();

    public static Guid GenerateAccountNumber() => Guid.NewGuid();

    public Guid AccountNumber { get; }
    public string OwnerName { get; }
    public DateTime DateOpened { get; }
    public AccountStatus Status { get; private set; }

    public decimal Balance => _balance;
    public IReadOnlyList<Transaction> Transactions => _transactions;


    public abstract AccountType Type { get; }
 

    protected Account(string ownerName)
    {
        
    }
}