using System;

namespace Problem3.BankingRisk
{
    public abstract class Transaction
    {
        public string TransactionId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string AccountNumber { get; set; }

        protected Transaction(string id, decimal amount, string account)
        {
            if (amount < 0)
                throw new NegativeAmountException("Transaction amount cannot be negative");

            TransactionId = id;
            Amount = amount;
            AccountNumber = account;
            Timestamp = DateTime.Now;
        }

        public abstract int CalculateRiskScore();
    }

    public class OnlineTransaction : Transaction
    {
        public string IPAddress { get; set; }
        public string DeviceId { get; set; }

        public OnlineTransaction(string id, decimal amount, string account, string ip, string device)
            : base(id, amount, account)
        {
            IPAddress = ip;
            DeviceId = device;
        }

        public override int CalculateRiskScore()
        {
            int score = 0;
            if (Amount > 50000) score += 50;
            else if (Amount > 10000) score += 30;
            else score += 10;

            if (IPAddress.StartsWith("192.168")) score -= 10;
            return Math.Max(0, Math.Min(100, score));
        }
    }

    public class WireTransfer : Transaction
    {
        public string BeneficiaryCountry { get; set; }
        public string SwiftCode { get; set; }

        public WireTransfer(string id, decimal amount, string account, string country, string swift)
            : base(id, amount, account)
        {
            BeneficiaryCountry = country;
            SwiftCode = swift;
        }

        public override int CalculateRiskScore()
        {
            int score = 0;
            if (Amount > 100000) score += 70;
            else if (Amount > 50000) score += 50;
            else score += 20;

            if (BeneficiaryCountry != "India") score += 20;
            return Math.Max(0, Math.Min(100, score));
        }
    }
}
