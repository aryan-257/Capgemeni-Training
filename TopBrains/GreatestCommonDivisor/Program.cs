using System;

namespace GreatestCommonDivisorApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter two non-negative integers separated by space: ");
            string[] input = Console.ReadLine().Split(' ');
            int a = int.Parse(input[0]);
            int b = int.Parse(input[1]);

            GcdCalculator calculator = new GcdCalculator();
            int gcd = calculator.ComputeGCD(a, b);

            Console.WriteLine("GCD: " + gcd);
        }
    }
}
