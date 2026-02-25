using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem6.FlashSale
{
    public class AuctionEngine
    {
        private SortedDictionary<decimal, List<Bid>> bids = new SortedDictionary<decimal, List<Bid>>(Comparer<decimal>.Create((a, b) => b.CompareTo(a)));
        private decimal minimumBid;
        private bool isAuctionOpen = true;

        public AuctionEngine(decimal minBid)
        {
            minimumBid = minBid;
        }

        public void AddBid(Bid bid)
        {
            if (!isAuctionOpen)
                throw new AuctionClosedException("Auction is closed, no more bids accepted");

            if (bid.Amount < minimumBid)
                throw new BidTooLowException($"Bid amount ₹{bid.Amount} is below minimum ₹{minimumBid}");

            if (!bid.Bidder.ValidateBid(bid.Amount))
                throw new BidTooLowException($"Insufficient wallet balance for bid amount ₹{bid.Amount}");

            if (!bids.ContainsKey(bid.Amount))
                bids[bid.Amount] = new List<Bid>();

            bids[bid.Amount].Add(bid);
            Console.WriteLine($"Bid placed: ₹{bid.Amount} by {bid.Bidder.Name}");
        }

        public Bid DetermineWinner()
        {
            if (bids.Count == 0)
                return null;

            var highestBid = bids.First().Value.First();
            Console.WriteLine($"\n🏆 Winner: {highestBid.Bidder.Name} with bid of ₹{highestBid.Amount}");
            return highestBid;
        }

        public void CloseAuction()
        {
            isAuctionOpen = false;
            Console.WriteLine("\n--- Auction Closed ---");
        }

        public void DisplayBids()
        {
            Console.WriteLine("\n=== Current Bids (Highest First) ===");
            foreach (var kvp in bids)
            {
                Console.WriteLine($"\nBid Amount: ₹{kvp.Key}");
                foreach (var bid in kvp.Value)
                    Console.WriteLine($"  - {bid.Bidder.Name} at {bid.Timestamp:HH:mm:ss}");
            }
        }
    }
}
