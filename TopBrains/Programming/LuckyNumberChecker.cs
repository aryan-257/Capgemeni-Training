using System;

namespace ProgrammingApp
{
    public class LuckyNumberChecker
    {
        // Method to sum digits of a number
        public int SumOfDigits(int n)
        {
            int sum = 0;
            while (n > 0)
            {
                sum += n % 10;
                n /= 10;
            }
            return sum;
        }

        // Check if a number is prime
        public bool IsPrime(int n)
        {
            if (n <= 1) return false;
            for (int i = 2; i * i <= n; i++)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        // Check if a number is a Lucky Number
        public bool IsLuckyNumber(int x)
        {
            if (IsPrime(x)) return false; // Must be non-prime
            int Sx = SumOfDigits(x);
            int SxSquared = SumOfDigits(x * x);
            return SxSquared == Sx * Sx;
        }

        // Count lucky numbers in a range [m, n]
        public int CountLuckyNumbers(int m, int n)
        {
            int count = 0;
            for (int i = m; i <= n; i++)
            {
                if (IsLuckyNumber(i)) count++;
            }
            return count;
        }
    }
}
