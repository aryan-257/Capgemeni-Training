using System;

namespace Problem9.ITSupport
{
    public class SupportTicket
    {
        public string TicketId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Severity { get; set; }
        public DateTime CreatedAt { get; set; }
        public User ReportedBy { get; set; }

        public SupportTicket(string id, string title, string desc, int severity, User user)
        {
            TicketId = id;
            Title = title;
            Description = desc;
            Severity = severity;
            ReportedBy = user;
            CreatedAt = DateTime.Now;
        }
    }
}
