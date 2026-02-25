using System;

namespace InventoryCleanupApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter product name: ");
            string input = Console.ReadLine();

            InventoryCleaner cleaner = new InventoryCleaner();
            string result = cleaner.CleanName(input);

            Console.WriteLine("Cleaned Name: " + result);
        }
    }
}
