using System;

namespace Question4
{
    class QuadraticEquation
    {
        public static void FindRoots()
        {
            Console.Write("Enter value of a: ");
            int a = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter value of b: ");
            int b = Convert.ToInt32(Console.ReadLine());

            Console.Write("Enter value of c: ");
            int c = Convert.ToInt32(Console.ReadLine());

            int discriminant = (b * b) - (4 * a * c);

            if (discriminant > 0)
            {
                Console.WriteLine("Roots are real and different.");
            }
            else if (discriminant == 0)
            {
                Console.WriteLine("Roots are real and same.");
            }
            else
            {
                Console.WriteLine("Roots are complex and different.");
            }
        }
    }
}
