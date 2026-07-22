namespace BankingApp.Models;

public class Customer
{
    private readonly Dictionary<string, BankAccount> _accounts;

    public string CustomerId { get; }
    public string Name { get; set; }
    public IReadOnlyDictionary<string, BankAccount> Accounts => _accounts;

    public Customer(string name)
    {
        CustomerId = "Cust-" + Guid.NewGuid().ToString("N")[..6].ToUpper();
        Name = name;
        _accounts = new Dictionary<string, BankAccount>();
    }

    public SavingsAccount OpenSavingsAccount(decimal initialDeposit, decimal interestRate)
    {
        SavingsAccount account = new SavingsAccount(Name, initialDeposit, interestRate); 

        _accounts.Add(account.AccountNumber, account);
        return account;
    }

    public CheckingAccount OpenCheckingAccount(decimal initialDeposit, decimal overdraftLimit)
    {
        CheckingAccount account = new CheckingAccount(Name, initialDeposit, overdraftLimit);

        _accounts.Add(account.AccountNumber, account);
        return account;
    }

    public BankAccount GetAccount(string accountNumber)
    {
        if (_accounts.TryGetValue(accountNumber, out BankAccount? account))
        {
            return account;
        }

        throw new KeyNotFoundException($"No account found with number: {accountNumber}");
    }


}