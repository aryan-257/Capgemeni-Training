using System;
using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.Models
{
    /// <summary>
    /// Savings account with minimum balance requirement
    /// </summary>
    public class SavingsAccount : BankAccount
    {
        public decimal MinimumBalance { get; set; }
        public decimal InterestRate { get; set; }

        public SavingsAccount(string accountNumber, string customerName, decimal initialBalance, 
                             decimal minimumBalance = 1000, decimal interestRate = 0.04m)
            : base(accountNumber, customerName, initialBalance)
        {
            MinimumBalance = minimumBalance;
            InterestRate = interestRate;

            if (initialBalance < minimumBalance)
            {
                throw new MinimumBalanceException($"Initial balance must be at least {minimumBalance:C}");
            }
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive");
            }

            if (Balance - amount < MinimumBalance)
            {
                throw new MinimumBalanceException($"Cannot withdraw. Minimum balance of {MinimumBalance:C} must be maintained");
            }

            Balance -= amount;
            AddTransaction($"Withdrawn: {amount:C} | New Balance: {Balance:C}");
        }

        public override decimal CalculateInterest()
        {
            decimal interest = Balance * InterestRate;
            Balance += interest;
            AddTransaction($"Interest credited: {interest:C} | New Balance: {Balance:C}");
            return interest;
        }
    }
}
