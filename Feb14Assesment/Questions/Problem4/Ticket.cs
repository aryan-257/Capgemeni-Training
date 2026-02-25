using System;

namespace Problem4.AirlineBooking
{
    public abstract class Ticket
    {
        public string TicketId { get; set; }
        public string PassengerName { get; set; }
        public string SeatNumber { get; set; }
        public decimal BaseFare { get; set; }

        protected Ticket(string id, string passenger, string seat, decimal fare)
        {
            if (fare <= 0)
                throw new InvalidFareException("Fare must be positive");

            TicketId = id;
            PassengerName = passenger;
            SeatNumber = seat;
            BaseFare = fare;
        }

        public abstract decimal CalculateFare();
        public abstract string GetClass();
    }

    public class Economy : Ticket
    {
        public Economy(string id, string passenger, string seat, decimal fare)
            : base(id, passenger, seat, fare) { }

        public override decimal CalculateFare() => BaseFare;
        public override string GetClass() => "Economy";
    }

    public class Business : Ticket
    {
        public Business(string id, string passenger, string seat, decimal fare)
            : base(id, passenger, seat, fare) { }

        public override decimal CalculateFare() => BaseFare * 2.5m;
        public override string GetClass() => "Business";
    }

    public class FirstClass : Ticket
    {
        public FirstClass(string id, string passenger, string seat, decimal fare)
            : base(id, passenger, seat, fare) { }

        public override decimal CalculateFare() => BaseFare * 4m;
        public override string GetClass() => "First Class";
    }
}
