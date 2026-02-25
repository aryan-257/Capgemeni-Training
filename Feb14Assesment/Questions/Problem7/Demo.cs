using System;

namespace Problem7.LibraryFine
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 7: Library Fine Calculation & Penalty ===\n");
            FineManager manager = new FineManager();

            try
            {
                var student1 = new StudentMember("M001", "Alice", "Computer Science");
                var student2 = new StudentMember("M002", "Bob", "Electronics");
                var faculty1 = new FacultyMember("M003", "Dr. Smith", "Professor");

                manager.AddFine(student1, 10);
                manager.AddFine(student2, 5);
                manager.AddFine(faculty1, 15);

                manager.DisplayFines();

                Console.WriteLine("\n--- Processing Payment ---");
                manager.PayFine("M001", 30);

                manager.DisplayFines();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
