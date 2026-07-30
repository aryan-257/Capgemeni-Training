using System;

interface IInvestment
{
    double CalculateReturns(double principal, double rate, int years);
}

class FixedDeposit : IInvestment
{
    public double CalculateReturns(double principal, double rate, int years)
    {
        //simple interest formula for FD
        double interest = (principal * rate * years) / 100;
        return principal + interest;
    }
}

class MutualFund : IInvestment
{
    public double CalculateReturns(double principal, double rate, int years)
    {
        // compound interest for mutual funds (yearly compounding)
        double amount = principal * Math.Pow((1 + rate / 100), years);
        return amount;
    }
}

class RecurringDeposit : IInvestment
{
    public double CalculateReturns(double principal, double rate, int years)
    {
        //RD ka approx formula, thoda simplified rakha h
        int months = years * 12;
        double interest = principal * months * (months + 1) * rate / (2 * 12 * 100);
        return (principal * months) + interest;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter Investment Type (FixedDeposit/MutualFund/RecurringDeposit): ");
        string investmentType = Console.ReadLine();

        Console.Write("Enter Principal Amount: ");
        string principalInput = Console.ReadLine();

        Console.Write("Enter Annual Rate (%): ");
        string rateInput = Console.ReadLine();

        Console.Write("Enter Duration (Years): ");
        string yearsInput = Console.ReadLine();

        //principal validation
        if (!double.TryParse(principalInput, out double principal) || principal <= 0)
        {
            Console.WriteLine("Invalid principal amount entered.");
            return;
        }

        //rate validation - percentage se zyada nhi hona chahiye realistically
        if (!double.TryParse(rateInput, out double rate) || rate <= 0 || rate > 100)
        {
            Console.WriteLine("Invalid rate entered. Must be between 0-100.");
            return;
        }

        //years validation
        if (!int.TryParse(yearsInput, out int years) || years <= 0 || years > 50)
        {
            Console.WriteLine("Invalid duration entered. Must be between 1-50 years.");
            return;
        }

        IInvestment investment;

        string type = investmentType.Trim().ToLower();

        if (type == "fixeddeposit")
        {
            investment = new FixedDeposit();
        }
        else if (type == "mutualfund")
        {
            investment = new MutualFund();
        }
        else if (type == "recurringdeposit")
        {
            investment = new RecurringDeposit();
        }
        else
        {
            Console.WriteLine("Invalid investment type entered.");
            return;
        }

        double projectedValue = investment.CalculateReturns(principal, rate, years);
        projectedValue = Math.Round(projectedValue, 2);

        Console.WriteLine();
        Console.WriteLine("Projected Investment Value: " + projectedValue);
    }
}