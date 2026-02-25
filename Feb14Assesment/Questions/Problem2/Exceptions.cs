using System;

namespace Problem2.HospitalEmergency
{
    public class InvalidSeverityException : Exception
    {
        public InvalidSeverityException(string message) : base(message) { }
    }

    public class PatientNotFoundException : Exception
    {
        public PatientNotFoundException(string message) : base(message) { }
    }

    public class QueueOverflowException : Exception
    {
        public QueueOverflowException(string message) : base(message) { }
    }
}
