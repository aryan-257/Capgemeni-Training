using System;

namespace Problem3.BankingRisk
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 3: Banking Transaction Risk Monitoring ===\n");
            RiskMonitor monitor = new RiskMonitor();

            try
            {
                monitor.AddTransaction(new OnlineTransaction("T001", 5000, "ACC001", "192.168.1.1", "DEV001"));
                monitor.AddTransaction(new OnlineTransaction("T002", 75000, "ACC002", "203.45.67.89", "DEV002"));
                monitor.AddTransaction(new WireTransfer("T003", 150000, "ACC003", "USA", "SWIFT001"));
                monitor.AddTransaction(new WireTransfer("T004", 25000, "ACC004", "India", "SWIFT002"));

                monitor.DisplayTransactions();

                Console.WriteLine("\n--- High Risk Transactions ---");
                var highRisk = monitor.GetHighRiskTransactions();
                foreach (var txn in highRisk)
                    Console.WriteLine($"  {txn.TransactionId}: ₹{txn.Amount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
