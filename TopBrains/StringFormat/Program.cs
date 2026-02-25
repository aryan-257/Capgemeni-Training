using System;

public class Program
{
    public static void Main(string[] args)
    {
        int count = int.Parse(Console.ReadLine());
        string[] items = new string[count];

        for (int i = 0; i < count; i++)
        {
            items[i] = Console.ReadLine();
        }

        int minScore = int.Parse(Console.ReadLine());

        string json = StudentProcessor.BuildJson(items, minScore);
        Console.WriteLine(json);
    }
}
