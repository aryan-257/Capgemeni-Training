using System;

namespace Problem6.FlashSale
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 6: E-Commerce Flash Sale Bidding Engine ===\n");
            AuctionEngine auction = new AuctionEngine(1000);

            try
            {
                var buyer1 = new Buyer("B001", "John", "john@email.com", 50000);
                var buyer2 = new Buyer("B002", "Sarah", "sarah@email.com", 60000);
                var buyer3 = new Buyer("B003", "Mike", "mike@email.com", 40000);

                auction.AddBid(new Bid("BID001", buyer1, 15000, "PROD001"));
                auction.AddBid(new Bid("BID002", buyer2, 18000, "PROD001"));
                auction.AddBid(new Bid("BID003", buyer3, 12000, "PROD001"));
                auction.AddBid(new Bid("BID004", buyer1, 20000, "PROD001"));

                auction.DisplayBids();
                auction.CloseAuction();
                auction.DetermineWinner();

                Console.WriteLine("\n--- Testing Bid After Auction Close ---");
                auction.AddBid(new Bid("BID005", buyer2, 25000, "PROD001"));
            }
            catch (AuctionClosedException ex)
            {
                Console.WriteLine($"AUCTION ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
