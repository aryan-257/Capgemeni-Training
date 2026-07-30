using System;

class Program
{
    static void Main()
    {
        string logLine = "2023-10-27 14:30:00 ERROR: Disk full";
        int lineCounter = 0;

        LogParser.ParseLogLine(in logLine, out DateTime timestamp, out LogLevel level, ref lineCounter);

        Console.WriteLine("Timestamp: " + timestamp);
        Console.WriteLine("Log Level: " + level);
        Console.WriteLine("Counter after call: " + lineCounter);
    }
}
