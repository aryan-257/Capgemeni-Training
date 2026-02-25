using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem3.BankingRisk
{
    public class RiskMonitor
    {
        private SortedDictionary<int, List<Transaction>> transactions = new SortedDictionary<int, List<Transaction>>(Comparer<int>.Create((a, b) => b.CompareTo(a)));
        private const decimal TransactionLimit = 500000;

        public void AddTransaction(Transaction transaction)
        {
            if (transaction.Amount > TransactionLimit)
                throw new TransactionLimitExceededException($"Transaction exceeds limit of {TransactionLimit}");

            int riskScore = transaction.CalculateRiskScore();

            if (riskScore > 80)
                throw new FraudDetectedException($"High risk transaction detected! Risk Score: {riskScore}");

            if (!transactions.ContainsKey(riskScore))
                transactions[riskScore] = new List<Transaction>();

            transactions[riskScore].Add(transaction);
            Console.WriteLine($"Transaction {transaction.TransactionId} added with risk score {riskScore}");
        }

        public void DisplayTransactions()
        {
            Console.WriteLine("\n=== Transaction Risk Monitor ===");
            foreach (var kvp in transactions)
            {
                Console.WriteLine($"\nRisk Score {kvp.Key}:");
                foreach (var txn in kvp.Value)
                    Console.WriteLine($"  - {txn.TransactionId}: ₹{txn.Amount} from {txn.AccountNumber}");
            }
        }

        public List<Transaction> GetHighRiskTransactions()
        {
            return transactions.Where(kvp => kvp.Key > 50)
                              .SelectMany(kvp => kvp.Value)
                              .ToList();
        }
    }
}
