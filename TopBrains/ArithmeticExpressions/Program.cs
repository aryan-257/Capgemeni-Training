using System;

public class Program
{
    public static void Main(string[] args)
    {
        string expression = Console.ReadLine();

        string result = ArithmeticEvaluator.Evaluate(expression);
        Console.WriteLine(result);
    }
}
