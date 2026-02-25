using System;

namespace SmartBankingSystem.Exceptions
{
    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException() : base()
        {
        }

        public MinimumBalanceException(string message) : base(message)
        {
        }

        public MinimumBalanceException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }
    }
}
