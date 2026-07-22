using System.Net.Quic;
using BankingApp.Models;
using BankingApp.Services;

namespace BankingApp;

class Program
{
    private static readonly BankService _bank = new BankService();

    static void Main(string[] args)
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
            Console.WriteLine("6. View Customer Details");
            Console.WriteLine("7. Exit");
            Console.WriteLine("========================================");
            Console.Write("Select an option (1-7): ");

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
        
    }

    private static void OpenAccountUI()
    {
        
    }

    private static void DepositUI()
    {
        
    }

    private static void WithdrawUI()
    {
        
    }

    private static void TransferUI()
    {
        
    }

    private static void ViewCustomerDetailsUI()
    {
        
    }
}
