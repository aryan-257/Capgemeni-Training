using System;

namespace HealthSyncAdvancedBilling
{
    // Demonstrates Polymorphism through method overriding
    public class VisitingConsultant : Consultant
    {
        public int ConsultationsCount { get; set; }
        public double RatePerVisit { get; set; }

        public VisitingConsultant(string consultantId, string name, int consultationsCount, double ratePerVisit)
            : base(consultantId, name)
        {
            ConsultationsCount = consultationsCount;
            RatePerVisit = ratePerVisit;
        }

        // Override abstract method with Visiting specific calculation
        public override double CalculateGrossPayout()
        {
            return ConsultationsCount * RatePerVisit;
        }

        // Override virtual method to apply flat 10% TDS rate
        public override double CalculateTDS(double grossPayout)
        {
            return 0.10; // Flat 10% for all visiting consultants
        }
    }
}
