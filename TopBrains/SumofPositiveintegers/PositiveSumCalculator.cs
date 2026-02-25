using System;

namespace SumOfPositiveIntegersApp
{
    public class PositiveSumCalculator
    {
        // Method to sum positive numbers until a 0 is encountered
        public long SumPositiveUntilZero(int[] nums)
        {
            long sum = 0;
            foreach (int num in nums)
            {
                if (num == 0) break;       // Stop processing at 0
                if (num < 0) continue;     // Ignore negatives
                sum += num;                // Add positive numbers
            }
            return sum;
        }
    }
}
