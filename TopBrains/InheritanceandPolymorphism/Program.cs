using System;

namespace InheritanceAndPolymorphismApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter employee details (comma separated):");
            string[] employees = Console.ReadLine().Split(',');

            PayrollCalculator calculator = new PayrollCalculator();
            decimal totalPay = calculator.CalculateTotalPay(employees);

            Console.WriteLine("Total Payroll: " + totalPay);
        }
    }
}
