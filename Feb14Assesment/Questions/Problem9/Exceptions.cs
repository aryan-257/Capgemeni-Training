using System;

namespace Problem9.ITSupport
{
    public class TicketNotFoundException : Exception
    {
        public TicketNotFoundException(string message) : base(message) { }
    }

    public class InvalidPriorityException : Exception
    {
        public InvalidPriorityException(string message) : base(message) { }
    }
}
