using System;

namespace Problem8.TrafficViolation
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 8: Smart Traffic Violation Monitoring ===\n");
            TrafficMonitor monitor = new TrafficMonitor();

            try
            {
                var car1 = new Car("KA01AB1234", "Toyota", "John");
                var bike1 = new Bike("KA02CD5678", "Honda", "Sarah");
                var truck1 = new Truck("KA03EF9012", "Tata", "Mike");

                monitor.AddViolation(new Violation("V001", car1, "Speeding", 2000));
                monitor.AddViolation(new Violation("V002", bike1, "No Helmet", 500));
                monitor.AddViolation(new Violation("V003", truck1, "Overloading", 5000));
                monitor.AddViolation(new Violation("V004", car1, "Red Light", 1000));

                monitor.DisplayViolations();

                Console.WriteLine("\n--- Repeat Offenders ---");
                monitor.EscalateRepeatOffenders();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
