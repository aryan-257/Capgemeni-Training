using System;

namespace Question2
{
    public class LargestofThree
    {
        public static void FindLargest()
        {
            Console.Write("Enter first number: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter second number: ");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter third number: ");
            int c = Convert.ToInt32(Console.ReadLine());

            if (a >= b && a >= c)
            {
                Console.WriteLine("Largest number is: " + a);
            }
            else if (b >= a && b >= c)
            {
                Console.WriteLine("Largest number is: " + b);
            }
            else
            {
                Console.WriteLine("Largest number is: " + c);
            }
        }
    }
}
