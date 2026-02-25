using System;

namespace ProgrammingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter range m and n: ");
            string[] input = Console.ReadLine().Split(' ');
            int m = int.Parse(input[0]);
            int n = int.Parse(input[1]);

            LuckyNumberChecker checker = new LuckyNumberChecker();
            int result = checker.CountLuckyNumbers(m, n);

            Console.WriteLine(result);
        }
    }
}
