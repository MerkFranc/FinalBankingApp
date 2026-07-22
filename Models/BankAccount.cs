using BankingApp.Enums;

namespace BankingApp.Models;

public abstract class BankAccount
{
    private decimal _balance;

    public string AccountNumber { get; }
    public string AccountHolderName { get; set; }
    public AccountType Type { get; }

    public decimal Balance
    {
        get
        {
            return _balance;
        }
        protected set
        {
            if (value < MinimumBalance)
            {
                throw new ArgumentException($"Balance cannot go below {MinimumBalance:C}.");
            }
            _balance = value;
        }
    }

    /// Defaults to 0; subclasses (e.g. accounts with overdraft) can override this.
    protected virtual decimal MinimumBalance => 0;

    public BankAccount(string accountHolderName, decimal initialDeposit, AccountType type)
    {
        AccountNumber = "ACC-" + Guid.NewGuid().ToString("N")[..8].ToUpper();
        AccountHolderName = accountHolderName;
        Type = type;
        Balance = initialDeposit;
    }

    public void Deposit(decimal amount)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Deposit amount must be positive.");
        }

        Balance += amount;
    }

    public virtual void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive.");
        }

        if (amount > Balance)
        {
            throw new InvalidOperationException("Insufficient funds.");
        }

        Balance -= amount;
    }
}