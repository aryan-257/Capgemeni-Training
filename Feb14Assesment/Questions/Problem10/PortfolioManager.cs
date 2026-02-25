using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem10.InvestmentPortfolio
{
    public class PortfolioManager
    {
        private SortedDictionary<int, List<Asset>> portfolio = new SortedDictionary<int, List<Asset>>();
        private decimal totalInvestment = 0;
        private const decimal InvestmentLimit = 10000000;

        public void AddInvestment(Asset asset)
        {
            if (totalInvestment + asset.InvestedAmount > InvestmentLimit)
                throw new InvestmentLimitExceededException($"Adding this investment would exceed limit of ₹{InvestmentLimit}");

            if (!portfolio.ContainsKey(asset.RiskRating))
                portfolio[asset.RiskRating] = new List<Asset>();

            portfolio[asset.RiskRating].Add(asset);
            totalInvestment += asset.InvestedAmount;
            Console.WriteLine($"Added {asset.GetAssetType()}: {asset.Name} (Risk: {asset.RiskRating}, Amount: ₹{asset.InvestedAmount})");
        }

        public void RecalculatePortfolioRisk()
        {
            if (totalInvestment == 0)
            {
                Console.WriteLine("\nPortfolio Risk: N/A (No investments)");
                return;
            }

            decimal weightedRisk = 0;
            foreach (var kvp in portfolio)
            {
                foreach (var asset in kvp.Value)
                {
                    decimal weight = asset.InvestedAmount / totalInvestment;
                    weightedRisk += kvp.Key * weight;
                }
            }

            Console.WriteLine($"\n=== Portfolio Risk Analysis ===");
            Console.WriteLine($"Total Investment: ₹{totalInvestment:N2}");
            Console.WriteLine($"Weighted Average Risk: {weightedRisk:F2}");
            Console.WriteLine($"Risk Category: {GetRiskCategory(weightedRisk)}");
        }

        private string GetRiskCategory(decimal risk)
        {
            if (risk <= 2m) return "Low Risk";
            if (risk <= 3.5m) return "Moderate Risk";
            return "High Risk";
        }

        public void DisplayPortfolio()
        {
            Console.WriteLine("\n=== Investment Portfolio (By Risk Rating) ===");
            foreach (var kvp in portfolio)
            {
                Console.WriteLine($"\nRisk Rating {kvp.Key}:");
                foreach (var asset in kvp.Value)
                {
                    decimal returns = asset.CalculateReturn();
                    Console.WriteLine($"  - {asset.Name} ({asset.GetAssetType()}): ₹{asset.InvestedAmount} | Expected Return: ₹{returns:F2}");
                }
            }
        }

        public decimal GetTotalExpectedReturns()
        {
            return portfolio.SelectMany(kvp => kvp.Value)
                          .Sum(asset => asset.CalculateReturn());
        }
    }
}
