using System;

namespace Problem4.AirlineBooking
{
    public class SeatAlreadyBookedException : Exception
    {
        public SeatAlreadyBookedException(string message) : base(message) { }
    }

    public class InvalidFareException : Exception
    {
        public InvalidFareException(string message) : base(message) { }
    }
}
