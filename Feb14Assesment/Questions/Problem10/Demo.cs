using System;

namespace Problem10.InvestmentPortfolio
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 10: Investment Portfolio Risk Categorization ===\n");
            PortfolioManager manager = new PortfolioManager();

            try
            {
                manager.AddInvestment(new Stocks("S001", "Tech Corp", 500000, 4, "TechCorp Inc", 1500));
                manager.AddInvestment(new Bonds("B001", "Government Bond", 300000, 1, 6.5m));
                manager.AddInvestment(new MutualFunds("M001", "Growth Fund", 200000, 3, "ABC Managers"));
                manager.AddInvestment(new Stocks("S002", "Pharma Ltd", 400000, 3, "Pharma Ltd", 2500));

                manager.DisplayPortfolio();
                manager.RecalculatePortfolioRisk();

                decimal totalReturns = manager.GetTotalExpectedReturns();
                Console.WriteLine($"\nTotal Expected Returns: ₹{totalReturns:F2}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
