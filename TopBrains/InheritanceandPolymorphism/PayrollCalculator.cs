using System;
using System.Collections.Generic;

namespace InheritanceAndPolymorphismApp
{
    public class PayrollCalculator
    {
        public decimal CalculateTotalPay(string[] employees)
        {
            decimal totalPay = 0;

            foreach (string emp in employees)
            {
                string[] parts = emp.Split(' ');
                Employee employee = null;

                switch (parts[0])
                {
                    case "H":
                        employee = new HourlyEmployee(
                            decimal.Parse(parts[1]),
                            decimal.Parse(parts[2]));
                        break;

                    case "S":
                        employee = new SalariedEmployee(
                            decimal.Parse(parts[1]));
                        break;

                    case "C":
                        employee = new CommissionEmployee(
                            decimal.Parse(parts[1]),
                            decimal.Parse(parts[2]));
                        break;
                }

                if (employee != null)
                    totalPay += employee.CalculatePay();
            }

            return Math.Round(totalPay, 2, MidpointRounding.AwayFromZero);
        }
    }
}
