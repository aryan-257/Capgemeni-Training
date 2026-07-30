using System;

class Program
{
    static void Main()
    {
        // call 1 - default time and compounding use ho rha
        double result1 = FinancialCalculator.CalculateCompoundInterest(10000, 0.05, 10);
        Console.WriteLine("Future Value (annual compounding): " + result1);

        // call 2 - named argument use kiya taaki sirf compoundingFrequency change ho, baaki default rahe
        double result2 = FinancialCalculator.CalculateCompoundInterest(10000, 0.05, 10, compoundingFrequency: 12);
        Console.WriteLine("Future Value (monthly compounding): " + result2);
    }
}
