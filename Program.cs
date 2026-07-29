using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp;

class Program
{
    private static readonly BankService _bank = new BankService();

    static async Task Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.WriteLine("========================================");
            Console.WriteLine("           WELCOME TO C# BANK.          ");
            Console.WriteLine("========================================");
            Console.WriteLine("1. Create New Customer");
            Console.WriteLine("2. Open Account");
            Console.WriteLine("3. Deposit");
            Console.WriteLine("4. Withdraw");
            Console.WriteLine("5. Transfer Funds");
            Console.WriteLine("6. View Account Details");
            Console.WriteLine("7. Save Data to File");
            Console.WriteLine("8. View Saved File");
            Console.WriteLine("9. Exit");
            Console.WriteLine("========================================");
            Console.Write("Select an option (1-9): ");

            string choice = Console.ReadLine()?.Trim();
            Console.WriteLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        CreateCustomerUI();
                        break;
                    case "2":
                        OpenAccountUI();
                        break;
                    case "3":
                        DepositUI();
                        break;
                    case "4":
                        WithdrawUI();
                        break;
                    case "5":
                        TransferUI();
                        break;
                    case "6":
                        ViewCustomerDetailsUI();
                        break;
                    case "7":
                        await SaveDataUI();
                        break;
                    case "8":
                        await LoadDataUI();
                        break;
                    case "9":
                        running = false;
                        Console.WriteLine("Thank you for using C# Bank. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Press Enter to try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[ERROR] {ex.Message}");
            }

            if (running)
            {
                Console.WriteLine("\nPress Enter to return to the main menu...");
                Console.ReadLine();
            }
        }
    }

    private static void CreateCustomerUI()
    {
        string name = ReadRequired("Enter customer name: ");
        Customer customer = _bank.CreateCustomer(name);

        Console.WriteLine($"\nCustomer created successfully!");
        Console.WriteLine($"    ID:   {customer.CustomerId}");
        Console.WriteLine($"    Name: {customer.Name}");
    }

    private static void OpenAccountUI()
    {
        string customerId = ReadRequired("Enter customer ID: ");
        Customer customer = _bank.GetCustomer(customerId);

        Console.WriteLine("Account type:\n1 - Checking\n2 - Savings");
        string typeChoice = ReadRequired("Select 1 or 2: ");
        decimal initialDeposit = ReadDecimal("Initial deposit: ");

        BankAccount account;

        if (typeChoice == "1")
        {
            decimal overdraftLimit = ReadDecimal("Overdraft limit: ");
            account = customer.OpenCheckingAccount(initialDeposit, overdraftLimit);
        }
        else if (typeChoice == "2")
        {
            decimal interestRate = ReadDecimal("Interest rate (e.g 0.02 for 2%): ");
            account = customer.OpenSavingsAccount(initialDeposit, interestRate);
        }
        else
        {
            Console.WriteLine("Invalid account type.");
            return;
        }

        Console.WriteLine($"\nAccount opened!");
        Console.WriteLine($"    Number: {account.AccountNumber}");
        Console.WriteLine($"    Type:   {account.Type}");
        Console.WriteLine($"    Balance: {account.Balance:C}");
    }

    private static void DepositUI()
    {
        BankAccount account =
            SelectCustomerAccount("Enter customer ID: ");

        decimal amount = ReadDecimal("Deposit amount: ");
        account.Deposit(amount);

        Console.WriteLine(
            $"\nDeposit successful. New balance: {account.Balance:C}");
    }

    private static void WithdrawUI()
    {
        BankAccount account =
            SelectCustomerAccount("Enter customer ID: ");

        Console.WriteLine($"Current balance: {account.Balance:C}");

        decimal amount = ReadDecimal("Withdrawal amount: ");
        account.Withdraw(amount);

        Console.WriteLine(
            $"\nWithdrawal successful. New balance: {account.Balance:C}");
    }

    private static void TransferUI()
    {
        Console.WriteLine("Select the source account.");

        BankAccount source =
            SelectCustomerAccount("Enter source customer ID: ");

        Console.WriteLine("\nSelect the destination account.");

        BankAccount destination =
            SelectCustomerAccount("Enter destination customer ID: ");

        if (source.AccountNumber == destination.AccountNumber)
        {
            Console.WriteLine("You cannot transfer to the same account.");
            return;
        }

        decimal amount = ReadDecimal("Transfer amount: ");

        _bank.TransferFunds(
            source.AccountNumber,
            destination.AccountNumber,
            amount);

        Console.WriteLine("\nTransfer successful.");
    }

    private static void ViewCustomerDetailsUI()
    {
        string customerId = ReadRequired("Enter customer ID: ");
        Customer customer = _bank.GetCustomer(customerId);

        Console.WriteLine(
            $"\nCustomer: {customer.Name} ({customer.CustomerId})");

        if (customer.Accounts.Count == 0)
        {
            Console.WriteLine("No accounts on file.");
            return;
        }

        foreach (BankAccount account in customer.Accounts.Values)
        {
            Console.WriteLine(
                $"    {account.AccountNumber} [{account.Type}] " +
                $"Balance: {account.Balance:C}");
        }
    }

    private static async Task SaveDataUI()
    {
        string path = ReadRequired("Enter file path to save (e.g. bank.json): ");
        await _bank.SaveToFileAsync(path);
        Console.WriteLine($"\nData saved to {path}");
    }

    private static async Task LoadDataUI()
    {
        string path = ReadRequired("Enter file path to load (e.g. bank.json): ");
        BankSnapshot? snapshot = await _bank.LoadSnapshotAsync(path);

        if (snapshot is null)
        {
            Console.WriteLine("File is empty or invalid.");
            return;
        }

        Console.WriteLine($"\n Saved on: {snapshot.SaveAt}");
        foreach (CustomerSnapshot customer in snapshot.Customers)
        {
            Console.WriteLine($"    {customer.Name} ({customer.CustomerId})");
            foreach (AccountSnapshot account in customer.Accounts)
            {
                Console.WriteLine($"    {account.AccountNumber} [{account.Type}] Balance: {account.Balance:C}");
            }
        }
    }

    private static string ReadRequired(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();
            if (!string.IsNullOrWhiteSpace(input))
            {
                return input;
            }
            Console.WriteLine("This field cannot be empty. Please try again.");
        }
    }

    private static decimal ReadDecimal(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (decimal.TryParse(Console.ReadLine(), out decimal value))
            {
                return value;
            }
            Console.WriteLine("Please enter a valid number. Try again.");
        }
    }

    private static BankAccount SelectCustomerAccount(string customerPrompt)
    {
        string customerID = ReadRequired(customerPrompt);
        Customer customer = _bank.GetCustomer(customerID);

        if (customer.Accounts.Count == 0)
        {
            throw new InvalidOperationException("This customer has no accounts.");
        }

        Console.WriteLine($"\nAccounts for {customer.Name}:");

        List<BankAccount> accounts = customer.Accounts.Values.ToList();

        for (int i = 0; i < accounts.Count; i++)
        {
            BankAccount account = accounts[i];

            Console.WriteLine(
                $"{i + 1}. {account.AccountNumber} " +
                $"[{account.Type}] Balanace: {account.Balance:C}");
        }

        while (true)
        {
            Console.Write("Select an account: ");

            if (int.TryParse(Console.ReadLine(), out int choice) &&
                choice >= 1 &&
                choice <= accounts.Count)
            {
                return accounts[choice - 1];
            }

            Console.WriteLine("Invalid account selection");
        }
    }
}