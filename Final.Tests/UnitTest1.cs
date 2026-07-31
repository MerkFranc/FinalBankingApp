using BankingApp.Models;

namespace Final.Tests;

public class UnitTest1
{
    [Fact]
    public void Deposit_IncreasesBalance()
    {
        var account = new SavingsAccount("Alice", 100m, 0.01m);

        account.Deposit(50m);

        Assert.Equal(150m, account.Balance);
    }

    [Fact]
    public void Deposit_NegativeAmount_Throws()
    {
        var account = new SavingsAccount("Alice", 100m, 0.01m);

        Assert.Throws<ArgumentException>(() => account.Deposit(-10m));
    }

    [Fact]
    public void Withdraw_DecreasesBalance()
    {
        var account = new SavingsAccount("Alice", 100m, 0.01m);

        account.Withdraw(40m);

        Assert.Equal(60m, account.Balance);
    }

    [Fact]
    public void Withdraw_MoreThanBalance_ThrowsInsufficientFunds()
    {
        var account = new SavingsAccount("Alice", 100m, 0.01m);

        Assert.Throws<InvalidOperationException>(() => account.Withdraw(200m));
    }

    [Fact]
    public void ApplyInterest_AddsInterestToBalance()
    {
        var account = new SavingsAccount("Alice", 1000m, 0.05m);

        account.ApplyInterest();

        Assert.Equal(1050m, account.Balance);
    }

    [Fact]
    public void CheckingAccount_CanWithdrawIntoOverdraft()
    {
        var account = new CheckingAccount("Bob", 100m, 50m);

        account.Withdraw(130m);

        Assert.Equal(-30m, account.Balance);
    }

    [Fact]
    public void CheckingAccount_WithdrawBeyondOverdraft_Throws()
    {
        var account = new CheckingAccount("Bob", 100m, 50m);

        Assert.Throws<InvalidOperationException>(() => account.Withdraw(200m));
    }

    [Fact]
    public void History_RecordsEachTransaction()
    {
        var account = new SavingsAccount("Alice", 100m, 0.01m);

        account.Deposit(20m);
        account.Withdraw(10m);

        Assert.Equal(2, account.History.Count);
    }
}
