using System;

namespace Problem10.InvestmentPortfolio
{
    public abstract class Asset
    {
        public string AssetId { get; set; }
        public string Name { get; set; }
        public decimal InvestedAmount { get; set; }
        public int RiskRating { get; set; }

        protected Asset(string id, string name, decimal amount, int risk)
        {
            if (risk < 1 || risk > 5)
                throw new InvalidRiskRatingException("Risk rating must be between 1 (Low) and 5 (High)");

            AssetId = id;
            Name = name;
            InvestedAmount = amount;
            RiskRating = risk;
        }

        public abstract decimal CalculateReturn();
        public abstract string GetAssetType();
    }

    public class Stocks : Asset
    {
        public string CompanyName { get; set; }
        public decimal CurrentPrice { get; set; }

        public Stocks(string id, string name, decimal amount, int risk, string company, decimal price)
            : base(id, name, amount, risk)
        {
            CompanyName = company;
            CurrentPrice = price;
        }

        public override decimal CalculateReturn()
        {
            return InvestedAmount * 0.12m;
        }

        public override string GetAssetType() => "Stocks";
    }

    public class Bonds : Asset
    {
        public decimal InterestRate { get; set; }

        public Bonds(string id, string name, decimal amount, int risk, decimal rate)
            : base(id, name, amount, risk)
        {
            InterestRate = rate;
        }

        public override decimal CalculateReturn()
        {
            return InvestedAmount * (InterestRate / 100);
        }

        public override string GetAssetType() => "Bonds";
    }

    public class MutualFunds : Asset
    {
        public string FundManager { get; set; }

        public MutualFunds(string id, string name, decimal amount, int risk, string manager)
            : base(id, name, amount, risk)
        {
            FundManager = manager;
        }

        public override decimal CalculateReturn()
        {
            return InvestedAmount * 0.08m;
        }

        public override string GetAssetType() => "Mutual Funds";
    }
}
