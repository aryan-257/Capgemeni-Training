using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        string input = "978-3-16-148410-0, 1234567890123, invalid-isbn, 978-1-4028-9462-6";

        bool success = OrderProcessor.TryProcessOrder(out List<string> validIsbns, input);

        Console.WriteLine("Processing successful: " + success);
        Console.WriteLine("Valid ISBNs found:");

        foreach (string isbn in validIsbns)
        {
            Console.WriteLine(" - " + isbn);
        }
    }
}
