using System;

class Program
{
    static void Main()
    {
        int score = RiskCalculator.CalculateRiskScore("TX001");
        Console.WriteLine("Risk Score: " + score);

        // ek invalid id bhi test kr lete h
        int invalidScore = RiskCalculator.CalculateRiskScore("bad-id");
        Console.WriteLine("Risk Score for invalid id: " + invalidScore);
    }
}
