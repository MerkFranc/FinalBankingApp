using BankingApp.Models;

namespace BankingApp.Services;

public class BankService
{
    private readonly Dictionary<string, Customer> _customers = new();

    public IReadOnlyDictionary<string, Customer> Customers => _customers;

    public Customer CreateCustomer(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Customer name cannot be empty.");
        }

        Customer customer = new Customer(name);
        _customers.Add(customer.CustomerId, customer);
        return customer;
    }

    public Customer GetCustomer(string customerId)
    {
        if (_customers.TryGetValue(customerId, out Customer? customer) && customer is not null)
        {
            return customer;
        }

        throw new KeyNotFoundException($"Customer with ID '{customerId}' not found.");
    }

    public BankAccount FindAccount(string accountNumber)
    {
        foreach (Customer customer in _customers.Values)
        {
            if (customer.Accounts.TryGetValue(accountNumber, out BankAccount? account) && account is not null)
            {
                return account;
            }
        }

        throw new KeyNotFoundException($"Account '{accountNumber}' not found.");
    }

    public void TransferFunds(string sourceAccountNumber, string destinationAccountNumber, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("Transfer amount must be positive.");
        }

        BankAccount source = FindAccount(sourceAccountNumber);
        BankAccount destination = FindAccount(destinationAccountNumber);

        source.Withdraw(amount);
        destination.Deposit(amount);
    }

}