using System;
using System.Linq;

namespace NullValueApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter values separated by space (use 'null' for nulls):");
            string[] input = Console.ReadLine().Split(' ');

            double?[] values = input.Select(s => s.ToLower() == "null" ? (double?)null : double.Parse(s)).ToArray();

            NullableAverageCalculator calculator = new NullableAverageCalculator();
            double? average = calculator.ComputeAverage(values);

            if (average.HasValue)
                Console.WriteLine("Average of non-null values: " + average.Value);
            else
                Console.WriteLine("No non-null values, result is null");
        }
    }
}

