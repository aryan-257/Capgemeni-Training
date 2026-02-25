using System;

namespace Problem2.HospitalEmergency
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

    public class Patient : Person
    {
        public string PatientId { get; set; }
        public int Severity { get; set; }
        public string Condition { get; set; }

        public Patient(string patientId, string name, int age, int severity, string condition)
            : base(name, age)
        {
            PatientId = patientId;
            Severity = severity;
            Condition = condition;
        }

        public override void DisplayInfo()
        {
            Console.WriteLine($"Patient: {Name}, Age: {Age}, Severity: {Severity}, Condition: {Condition}");
        }

        public virtual void Treat()
        {
            Console.WriteLine($"Treating patient {Name} for {Condition}");
        }
    }
}
