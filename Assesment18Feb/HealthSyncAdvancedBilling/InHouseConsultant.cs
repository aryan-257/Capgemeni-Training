using System;

namespace HealthSyncAdvancedBilling
{
    // Demonstrates Polymorphism through method overriding
    public class InHouseConsultant : Consultant
    {
        public double MonthlyStipend { get; set; }
        private const double AllowanceRate = 0.20; // 20% of stipend
        private const double BonusRate = 0.10; // 10% of stipend

        public InHouseConsultant(string consultantId, string name, double monthlyStipend)
            : base(consultantId, name)
        {
            MonthlyStipend = monthlyStipend;
        }

        // Override abstract method with In-House specific calculation
        public override double CalculateGrossPayout()
        {
            double allowance = MonthlyStipend * AllowanceRate;
            double bonus = MonthlyStipend * BonusRate;
            return MonthlyStipend + allowance + bonus;
        }

        // Uses inherited virtual CalculateTDS method (sliding scale: 5% or 15%)
    }
}
