using System;

namespace Problem7.LibraryFine
{
    public class FineNotFoundException : Exception
    {
        public FineNotFoundException(string message) : base(message) { }
    }

    public class InvalidPaymentException : Exception
    {
        public InvalidPaymentException(string message) : base(message) { }
    }
}
