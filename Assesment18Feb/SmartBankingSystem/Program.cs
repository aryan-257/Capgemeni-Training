using System;
using SmartBankingSystem.Models;
using SmartBankingSystem.Services;
using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem
{
    class Program
    {
        private static BankingService bankingService = new BankingService();

        static void Main(string[] args)
        {
            Console.WriteLine("=== Smart Banking System ===\n");

            // Load sample data
            LoadSampleData();

            while (true)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                try
                {
                    switch (choice)
                    {
                        case "1":
                            CreateAccountMenu();
                            break;
                        case "2":
                            DepositMenu();
                            break;
                        case "3":
                            WithdrawMenu();
                            break;
                        case "4":
                            TransferMenu();
                            break;
                        case "5":
                            ViewAccountMenu();
                            break;
                        case "6":
                            ViewAllAccounts();
                            break;
                        case "7":
                            CalculateInterestMenu();
                            break;
                        case "8":
                            LINQQueriesMenu();
                            break;
                        case "9":
                            ViewTransactionHistory();
                            break;
                        case "10":
                            bankingService.DisplayStatistics();
                            break;
                        case "11":
                            Console.WriteLine("Thank you for using Smart Banking System!");
                            return;
                        default:
                            Console.WriteLine("Invalid option. Try again.");
                            break;
                    }
                }
                catch (InsufficientBalanceException ex)
                {
                    Console.WriteLine($"Insufficient Balance Error: {ex.Message}");
                }
                catch (MinimumBalanceException ex)
                {
                    Console.WriteLine($"Minimum Balance Error: {ex.Message}");
                }
                catch (InvalidTransactionException ex)
                {
                    Console.WriteLine($"Invalid Transaction Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n=== SMART BANKING SYSTEM MENU ===");
            Console.WriteLine("1.  Create New Account");
            Console.WriteLine("2.  Deposit Money");
            Console.WriteLine("3.  Withdraw Money");
            Console.WriteLine("4.  Transfer Money");
            Console.WriteLine("5.  View Account Details");
            Console.WriteLine("6.  View All Accounts");
            Console.WriteLine("7.  Calculate Interest");
            Console.WriteLine("8.  LINQ Queries");
            Console.WriteLine("9.  View Transaction History");
            Console.WriteLine("10. Bank Statistics");
            Console.WriteLine("11. Exit");
            Console.Write("\nEnter your choice: ");
        }

        static void CreateAccountMenu()
        {
            Console.WriteLine("\n=== Create New Account ===");
            Console.WriteLine("1. Savings Account");
            Console.WriteLine("2. Current Account");
            Console.WriteLine("3. Loan Account");
            Console.Write("Select account type: ");
            string type = Console.ReadLine();

            Console.Write("Account Number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Customer Name: ");
            string customerName = Console.ReadLine();
            Console.Write("Initial Balance/Loan Amount: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            BankAccount account = null;

            switch (type)
            {
                case "1":
                    account = new SavingsAccount(accountNumber, customerName, amount);
                    break;
                case "2":
                    Console.Write("Overdraft Limit (default 5000): ");
                    string overdraftInput = Console.ReadLine();
                    decimal overdraft = string.IsNullOrWhiteSpace(overdraftInput) ? 5000 : decimal.Parse(overdraftInput);
                    account = new CurrentAccount(accountNumber, customerName, amount, overdraft);
                    break;
                case "3":
                    Console.Write("Interest Rate (default 0.10): ");
                    string rateInput = Console.ReadLine();
                    decimal rate = string.IsNullOrWhiteSpace(rateInput) ? 0.10m : decimal.Parse(rateInput);
                    Console.Write("Tenure in Months (default 12): ");
                    string tenureInput = Console.ReadLine();
                    int tenure = string.IsNullOrWhiteSpace(tenureInput) ? 12 : int.Parse(tenureInput);
                    account = new LoanAccount(accountNumber, customerName, amount, rate, tenure);
                    break;
                default:
                    Console.WriteLine("Invalid account type");
                    return;
            }

            bankingService.AddAccount(account);
            Console.WriteLine("Account created successfully!");
        }

        static void DepositMenu()
        {
            Console.Write("\nEnter Account Number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Enter Amount to Deposit: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            var account = bankingService.FindAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            account.Deposit(amount);
            Console.WriteLine($"Deposit successful! New Balance: {account.Balance:C}");
        }

        static void WithdrawMenu()
        {
            Console.Write("\nEnter Account Number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Enter Amount to Withdraw: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            var account = bankingService.FindAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            account.Withdraw(amount);
            Console.WriteLine($"Withdrawal successful! New Balance: {account.Balance:C}");
        }

        static void TransferMenu()
        {
            Console.Write("\nFrom Account Number: ");
            string fromAccount = Console.ReadLine();
            Console.Write("To Account Number: ");
            string toAccount = Console.ReadLine();
            Console.Write("Amount to Transfer: ");
            decimal amount = decimal.Parse(Console.ReadLine());

            bankingService.TransferMoney(fromAccount, toAccount, amount);
        }

        static void ViewAccountMenu()
        {
            Console.Write("\nEnter Account Number: ");
            string accountNumber = Console.ReadLine();

            var account = bankingService.FindAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            Console.WriteLine("\n=== Account Details ===");
            Console.WriteLine($"Account Number: {account.AccountNumber}");
            Console.WriteLine($"Customer Name: {account.CustomerName}");
            Console.WriteLine($"Account Type: {account.GetType().Name}");
            Console.WriteLine($"Balance: {account.Balance:C}");
            Console.WriteLine($"Date Opened: {account.DateOpened:yyyy-MM-dd}");

            if (account is SavingsAccount savings)
            {
                Console.WriteLine($"Minimum Balance: {savings.MinimumBalance:C}");
                Console.WriteLine($"Interest Rate: {savings.InterestRate:P}");
            }
            else if (account is CurrentAccount current)
            {
                Console.WriteLine($"Overdraft Limit: {current.OverdraftLimit:C}");
                Console.WriteLine($"Interest Rate: {current.InterestRate:P}");
            }
            else if (account is LoanAccount loan)
            {
                Console.WriteLine($"Loan Amount: {loan.LoanAmount:C}");
                Console.WriteLine($"Outstanding: {loan.GetOutstandingAmount():C}");
                Console.WriteLine($"Interest Rate: {loan.InterestRate:P}");
                Console.WriteLine($"Tenure: {loan.TenureMonths} months");
            }
        }

        static void ViewAllAccounts()
        {
            var accounts = bankingService.GetAllAccounts();
            if (accounts.Count == 0)
            {
                Console.WriteLine("\nNo accounts found.");
                return;
            }

            Console.WriteLine("\n=== All Accounts ===");
            foreach (var account in accounts)
            {
                Console.WriteLine($"{account.GetType().Name} - {account}");
            }
        }

        static void CalculateInterestMenu()
        {
            Console.WriteLine("\n=== Calculate Interest ===");
            Console.WriteLine("1. Calculate for specific account");
            Console.WriteLine("2. Calculate for all accounts");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("Enter Account Number: ");
                string accountNumber = Console.ReadLine();
                var account = bankingService.FindAccount(accountNumber);
                if (account == null)
                {
                    Console.WriteLine("Account not found!");
                    return;
                }

                decimal interest = account.CalculateInterest();
                Console.WriteLine($"Interest calculated: {interest:C}");
                Console.WriteLine($"New Balance: {account.Balance:C}");
            }
            else if (choice == "2")
            {
                bankingService.CalculateInterestForAll();
            }
        }

        static void LINQQueriesMenu()
        {
            Console.WriteLine("\n=== LINQ Queries ===");
            Console.WriteLine("1. Accounts with balance > 50,000");
            Console.WriteLine("2. Total Bank Balance");
            Console.WriteLine("3. Top 3 Highest Balance Accounts");
            Console.WriteLine("4. Group Accounts by Type");
            Console.WriteLine("5. Customers whose name starts with 'R'");
            Console.Write("Choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    var highBalance = bankingService.GetHighBalanceAccounts();
                    Console.WriteLine($"\nAccounts with balance > 50,000: {highBalance.Count}");
                    foreach (var acc in highBalance)
                    {
                        Console.WriteLine(acc);
                    }
                    break;

                case "2":
                    decimal total = bankingService.GetTotalBankBalance();
                    Console.WriteLine($"\nTotal Bank Balance: {total:C}");
                    break;

                case "3":
                    var top3 = bankingService.GetTop3HighestBalanceAccounts();
                    Console.WriteLine("\nTop 3 Highest Balance Accounts:");
                    for (int i = 0; i < top3.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {top3[i]}");
                    }
                    break;

                case "4":
                    var grouped = bankingService.GroupAccountsByType();
                    Console.WriteLine("\nAccounts Grouped by Type:");
                    foreach (var group in grouped)
                    {
                        Console.WriteLine($"\n{group.Key}: {group.Value.Count} accounts");
                        foreach (var acc in group.Value)
                        {
                            Console.WriteLine($"  {acc}");
                        }
                    }
                    break;

                case "5":
                    var rCustomers = bankingService.GetCustomersByNamePrefix("R");
                    Console.WriteLine($"\nCustomers whose name starts with 'R': {rCustomers.Count}");
                    foreach (var acc in rCustomers)
                    {
                        Console.WriteLine(acc);
                    }
                    break;

                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
        }

        static void ViewTransactionHistory()
        {
            Console.Write("\nEnter Account Number: ");
            string accountNumber = Console.ReadLine();

            var account = bankingService.FindAccount(accountNumber);
            if (account == null)
            {
                Console.WriteLine("Account not found!");
                return;
            }

            Console.WriteLine($"\n=== Transaction History for {accountNumber} ===");
            foreach (var transaction in account.TransactionHistory)
            {
                Console.WriteLine(transaction);
            }
        }

        static void LoadSampleData()
        {
            try
            {
                // Savings Accounts
                bankingService.AddAccount(new SavingsAccount("SA001", "Rajesh Kumar", 75000));
                bankingService.AddAccount(new SavingsAccount("SA002", "Priya Sharma", 45000));
                bankingService.AddAccount(new SavingsAccount("SA003", "Ramesh Patel", 120000));

                // Current Accounts
                bankingService.AddAccount(new CurrentAccount("CA001", "Amit Singh", 60000, 10000));
                bankingService.AddAccount(new CurrentAccount("CA002", "Ravi Verma", 85000, 15000));

                // Loan Accounts
                bankingService.AddAccount(new LoanAccount("LA001", "Sunita Reddy", 500000, 0.12m, 24));
                bankingService.AddAccount(new LoanAccount("LA002", "Rohan Mehta", 300000, 0.10m, 12));

                Console.WriteLine("Sample data loaded successfully!\n");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading sample data: {ex.Message}");
            }
        }
    }
}
