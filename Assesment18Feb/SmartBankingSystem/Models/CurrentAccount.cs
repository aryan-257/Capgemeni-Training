using System;
using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.Models
{
    /// <summary>
    /// Current account with overdraft facility
    /// </summary>
    public class CurrentAccount : BankAccount
    {
        public decimal OverdraftLimit { get; set; }
        public decimal InterestRate { get; set; }

        public CurrentAccount(string accountNumber, string customerName, decimal initialBalance,
                             decimal overdraftLimit = 5000, decimal interestRate = 0.02m)
            : base(accountNumber, customerName, initialBalance)
        {
            OverdraftLimit = overdraftLimit;
            InterestRate = interestRate;
        }

        public override void Withdraw(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Withdrawal amount must be positive");
            }

            if (Balance - amount < -OverdraftLimit)
            {
                throw new InsufficientBalanceException($"Cannot withdraw. Overdraft limit of {OverdraftLimit:C} exceeded");
            }

            Balance -= amount;
            AddTransaction($"Withdrawn: {amount:C} | New Balance: {Balance:C}");
        }

        public override decimal CalculateInterest()
        {
            // Interest only on positive balance
            if (Balance > 0)
            {
                decimal interest = Balance * InterestRate;
                Balance += interest;
                AddTransaction($"Interest credited: {interest:C} | New Balance: {Balance:C}");
                return interest;
            }
            return 0;
        }
    }
}
