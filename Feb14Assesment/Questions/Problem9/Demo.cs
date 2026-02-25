using System;

namespace Problem9.ITSupport
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 9: IT Support Ticket Severity System ===\n");
            TicketSystem system = new TicketSystem();

            try
            {
                var emp1 = new Employee("E001", "John", "john@company.com", "IT");
                var admin1 = new Admin("A001", "Sarah", "sarah@company.com", "System Admin");

                system.AddTicket(new SupportTicket("T001", "Server Down", "Production server not responding", 1, emp1));
                system.AddTicket(new SupportTicket("T002", "Printer Issue", "Printer not working", 4, emp1));
                system.AddTicket(new SupportTicket("T003", "Network Slow", "Network speed degraded", 2, emp1));

                system.DisplayTickets();

                Console.WriteLine("\n--- Processing Tickets ---");
                var ticket1 = system.ProcessTicket();
                admin1.ResolveTicket(ticket1);

                Console.WriteLine("\n--- Escalating Ticket ---");
                system.EscalateTicket("T002");

                system.DisplayTickets();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
