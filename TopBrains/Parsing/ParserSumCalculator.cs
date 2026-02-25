using System;

namespace ParsingApp
{
    public class ParserSumCalculator
    {
        // Method to sum only valid 32-bit integers
        public int SumValidIntegers(string[] tokens)
        {
            int sum = 0;
            foreach (string token in tokens)
            {
                if (int.TryParse(token, out int value))
                {
                    sum += value;
                }
                // Ignore invalid or overflow values automatically
            }
            return sum;
        }
    }
}
