using System;

class Program
{
    static void Main()
    {
        string template = "User {0} logged in from {1} at {2}";

        string formatted = LogFormatter.FormatLogMessage(template, "JohnDoe", "192.168.1.1", DateTime.Now);

        Console.WriteLine(formatted);
    }
}
