using System;

namespace BankTransactionApp
{
    public class BankAccount
    {
        private long balance; // Use long to handle large transactions safely

        public BankAccount(long initialBalance)
        {
            balance = initialBalance;
        }

        // Process an array of transactions
        public void ProcessTransactions(long[] transactions)
        {
            foreach (var t in transactions)
            {
                if (t >= 0)
                {
                    Deposit(t);
                }
                else
                {
                    Withdraw(-t); // t is negative, withdraw amount
                }
            }
        }

        private void Deposit(long amount)
        {
            balance += amount;
        }

        private void Withdraw(long amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
            }
            // If not enough balance, ignore
        }

        public long GetBalance()
        {
            return balance;
        }
    }
}
