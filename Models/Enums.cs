namespace BankingApp.Models;

public enum AccountType
{
    Checking,
    Savings,
    Business
}

public enum TransactionType
{
    Deposit,
    Withdrawal,
    Transfer,
    Fee,
    Interest
}

public enum AccountStatus
{
    Active,
    Frozen,
    Closed
}