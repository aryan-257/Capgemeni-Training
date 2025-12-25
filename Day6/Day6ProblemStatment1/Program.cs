// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using System;

namespace Day6ProblemStatment1;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Enter book title:");
        string title = Console.ReadLine()!;

        Console.WriteLine("Enter book author:");
        string author = Console.ReadLine()!;

        Console.WriteLine("Enter number of pages:");
        int numPages = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter due date (yyyy-MM-dd):");
        DateTime dueDate = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter return date (yyyy-MM-dd):");
        DateTime returnDate = DateTime.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter number of days to read:");
        int daysToRead = int.Parse(Console.ReadLine()!);

        Console.WriteLine("Enter daily late fee rate:");
        double dailyLateFeeRate = double.Parse(Console.ReadLine()!);

        Book myBook = new Book(title, author, numPages, dueDate, returnDate);

        double averagePagesPerDay = myBook.AveragePagesPerDay(daysToRead);
        double lateFee = myBook.CalculateLateFee(dailyLateFeeRate);

        Console.WriteLine($"Average pages to read per day: {averagePagesPerDay}");
        Console.WriteLine($"Late fee: {lateFee}");

    }
}

