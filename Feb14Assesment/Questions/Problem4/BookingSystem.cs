using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem4.AirlineBooking
{
    public class BookingSystem
    {
        private SortedDictionary<decimal, List<Ticket>> bookings = new SortedDictionary<decimal, List<Ticket>>();
        private HashSet<string> bookedSeats = new HashSet<string>();

        public void AddTicket(Ticket ticket)
        {
            if (bookedSeats.Contains(ticket.SeatNumber))
                throw new SeatAlreadyBookedException($"Seat {ticket.SeatNumber} is already booked");

            decimal fare = ticket.CalculateFare();

            if (!bookings.ContainsKey(fare))
                bookings[fare] = new List<Ticket>();

            bookings[fare].Add(ticket);
            bookedSeats.Add(ticket.SeatNumber);
            Console.WriteLine($"Booked {ticket.GetClass()} ticket for {ticket.PassengerName}, Seat: {ticket.SeatNumber}, Fare: ₹{fare}");
        }

        public void DisplayBookings()
        {
            Console.WriteLine("\n=== Airline Bookings (Sorted by Fare) ===");
            foreach (var kvp in bookings)
            {
                Console.WriteLine($"\nFare: ₹{kvp.Key}");
                foreach (var ticket in kvp.Value)
                    Console.WriteLine($"  - {ticket.PassengerName} ({ticket.GetClass()}) - Seat {ticket.SeatNumber}");
            }
        }

        public bool IsSeatAvailable(string seatNumber) => !bookedSeats.Contains(seatNumber);
    }
}
