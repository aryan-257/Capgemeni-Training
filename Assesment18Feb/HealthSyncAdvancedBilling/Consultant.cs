using System;
using System.Text.RegularExpressions;

namespace HealthSyncAdvancedBilling
{
    // Abstract base class demonstrating Abstraction
    public abstract class Consultant
    {
        public string ConsultantId { get; set; }
        public string Name { get; set; }

        public Consultant(string consultantId, string name)
        {
            if (!ValidateConsultantId(consultantId))
            {
                throw new ArgumentException("Invalid doctor id");
            }
            ConsultantId = consultantId;
            Name = name;
        }

        // Validation: ID must be exactly 6 chars, start with "DR", last 4 must be numeric
        private bool ValidateConsultantId(string id)
        {
            if (string.IsNullOrEmpty(id) || id.Length != 6)
                return false;

            if (!id.StartsWith("DR"))
                return false;

            string lastFour = id.Substring(2);
            return Regex.IsMatch(lastFour, @"^\d{4}$");
        }

        // Abstract method - forces subclasses to implement their own calculation
        public abstract double CalculateGrossPayout();

        // Virtual method - can be overridden by subclasses for custom taxation
        public virtual double CalculateTDS(double grossPayout)
        {
            // Default sliding scale for In-House consultants
            if (grossPayout <= 5000)
                return 0.05; // 5%
            else
                return 0.15; // 15%
        }

        public double CalculateNetPayout()
        {
            double gross = CalculateGrossPayout();
            double tdsRate = CalculateTDS(gross);
            double tdsAmount = gross * tdsRate;
            return gross - tdsAmount;
        }

        public void DisplayPayoutDetails()
        {
            double gross = CalculateGrossPayout();
            double tdsRate = CalculateTDS(gross);
            double net = CalculateNetPayout();

            Console.WriteLine($"Consultant: {Name} (ID: {ConsultantId})");
            Console.WriteLine($"Gross: {gross:F2} | TDS Applied: {tdsRate * 100}% | Net Payout: {net:F2}");
        }
    }
}
