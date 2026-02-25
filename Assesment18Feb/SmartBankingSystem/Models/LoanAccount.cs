using System;
using SmartBankingSystem.Exceptions;

namespace SmartBankingSystem.Models
{
    /// <summary>
    /// Loan account - cannot deposit, only repayment
    /// </summary>
    public class LoanAccount : BankAccount
    {
        public decimal LoanAmount { get; set; }
        public decimal InterestRate { get; set; }
        public int TenureMonths { get; set; }

        public LoanAccount(string accountNumber, string customerName, decimal loanAmount,
                          decimal interestRate = 0.10m, int tenureMonths = 12)
            : base(accountNumber, customerName, -loanAmount) // Negative balance represents loan
        {
            LoanAmount = loanAmount;
            InterestRate = interestRate;
            TenureMonths = tenureMonths;
        }

        public override void Deposit(decimal amount)
        {
            throw new InvalidTransactionException("Cannot deposit to loan account. Use Repay() method instead");
        }

        /// <summary>
        /// Repay loan amount
        /// </summary>
        public void Repay(decimal amount)
        {
            if (amount <= 0)
            {
                throw new ArgumentException("Repayment amount must be positive");
            }

            if (Balance + amount > 0)
            {
                throw new InvalidTransactionException("Repayment amount exceeds outstanding loan");
            }

            Balance += amount;
            AddTransaction($"Loan repayment: {amount:C} | Outstanding: {Math.Abs(Balance):C}");
        }

        public override void Withdraw(decimal amount)
        {
            throw new InvalidTransactionException("Cannot withdraw from loan account");
        }

        public override decimal CalculateInterest()
        {
            // Interest on outstanding loan (negative balance)
            decimal interest = Math.Abs(Balance) * InterestRate;
            Balance -= interest; // Increases loan amount
            AddTransaction($"Interest charged: {interest:C} | Outstanding: {Math.Abs(Balance):C}");
            return interest;
        }

        public decimal GetOutstandingAmount()
        {
            return Math.Abs(Balance);
        }
    }
}
