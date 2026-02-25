using System;

namespace DisplayHeightApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your height in cm: ");
            int height = Convert.ToInt32(Console.ReadLine());

            HeightCategory hc = new HeightCategory();
            string category = hc.GetCategory(height);

            Console.WriteLine($"Height category: {category}");
        }
    }
}
