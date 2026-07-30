using System;

class BMICalculator
{
    static void Main()
    {
        Console.Write("Enter Weight (kg): ");
        string weightInput = Console.ReadLine();

        Console.Write("Enter Height (m): ");
        string heightInput = Console.ReadLine();

        //weight check
        if (!double.TryParse(weightInput, out double weight) || weight <= 0)
        {
            Console.WriteLine("Invalid weight entered. Weight must be a positive number.");
            return;
        }

        //height check - cant be zero warna divide by zero ho jayega
        if (!double.TryParse(heightInput, out double height) || height <= 0)
        {
            Console.WriteLine("Invalid height entered. Height must be a positive number.");
            return;
        }

        double bmi = weight / (height * height);
        bmi = Math.Round(bmi, 2);

        string category;

        //bmi category check
        if (bmi < 18.5)
        {
            category = "Underweight";
        }
        else if (bmi >= 18.5 && bmi <= 24.9)
        {
            category = "Normal";
        }
        else if (bmi >= 25 && bmi <= 29.9)
        {
            category = "Overweight";
        }
        else
        {
            category = "Obese";
        }

        Console.WriteLine();
        Console.WriteLine("Your BMI is: " + bmi);
        Console.WriteLine("Category: " + category);
    }
}