using System;

static class LogParser
{
    // in parameter use kiya h - string ki copy nhi banti, direct reference use hota h (read-only)
    public static void ParseLogLine(in string logLine, out DateTime timestamp, out LogLevel level, ref int counter)
    {
        // log line format: "2023-10-27 14:30:00 ERROR: Disk full"
        string[] parts = logLine.Split(new char[] { ' ' }, 3);

        string datePart = parts[0] + " " + parts[1];
        DateTime.TryParse(datePart, out timestamp);

        string levelPart = parts[2].Split(':')[0].Trim();

        if (!Enum.TryParse(levelPart, true, out level))
        {
            level = LogLevel.Info; // agar match na ho to default Info
        }

        counter = counter + 1; // ref hai isliye original variable hi update hoga
    }
}
