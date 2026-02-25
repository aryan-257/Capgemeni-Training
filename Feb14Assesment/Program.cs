using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("╔════════════════════════════════════════════════════════════╗");
        Console.WriteLine("║   SortedDictionary Practice - 10 Real-World Problems      ║");
        Console.WriteLine("╚════════════════════════════════════════════════════════════╝\n");

        while (true)
        {
            Console.WriteLine("\nSelect a problem to run (1-10) or 0 to exit:");
            Console.WriteLine("1. Smart Warehouse Inventory Prioritization");
            Console.WriteLine("2. Hospital Emergency Queue Management");
            Console.WriteLine("3. Banking Transaction Risk Monitoring");
            Console.WriteLine("4. Airline Booking Fare Classification");
            Console.WriteLine("5. University Course Registration Priority");
            Console.WriteLine("6. E-Commerce Flash Sale Bidding Engine");
            Console.WriteLine("7. Library Fine Calculation & Penalty");
            Console.WriteLine("8. Smart Traffic Violation Monitoring");
            Console.WriteLine("9. IT Support Ticket Severity System");
            Console.WriteLine("10. Investment Portfolio Risk Categorization");
            Console.WriteLine("11. Run All Problems");
            Console.WriteLine("0. Exit");

            Console.Write("\nYour choice: ");
            string choice = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case "1":
                        Problem1.SmartWarehouse.Demo.Run();
                        break;
                    case "2":
                        Problem2.HospitalEmergency.Demo.Run();
                        break;
                    case "3":
                        Problem3.BankingRisk.Demo.Run();
                        break;
                    case "4":
                        Problem4.AirlineBooking.Demo.Run();
                        break;
                    case "5":
                        Problem5.UniversityCourse.Demo.Run();
                        break;
                    case "6":
                        Problem6.FlashSale.Demo.Run();
                        break;
                    case "7":
                        Problem7.LibraryFine.Demo.Run();
                        break;
                    case "8":
                        Problem8.TrafficViolation.Demo.Run();
                        break;
                    case "9":
                        Problem9.ITSupport.Demo.Run();
                        break;
                    case "10":
                        Problem10.InvestmentPortfolio.Demo.Run();
                        break;
                    case "11":
                        RunAll();
                        break;
                    case "0":
                        Console.WriteLine("\nThank you for using the practice system!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid choice. Please try again.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected Error: {ex.Message}");
            }

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void RunAll()
    {
        Problem1.SmartWarehouse.Demo.Run();
        Problem2.HospitalEmergency.Demo.Run();
        Problem3.BankingRisk.Demo.Run();
        Problem4.AirlineBooking.Demo.Run();
        Problem5.UniversityCourse.Demo.Run();
        Problem6.FlashSale.Demo.Run();
        Problem7.LibraryFine.Demo.Run();
        Problem8.TrafficViolation.Demo.Run();
        Problem9.ITSupport.Demo.Run();
        Problem10.InvestmentPortfolio.Demo.Run();
    }
}
