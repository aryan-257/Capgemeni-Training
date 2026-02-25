using System;
using System.IO;

namespace FileIOApp
{
    public class LogProcessor
    {
        // Method to extract ERROR logs and save to a new file
        public void ExtractErrorLogs(string inputFile, string outputFile)
        {
            if (!File.Exists(inputFile))
            {
                Console.WriteLine($"File not found: {inputFile}");
                return;
            }

            using (StreamReader reader = new StreamReader(inputFile))
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("ERROR"))
                    {
                        writer.WriteLine(line);
                    }
                }
            }

            Console.WriteLine($"ERROR logs extracted to {outputFile}");
        }
    }
}
