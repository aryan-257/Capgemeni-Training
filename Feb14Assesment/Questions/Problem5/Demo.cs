using System;

namespace Problem5.UniversityCourse
{
    public class Demo
    {
        public static void Run()
        {
            Console.WriteLine("\n=== Problem 5: University Course Registration Priority ===\n");
            CourseManager course = new CourseManager(5);

            try
            {
                course.RegisterStudent(new Student("S001", "Alice", 20, 9.2, "Computer Science"));
                course.RegisterStudent(new Student("S002", "Bob", 21, 8.5, "Electronics"));
                course.RegisterStudent(new Student("S003", "Charlie", 19, 9.8, "Computer Science"));
                course.RegisterStudent(new Student("S004", "Diana", 22, 7.9, "Mechanical"));

                course.DisplayRegistrations();
                course.AllocateSeats();

                Console.WriteLine("\n--- Testing Duplicate Enrollment ---");
                course.RegisterStudent(new Student("S001", "Alice", 20, 9.2, "Computer Science"));
            }
            catch (DuplicateEnrollmentException ex)
            {
                Console.WriteLine($"ENROLLMENT ERROR: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR: {ex.Message}");
            }
        }
    }
}
