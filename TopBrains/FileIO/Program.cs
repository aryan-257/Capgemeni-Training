using System;

namespace FileIOApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter input log file name (e.g., log.txt): ");
            string inputFile = Console.ReadLine();

            Console.Write("Enter output file name for ERROR logs (e.g., error.txt): ");
            string outputFile = Console.ReadLine();

            LogProcessor processor = new LogProcessor();
            processor.ExtractErrorLogs(inputFile, outputFile);
        }
    }
}
