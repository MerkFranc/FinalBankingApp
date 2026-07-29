using BankingApp.Enums;

namespace BankingApp.Models;


public class SavingsAccount : BankAccount
{
    public decimal InterestRate { get; set; }

    public SavingsAccount(string accountHolderName, decimal initialDeposit, decimal interestRate)
        : base(accountHolderName, initialDeposit, AccountType.Savings)
    {
        InterestRate = interestRate;
    }

    public void ApplyInterest()
    {
        decimal interest = Balance * InterestRate;
        AddToBalance(interest, TransactionType.Interest);
    }
}