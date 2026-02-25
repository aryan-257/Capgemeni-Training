using System;

namespace Problem5.UniversityCourse
{
    public abstract class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }

        protected Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public abstract void DisplayInfo();
    }

    public class Student : Person
    {
        public string StudentId { get; set; }
        public double GPA { get; set; }
        public string Major { get; set; }

        public Student(string id, string name, int age, double gpa, string major)
            : base(name, age)
        {
            if (gpa < 0.0 || gpa > 10.0)
                throw new InvalidGPAException("GPA must be between 0.0 and 10.0");

            StudentId = id;
            GPA = gpa;
            Major = major;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Student: {Name}, GPA: {GPA:F2}, Major: {Major}");
        }
    }
}
