using System;
using System.Linq;

namespace SumOfPositiveIntegersApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter integers (space-separated): ");
            int[] nums = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            PositiveSumCalculator calculator = new PositiveSumCalculator();
            long sum = calculator.SumPositiveUntilZero(nums);

            Console.WriteLine("Sum of positive integers until 0: " + sum);
        }
    }
}
