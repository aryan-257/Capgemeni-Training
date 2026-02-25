using System;

namespace Problem8.TrafficViolation
{
    public class InvalidVehicleException : Exception
    {
        public InvalidVehicleException(string message) : base(message) { }
    }

    public class PenaltyExceedsLimitException : Exception
    {
        public PenaltyExceedsLimitException(string message) : base(message) { }
    }
}
