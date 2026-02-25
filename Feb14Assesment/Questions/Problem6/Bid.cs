using System;

namespace Problem6.FlashSale
{
    public class Bid
    {
        public string BidId { get; set; }
        public Buyer Bidder { get; set; }
        public decimal Amount { get; set; }
        public DateTime Timestamp { get; set; }
        public string ProductId { get; set; }

        public Bid(string id, Buyer bidder, decimal amount, string productId)
        {
            BidId = id;
            Bidder = bidder;
            Amount = amount;
            ProductId = productId;
            Timestamp = DateTime.Now;
        }
    }
}
