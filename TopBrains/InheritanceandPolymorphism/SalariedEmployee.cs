using System;

namespace InheritanceAndPolymorphismApp
{
    public class SalariedEmployee : Employee
    {
        private decimal monthlySalary;

        public SalariedEmployee(decimal monthlySalary)
        {
            this.monthlySalary = monthlySalary;
        }

        public override decimal CalculatePay()
        {
            return monthlySalary;
        }
    }
}
