using System;

namespace GreatestCommonDivisorApp
{
    public class GcdCalculator
    {
        // Recursive method to calculate GCD using Euclid's algorithm
        public int ComputeGCD(int a, int b)
        {
            if (b == 0)
                return a;
            return ComputeGCD(b, a % b);
        }
    }
}
