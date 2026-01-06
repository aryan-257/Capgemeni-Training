using System;

namespace Question1;

public class FibonacciSeries
{
    public static void DisplayFibonacci()
    {
        Console.Write("Enter the number of terms: ");
        int n = Convert.ToInt32(Console.ReadLine());

        int first = 0, second = 1, next;

        Console.Write("Fibonacci Series: ");

        for (int i = 0; i < n; i++)
        {
            Console.Write(first + " ");
            next = first + second;
            first = second;
            second = next;
        }
        Console.WriteLine();
    }
}