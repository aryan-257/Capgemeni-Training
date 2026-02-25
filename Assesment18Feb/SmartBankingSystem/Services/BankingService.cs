using System;
using System.Collections.Generic;
using System.Linq;
using SmartBankingSystem.Models;
using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.Services
{
    /// <summary>
    /// Main banking service with all operations
    /// </summary>
    public class BankingService
    {
        private List<BankAccount> accounts;

        public BankingService()
        {
            accounts = new List<BankAccount>();
        }

        /// <summary>
        /// Add new account
        /// </summary>
        public void AddAccount(BankAccount account)
        {
            if (accounts.Any(a => a.AccountNumber == account.AccountNumber))
            {
                throw new InvalidTransactionException("Account number already exists");
            }
            accounts.Add(account);
        }

        /// <summary>
        /// Find account by account number
        /// </summary>
        public BankAccount FindAccount(string accountNumber)
        {
            return accounts.FirstOrDefault(a => a.AccountNumber == accountNumber);
        }

        /// <summary>
        /// Transfer money between accounts
        /// </summary>
        public void TransferMoney(string fromAccountNumber, string toAccountNumber, decimal amount)
        {
            var fromAccount = FindAccount(fromAccountNumber);
            var toAccount = FindAccount(toAccountNumber);

            if (fromAccount == null || toAccount == null)
            {
                throw new InvalidTransactionException("One or both accounts not found");
            }

            if (fromAccount is LoanAccount)
            {
                throw new InvalidTransactionException("Cannot transfer from loan account");
            }

            if (toAccount is LoanAccount)
            {
                throw new InvalidTransactionException("Cannot transfer to loan account");
            }

            // Withdraw from source
            fromAccount.Withdraw(amount);

            try
            {
                // Deposit to destination
                toAccount.Deposit(amount);
            }
            catch
            {
                // Rollback if deposit fails
                fromAccount.Deposit(amount);
                throw;
            }

            Console.WriteLine($"Transfer successful: {amount:C} from {fromAccountNumber} to {toAccountNumber}");
        }

        /// <summary>
        /// Get all accounts
        /// </summary>
        public List<BankAccount> GetAllAccounts()
        {
            return accounts;
        }

        // ============ LINQ Queries ============

        /// <summary>
        /// Get accounts with balance > 50,000
        /// </summary>
        public List<BankAccount> GetHighBalanceAccounts()
        {
            return accounts.Where(a => a.Balance > 50000).ToList();
        }

        /// <summary>
        /// Get total bank balance
        /// </summary>
        public decimal GetTotalBankBalance()
        {
            return accounts.Sum(a => a.Balance);
        }

        /// <summary>
        /// Get top 3 highest balance accounts
        /// </summary>
        public List<BankAccount> GetTop3HighestBalanceAccounts()
        {
            return accounts.OrderByDescending(a => a.Balance).Take(3).ToList();
        }

        /// <summary>
        /// Group accounts by account type
        /// </summary>
        public Dictionary<string, List<BankAccount>> GroupAccountsByType()
        {
            return accounts.GroupBy(a => a.GetType().Name)
                          .ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Find customers whose name starts with specific letter
        /// </summary>
        public List<BankAccount> GetCustomersByNamePrefix(string prefix)
        {
            return accounts.Where(a => a.CustomerName.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
                          .ToList();
        }

        /// <summary>
        /// Calculate interest for all accounts
        /// </summary>
        public void CalculateInterestForAll()
        {
            foreach (var account in accounts)
            {
                try
                {
                    decimal interest = account.CalculateInterest();
                    Console.WriteLine($"Account {account.AccountNumber}: Interest = {interest:C}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error calculating interest for {account.AccountNumber}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Get account statistics
        /// </summary>
        public void DisplayStatistics()
        {
            Console.WriteLine("\n=== Bank Statistics ===");
            Console.WriteLine($"Total Accounts: {accounts.Count}");
            Console.WriteLine($"Total Balance: {GetTotalBankBalance():C}");
            Console.WriteLine($"Average Balance: {(accounts.Count > 0 ? accounts.Average(a => a.Balance) : 0):C}");
            Console.WriteLine($"Savings Accounts: {accounts.OfType<SavingsAccount>().Count()}");
            Console.WriteLine($"Current Accounts: {accounts.OfType<CurrentAccount>().Count()}");
            Console.WriteLine($"Loan Accounts: {accounts.OfType<LoanAccount>().Count()}");
        }
    }
}
