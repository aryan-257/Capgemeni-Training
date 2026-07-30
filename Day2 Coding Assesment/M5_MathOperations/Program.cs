using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Add(5, 10) = " + MathOperations.Add(5, 10));
        Console.WriteLine("Add(1,2,3,4,5) = " + MathOperations.Add(1, 2, 3, 4, 5));
        Console.WriteLine("Multiply(2, 3) = " + MathOperations.Multiply(2, 3));
        Console.WriteLine("Multiply(2,3,4,5) = " + MathOperations.Multiply(2, 3, 4, 5));
    }
}
