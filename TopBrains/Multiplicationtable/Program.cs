using System;

public class Program
{
    public static void Main(string[] args)
    {
        int n = int.Parse(Console.ReadLine());
        int upto = int.Parse(Console.ReadLine());

        int[] result = MultiplicationTable.GetMultiplicationRow(n, upto);

        for (int i = 0; i < result.Length; i++)
        {
            Console.Write(result[i]);

            if (i < result.Length - 1)
            {
                Console.Write(",");
            }
        }
    }
}
