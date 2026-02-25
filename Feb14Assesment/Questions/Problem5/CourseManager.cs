using System;
using System.Collections.Generic;
using System.Linq;

namespace Problem5.UniversityCourse
{
    public class CourseManager
    {
        private SortedDictionary<double, List<Student>> registrations = new SortedDictionary<double, List<Student>>(Comparer<double>.Create((a, b) => b.CompareTo(a)));
        private HashSet<string> enrolledStudents = new HashSet<string>();
        private int maxSeats;
        private int currentEnrollment = 0;

        public CourseManager(int seats)
        {
            maxSeats = seats;
        }

        public void RegisterStudent(Student student)
        {
            if (currentEnrollment >= maxSeats)
                throw new CourseFullException("Course is full, no more seats available");

            if (enrolledStudents.Contains(student.StudentId))
                throw new DuplicateEnrollmentException($"Student {student.Name} is already enrolled");

            if (!registrations.ContainsKey(student.GPA))
                registrations[student.GPA] = new List<Student>();

            registrations[student.GPA].Add(student);
            enrolledStudents.Add(student.StudentId);
            currentEnrollment++;
            Console.WriteLine($"Registered {student.Name} (GPA: {student.GPA:F2}) - Priority based on GPA");
        }

        public void AllocateSeats()
        {
            Console.WriteLine("\n=== Seat Allocation (Priority Order) ===");
            int seatNumber = 1;
            foreach (var kvp in registrations)
            {
                foreach (var student in kvp.Value)
                {
                    Console.WriteLine($"Seat {seatNumber++}: {student.Name} (GPA: {student.GPA:F2})");
                }
            }
        }

        public void DisplayRegistrations()
        {
            Console.WriteLine($"\n=== Course Registrations ({currentEnrollment}/{maxSeats} seats filled) ===");
            foreach (var kvp in registrations)
            {
                Console.WriteLine($"\nGPA {kvp.Key:F2}:");
                foreach (var student in kvp.Value)
                    Console.WriteLine($"  - {student.Name} ({student.Major})");
            }
        }
    }
}
