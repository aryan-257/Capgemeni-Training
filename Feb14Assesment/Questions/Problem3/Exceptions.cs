using System;

namespace Problem3.BankingRisk
{
    public class FraudDetectedException : Exception
    {
        public FraudDetectedException(string message) : base(message) { }
    }

    public class NegativeAmountException : Exception
    {
        public NegativeAmountException(string message) : base(message) { }
    }

    public class TransactionLimitExceededException : Exception
    {
        public TransactionLimitExceededException(string message) : base(message) { }
    }
}
