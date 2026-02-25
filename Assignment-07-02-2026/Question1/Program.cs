using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        TicketBookingService bookingService = new TicketBookingService(5);

        Task t1 = Task.Run(() => Book(bookingService, 1, "Aryan"));
        Task t2 = Task.Run(() => Book(bookingService, 1, "Rahul"));
        Task t3 = Task.Run(() => Book(bookingService, 1, "Prince"));

        Task.WaitAll(t1, t2, t3);

        Console.WriteLine("Done");
        Console.ReadLine();
    }

    static void Book(TicketBookingService service, int seatNo, string userId)
    {
        bool result = service.BookSeat(seatNo, userId);

        if (result)
            Console.WriteLine($"Seat {seatNo} booked by {userId}");
        else
            Console.WriteLine($"Seat {seatNo} already booked. {userId} failed");
    }
}
