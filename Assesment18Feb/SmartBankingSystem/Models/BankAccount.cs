using System;
using System.Collections.Generic;

namespace SmartBankingSystem.Models
{
    /// <summary>
    /// Abstract base class for all bank accounts
    /// </summary>
    public abstract class BankAccount
    {
        public string AccountNumber { get; set; }
        public string CustomerName { get; set; }
        public decimal Balance { get; protected set; }
        public List<string> TransactionHistory { get; private set; }
        public DateTime DateOpened { get; set; }

        public BankAccount(string accountNumber, string customerName, decimal initialBalance)
        {
            AccountNumber = accountNumber;
            CustomerName = customerName;
            Balance = initialBalance;
            TransactionHistory = new List<string>();
            DateOpened = DateTime.Now;
            AddTransaction($"Account opened with balance: {initialBalance:C}");
        }

        /// <summary>
        /// Deposit money into account
        /// </summary>
        public virtual void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Deposit amount must be positive");
            }

            Balance += amount;
            AddTransaction($"Deposited: {amount:C} | New Balance: {Balance:C}");
        }

        /// <summary>
        /// Withdraw money from account
        /// </summary>
        public abstract void Withdraw(decimal amount);

        /// <summary>
        /// Calculate interest for the account
        /// </summary>
        public abstract decimal CalculateInterest();

        /// <summary>
        /// Add transaction to history
        /// </summary>
        protected void AddTransaction(string transaction)
        {
            TransactionHistory.Add($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {transaction}");
        }

        public override string ToString()
        {
            return $"Account: {AccountNumber} | Customer: {CustomerName} | Balance: {Balance:C}";
        }
    }
}
