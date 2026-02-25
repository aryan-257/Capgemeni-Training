using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem9.ITSupport
{
    public class TicketSystem
    {
        private SortedDictionary<int, Queue<SupportTicket>> tickets = new SortedDictionary<int, Queue<SupportTicket>>();

        public void AddTicket(SupportTicket ticket)
        {
            if (ticket.Severity < 1 || ticket.Severity > 5)
                throw new InvalidPriorityException("Severity must be between 1 (Critical) and 5 (Low)");

            if (!tickets.ContainsKey(ticket.Severity))
                tickets[ticket.Severity] = new Queue<SupportTicket>();

            tickets[ticket.Severity].Enqueue(ticket);
            Console.WriteLine($"Ticket added: {ticket.Title} (Severity: {ticket.Severity})");
        }

        public SupportTicket ProcessTicket()
        {
            if (tickets.Count == 0)
                throw new TicketNotFoundException("No tickets in queue");

            var firstQueue = tickets.First();
            if (firstQueue.Value.Count == 0)
            {
                tickets.Remove(firstQueue.Key);
                return ProcessTicket();
            }

            var ticket = firstQueue.Value.Dequeue();
            Console.WriteLine($"Processing ticket: {ticket.Title} (Severity: {ticket.Severity})");
            return ticket;
        }

        public void EscalateTicket(string ticketId)
        {
            foreach (var kvp in tickets)
            {
                var ticketList = kvp.Value.ToList();
                var ticket = ticketList.FirstOrDefault(t => t.TicketId == ticketId);
                if (ticket != null)
                {
                    if (ticket.Severity == 1)
                    {
                        Console.WriteLine($"Ticket {ticketId} is already at highest severity");
                        return;
                    }

                    kvp.Value.Clear();
                    foreach (var t in ticketList.Where(t => t.TicketId != ticketId))
                        kvp.Value.Enqueue(t);

                    ticket.Severity--;
                    AddTicket(ticket);
                    Console.WriteLine($"Ticket {ticketId} escalated to severity {ticket.Severity}");
                    return;
                }
            }
            throw new TicketNotFoundException($"Ticket {ticketId} not found");
        }

        public void DisplayTickets()
        {
            Console.WriteLine("\n=== Support Tickets (By Severity) ===");
            foreach (var kvp in tickets)
            {
                Console.WriteLine($"\nSeverity {kvp.Key}:");
                foreach (var ticket in kvp.Value)
                    Console.WriteLine($"  - {ticket.TicketId}: {ticket.Title}");
            }
        }
    }
}
