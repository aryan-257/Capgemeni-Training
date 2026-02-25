using System;

namespace Problem6.FlashSale
{
    public class BidTooLowException : Exception
    {
        public BidTooLowException(string message) : base(message) { }
    }

    public class AuctionClosedException : Exception
    {
        public AuctionClosedException(string message) : base(message) { }
    }
}
