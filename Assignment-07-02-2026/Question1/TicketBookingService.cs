using System;
using System.Collections.Generic;
using System.Linq;

public class TicketBookingService
{
    private readonly List<Seat> seats;
    private readonly object lockObj;

    public TicketBookingService(int totalSeats)
    {
        seats = new List<Seat>();
        lockObj = new object();

        for (int i = 1; i <= totalSeats; i++)
        {
            seats.Add(new Seat(i));
        }
    }

    public bool BookSeat(int seatNo, string userId)
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentException("Invalid user");

        lock (lockObj)
        {
            Seat seat = seats.FirstOrDefault(s => s.SeatNo == seatNo);

            if (seat == null)
                throw new ArgumentException("Invalid seat");

            if (seat.IsBooked)
                return false;

            seat.Book(userId);
            return true;
        }
    }
}
