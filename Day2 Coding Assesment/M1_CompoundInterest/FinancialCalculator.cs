using System;

static class FinancialCalculator
{
    // default time 10 years, default compounding annually (1 baar saal me)
    public static double CalculateCompoundInterest(double principal, double rate, int time = 10, int compoundingFrequency = 1)
    {
        // formula: A = P(1 + r/n)^(nt)
        double amount = principal * Math.Pow((1 + rate / compoundingFrequency), compoundingFrequency * time);
        return Math.Round(amount, 2);
    }
}
