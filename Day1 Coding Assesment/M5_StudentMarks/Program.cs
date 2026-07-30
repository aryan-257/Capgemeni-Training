using System;

class StudentMarks
{
    static void Main()
    {
        Console.Write("Enter Marks for Subject 1: ");
        string m1 = Console.ReadLine();

        Console.Write("Enter Marks for Subject 2: ");
        string m2 = Console.ReadLine();

        Console.Write("Enter Marks for Subject 3: ");
        string m3 = Console.ReadLine();

        Console.Write("Enter Marks for Subject 4: ");
        string m4 = Console.ReadLine();

        Console.Write("Enter Marks for Subject 5: ");
        string m5 = Console.ReadLine();

        //validate subject 1
        if (!double.TryParse(m1, out double sub1) || sub1 < 0 || sub1 > 100)
        {
            Console.WriteLine("Invalid marks for Subject 1. Must be between 0-100.");
            return;
        }

        //validate subject 2
        if (!double.TryParse(m2, out double sub2) || sub2 < 0 || sub2 > 100)
        {
            Console.WriteLine("Invalid marks for Subject 2. Must be between 0-100.");
            return;
        }

        //validate subject 3
        if (!double.TryParse(m3, out double sub3) || sub3 < 0 || sub3 > 100)
        {
            Console.WriteLine("Invalid marks for Subject 3. Must be between 0-100.");
            return;
        }

        //validate subject 4
        if (!double.TryParse(m4, out double sub4) || sub4 < 0 || sub4 > 100)
        {
            Console.WriteLine("Invalid marks for Subject 4. Must be between 0-100.");
            return;
        }

        // validate subject 5
        if (!double.TryParse(m5, out double sub5) || sub5 < 0 || sub5 > 100)
        {
            Console.WriteLine("Invalid marks for Subject 5. Must be between 0-100.");
            return;
        }

        double total = sub1 + sub2 + sub3 + sub4 + sub5;
        double average = total / 5;
        double percentage = (total / 500) * 100;

        percentage = Math.Round(percentage, 2);
        average = Math.Round(average, 2);

        Console.WriteLine();
        Console.WriteLine("Total Marks: " + total);
        Console.WriteLine("Average: " + average);
        Console.WriteLine("Percentage: " + percentage + "%");
    }
}