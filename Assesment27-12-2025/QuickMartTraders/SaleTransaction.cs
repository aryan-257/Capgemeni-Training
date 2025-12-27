using System;

namespace QuickMartTraders
{

    public class SaleTransaction
    {
        public string InvoiceNo { get; set; }
        public string CustomerName { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal PurchaseAmount { get; set; }
        public decimal SellingAmount { get; set; }
        public string ProfitOrLossStatus { get; set; }
        public decimal ProfitOrLossAmount { get; set; }
        public decimal ProfitMarginPercent { get; set;}

        public static SaleTransaction LastTransaction;
        public static bool HasLastTransaction = false;

        public void CalculateProfitOrLoss()
        {
            if (SellingAmount > PurchaseAmount)
            {
                ProfitOrLossStatus = "Profit";
                ProfitOrLossAmount = SellingAmount - PurchaseAmount;
                
            }
            else if (SellingAmount < PurchaseAmount)
            {
                ProfitOrLossStatus = "Loss";
                ProfitOrLossAmount = PurchaseAmount - SellingAmount;
                
            }
            else
            {
                ProfitOrLossStatus = "No Profit No Loss";
                ProfitOrLossAmount = 0;
                
            }

            ProfitMarginPercent = (ProfitOrLossAmount / PurchaseAmount) * 100;
        }

        public void PrintTransaction()
        {
            Console.WriteLine("Last Transaction");
            Console.WriteLine("Invoice No: " + InvoiceNo);
            Console.WriteLine("Customer: " + CustomerName);
            Console.WriteLine("Item: " + ItemName);
            Console.WriteLine("Quantity: " + Quantity);
            Console.WriteLine("Purchase Amount: " + PurchaseAmount.ToString("0.00"));
            Console.WriteLine("Selling Amount: " + SellingAmount.ToString("0.00"));
            Console.WriteLine("Status: " + ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + ProfitOrLossAmount.ToString("0.00"));
            Console.WriteLine("Profit Margin (%): " + ProfitMarginPercent.ToString("0.00") + "%");
            Console.WriteLine("------------------------------");
            
        }
    }


}
