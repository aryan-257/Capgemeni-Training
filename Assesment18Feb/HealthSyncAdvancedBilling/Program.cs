using System;

namespace HealthSyncAdvancedBilling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== HealthSync Advanced Billing System ===\n");

            // Scenario 1: In-House Consultant (High Earner)
            try
            {
                InHouseConsultant inHouse = new InHouseConsultant("DR2001", "Dr. Smith", 10000);
                inHouse.DisplayPayoutDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine();

            // Scenario 2: Visiting Consultant
            try
            {
                VisitingConsultant visiting = new VisitingConsultant("DR8005", "Dr. Johnson", 10, 600);
                visiting.DisplayPayoutDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine();

            // Scenario 3: Validation Failure
            try
            {
                InHouseConsultant invalid = new InHouseConsultant("MD1001", "Dr. Invalid", 5000);
                invalid.DisplayPayoutDetails();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
