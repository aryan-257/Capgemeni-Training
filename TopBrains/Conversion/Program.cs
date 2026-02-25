using System;

namespace ConversionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter value in feet: ");
            int feet = int.Parse(Console.ReadLine());

            UnitConverter converter = new UnitConverter();
            double centimeters = converter.FeetToCentimeters(feet);

            Console.WriteLine("Centimeters: " + centimeters);
        }
    }
}
