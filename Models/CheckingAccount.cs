using BankingApp.Enums;

namespace BankingApp.Models;

public class CheckingAccount : BankAccount
{
    private decimal _overdraftLimit;

    public decimal OverdraftLimit
    {
        get => _overdraftLimit;
        set
        {
            if (value < 0)
            {
                throw new ArgumentException("Overdraft limit cannot be negative.");
            }
            _overdraftLimit = value;
        }
    }

    public CheckingAccount(string accountHolderName, decimal initialDeposit, decimal overdraftLimit)
        : base(accountHolderName, initialDeposit, AccountType.Checking)
    {
        OverdraftLimit = overdraftLimit;
    }

    
    /// Checking accounts may run negative down to -OverdraftLimit.
    protected override decimal MinimumBalance => -OverdraftLimit;

    public override void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Withdrawal amount must be positive.");
        }

        if (amount > Balance + OverdraftLimit)
        {
            throw new InvalidOperationException("Withdrawal exceeds overdraft limit.");
        }

        // The base setter now permits this because MinimumBalance is -OverdraftLimit.
        Balance -= amount;
    }
}