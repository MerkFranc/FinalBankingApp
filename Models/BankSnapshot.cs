namespace BankingApp.Models;

public record AccountSnapshot(string AccountNumber, string Type, decimal Balance);
public record CustomerSnapshot(string CustomerId, string Name, List<AccountSnapshot> Accounts);
public record BankSnapshot(DateTime SaveAt, List<CustomerSnapshot> Customers);