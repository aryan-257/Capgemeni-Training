using System;

namespace Problem10.InvestmentPortfolio
{
    public class InvalidRiskRatingException : Exception
    {
        public InvalidRiskRatingException(string message) : base(message) { }
    }

    public class InvestmentLimitExceededException : Exception
    {
        public InvestmentLimitExceededException(string message) : base(message) { }
    }
}
