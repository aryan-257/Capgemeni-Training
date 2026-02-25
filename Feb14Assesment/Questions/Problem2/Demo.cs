using System;

namespace Problem2.HospitalEmergency
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 2: Hospital Emergency Queue System ===\n");
            EmergencyManager manager = new EmergencyManager();

            try
            {
                manager.AddPatient(new Patient("P001", "John", 45, 1, "Heart Attack"));
                manager.AddPatient(new Patient("P002", "Sarah", 30, 3, "Broken Arm"));
                manager.AddPatient(new Patient("P003", "Mike", 60, 1, "Stroke"));
                manager.AddPatient(new Patient("P004", "Emma", 25, 5, "Fever"));

                manager.DisplayQueue();

                Console.WriteLine("\n--- Treating Patients ---");
                var patient1 = manager.GetNextPatient();
                patient1.Treat();

                var patient2 = manager.GetNextPatient();
                patient2.Treat();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
