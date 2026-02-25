using System;

namespace Problem4.AirlineBooking
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 4: Airline Booking Fare Classification ===\n");
            BookingSystem system = new BookingSystem();

            try
            {
                system.AddTicket(new Economy("T001", "John", "12A", 5000));
                system.AddTicket(new Business("T002", "Sarah", "3B", 5000));
                system.AddTicket(new FirstClass("T003", "Mike", "1A", 5000));
                system.AddTicket(new Economy("T004", "Emma", "15C", 4500));

                system.DisplayBookings();

                Console.WriteLine("\n--- Testing Duplicate Seat Booking ---");
                system.AddTicket(new Economy("T005", "David", "12A", 5000));
            }
            catch (SeatAlreadyBookedException ex)
            {
                Console.WriteLine($"BOOKING ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
