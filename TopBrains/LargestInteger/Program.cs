using System;

public class Program
{
    public static void Main(string[] args)
    {
        int a = int.Parse(Console.ReadLine());
        int b = int.Parse(Console.ReadLine());
        int c = int.Parse(Console.ReadLine());

        int result = LargestFinder.FindLargest(a, b, c);
        Console.WriteLine(result);
    }
}
