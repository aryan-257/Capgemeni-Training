using System;

namespace ParsingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter tokens separated by space:");
            string[] tokens = Console.ReadLine().Split(' ');

            ParserSumCalculator calculator = new ParserSumCalculator();
            int sum = calculator.SumValidIntegers(tokens);

            Console.WriteLine("Sum of valid integers: " + sum);
        }
    }
}
