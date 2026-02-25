using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

public record Student(string Name, int Score);

public class StudentProcessor
{
    public static string BuildJson(string[] items, int minScore)
    {
        List<Student> students = new List<Student>();

        foreach (string item in items)
        {
            string[] parts = item.Split(':');

            if (parts.Length != 2)
                continue;

            if (!int.TryParse(parts[1], out int score))
                continue;

            students.Add(new Student(parts[0], score));
        }

        var result = students
            .Where(s => s.Score >= minScore)
            .OrderByDescending(s => s.Score)
            .ThenBy(s => s.Name)
            .ToList();

        return JsonSerializer.Serialize(result);
    }
}
