using System;

namespace SmartBankingSystem.Exceptions
{
    public class InvalidTransactionException : Exception
    {
        public InvalidTransactionException() : base()
        {
        }

        public InvalidTransactionException(string message) : base(message)
        {
        }

        public InvalidTransactionException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
