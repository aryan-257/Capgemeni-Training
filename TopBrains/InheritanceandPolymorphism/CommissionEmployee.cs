using System;

namespace InheritanceAndPolymorphismApp
{
    public class CommissionEmployee : Employee
    {
        private decimal commission;
        private decimal baseSalary;

        public CommissionEmployee(decimal commission, decimal baseSalary)
        {
            this.commission = commission;
            this.baseSalary = baseSalary;
        }

        public override decimal CalculatePay()
        {
            return baseSalary + commission;
        }
    }
}
