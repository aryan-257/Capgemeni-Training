using System;

class Patient
{
    public int Age;
    public double Weight;
    public double Height;
    public double Temperature;
    public double BMI;
}

class Validator
{
    // ye function tab tak loop chalayega jab tak valid age na mile
    public int GetValidAge()
    {
        int age;
        while (true)
        {
            Console.Write("Enter Age: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out age) && age > 0 && age < 120)
            {
                return age;
            }
            Console.WriteLine("Invalid age. Please enter a value between 1-120.");
        }
    }

    public double GetValidWeight()
    {
        double weight;
        while (true)
        {
            Console.Write("Enter Weight (kg): ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out weight) && weight > 0 && weight < 500)
            {
                return weight;
            }
            Console.WriteLine("Invalid weight. Please enter a valid positive number.");
        }
    }

    public double GetValidHeight()
    {
        double height;
        while (true)
        {
            Console.Write("Enter Height (m): ");
            string input = Console.ReadLine();

            if (double.TryParse(input, out height) && height > 0 && height < 3)
            {
                return height;
            }
            Console.WriteLine("Invalid height. Please enter a valid positive number (in meters).");
        }
    }

    public double GetValidTemperature()
    {
        double temp;
        while (true)
        {
            Console.Write("Enter Body Temperature (F): ");
            string input = Console.ReadLine();

            //normal body temp range rakha h thoda wide taki fever bhi allow ho
            if (double.TryParse(input, out temp) && temp > 90 && temp < 110)
            {
                return temp;
            }
            Console.WriteLine("Invalid temperature. Please enter a realistic value.");
        }
    }
}

class Program
{
    static void Main()
    {
        Validator validator = new Validator();
        Patient patient = new Patient();

        patient.Age = validator.GetValidAge();
        patient.Weight = validator.GetValidWeight();
        patient.Height = validator.GetValidHeight();
        patient.Temperature = validator.GetValidTemperature();

        patient.BMI = patient.Weight / (patient.Height * patient.Height);
        patient.BMI = Math.Round(patient.BMI, 2);

        Console.WriteLine();
        Console.WriteLine("---- Patient Summary ----");
        Console.WriteLine("Age: " + patient.Age);
        Console.WriteLine("Weight: " + patient.Weight + " kg");
        Console.WriteLine("Height: " + patient.Height + " m");
        Console.WriteLine("Temperature: " + patient.Temperature + " F");
        Console.WriteLine("BMI: " + patient.BMI);
    }
}