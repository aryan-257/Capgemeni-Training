using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem8.TrafficViolation
{
    public class TrafficMonitor
    {
        private SortedDictionary<decimal, List<Violation>> violations = new SortedDictionary<decimal, List<Violation>>(Comparer<decimal>.Create((a, b) => b.CompareTo(a)));
        private Dictionary<string, int> vehicleViolationCount = new Dictionary<string, int>();
        private const decimal MaxPenalty = 50000;

        public void AddViolation(Violation violation)
        {
            if (string.IsNullOrEmpty(violation.Vehicle.RegistrationNumber))
                throw new InvalidVehicleException("Invalid vehicle registration");

            if (violation.PenaltyAmount > MaxPenalty)
                throw new PenaltyExceedsLimitException($"Penalty ₹{violation.PenaltyAmount} exceeds maximum limit ₹{MaxPenalty}");

            if (!violations.ContainsKey(violation.PenaltyAmount))
                violations[violation.PenaltyAmount] = new List<Violation>();

            violations[violation.PenaltyAmount].Add(violation);

            string regNo = violation.Vehicle.RegistrationNumber;
            vehicleViolationCount[regNo] = vehicleViolationCount.GetValueOrDefault(regNo, 0) + 1;

            Console.WriteLine($"Violation recorded: {violation.ViolationType} - ₹{violation.PenaltyAmount} for {regNo}");
        }

        public void EscalateRepeatOffenders()
        {
            Console.WriteLine("\n=== Repeat Offenders (2+ violations) ===");
            foreach (var kvp in vehicleViolationCount.Where(x => x.Value >= 2))
            {
                Console.WriteLine($"⚠️ {kvp.Key}: {kvp.Value} violations - ESCALATED");
            }
        }

        public void DisplayViolations()
        {
            Console.WriteLine("\n=== Traffic Violations (Sorted by Penalty) ===");
            foreach (var kvp in violations)
            {
                Console.WriteLine($"\nPenalty: ₹{kvp.Key}");
                foreach (var v in kvp.Value)
                    Console.WriteLine($"  - {v.Vehicle.RegistrationNumber} ({v.Vehicle.GetVehicleType()}): {v.ViolationType}");
            }
        }
    }
}
