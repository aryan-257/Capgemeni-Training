// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Net;
using QuickMartTraders;

namespace QuickMartTraders
{
        class Program
        {
            static void Main(string[] args)
            {
                bool exit = false;

                while (!exit)
            {
                Console.WriteLine("================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction (Enter Purchase & Selling Details)");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                    CreateTransaction();
                    break;

                    case "2":
                    ViewLastTransaction();
                    break;

                    case "3":
                    RecalculateProfitLoss();
                    break;

                    case "4":
                    Console.WriteLine("Thank you. Application closed  normally.");
                    exit = true;
                    break;

                    default:
                    Console.WriteLine("Invalid option. Please enter a valid choice.");
                    break;

                }

                
            }
                
            }

            static void CreateTransaction()
        {
            SaleTransaction st = new SaleTransaction();

            Console.Write("Enter Invoice No: ");
            st.InvoiceNo = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(st.InvoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            st.CustomerName = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            st.ItemName = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            int qty;
            if (!int.TryParse(Console.ReadLine(), out qty) || qty <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }

            st.Quantity = qty;

            Console.Write("Enter Purchase Amount (total): ");
            decimal purchaseAmt;
            if (!decimal.TryParse(Console.ReadLine(), out purchaseAmt) || purchaseAmt <= 0)
            {
                Console.WriteLine("Purchase Amount must be greater than 0.");
                return;
            }

            st.PurchaseAmount = purchaseAmt;
            Console.Write("Enter Selling Amount (total): ");
            decimal sellingAmt;
            if (!decimal.TryParse(Console.ReadLine(), out sellingAmt) || sellingAmt <= 0)
            {
                Console.WriteLine("Selling Amount must be greater than 0.");
                return;
            }

            st.SellingAmount = sellingAmt;
            st.CalculateProfitOrLoss();

            SaleTransaction.LastTransaction = st;
            SaleTransaction.HasLastTransaction = true;

            Console.WriteLine();
            Console.WriteLine("Transaction saved successfully.");
            Console.WriteLine("Status: " + st.ProfitOrLossStatus);
            Console.WriteLine("Profit/Loss Amount: " + st.ProfitOrLossAmount.ToString("0.00"));
            Console.WriteLine("Profit Margin Percent: " + st.ProfitMarginPercent.ToString("0.00"));
            Console.WriteLine("---------------------------------------------------");

        }

        static void ViewLastTransaction()
        {
            if (!SaleTransaction.HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            SaleTransaction.LastTransaction.PrintTransaction();
        }
        static void RecalculateProfitLoss()
        {
            if (!SaleTransaction.HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            SaleTransaction.LastTransaction.CalculateProfitOrLoss();
            SaleTransaction.LastTransaction.PrintTransaction();
        }
    }
}
