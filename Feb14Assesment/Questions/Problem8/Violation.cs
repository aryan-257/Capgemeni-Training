using System;

namespace Problem8.TrafficViolation
{
    public class Violation
    {
        public string ViolationId { get; set; }
        public Vehicle Vehicle { get; set; }
        public string ViolationType { get; set; }
        public decimal PenaltyAmount { get; set; }
        public DateTime Timestamp { get; set; }

        public Violation(string id, Vehicle vehicle, string type, decimal penalty)
        {
            ViolationId = id;
            Vehicle = vehicle;
            ViolationType = type;
            PenaltyAmount = penalty;
            Timestamp = DateTime.Now;
        }
    }
}
