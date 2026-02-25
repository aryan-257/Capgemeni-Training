using System;

namespace Problem7.LibraryFine
{
    public abstract class LibraryUser
    {
        public string MemberId { get; set; }
        public string Name { get; set; }
        public decimal OutstandingFine { get; set; }

        protected LibraryUser(string id, string name)
        {
            MemberId = id;
            Name = name;
            OutstandingFine = 0;
        }

        public abstract decimal CalculateFine(int daysOverdue);
        public abstract string GetMemberType();
    }

    public class StudentMember : LibraryUser
    {
        public string Department { get; set; }

        public StudentMember(string id, string name, string dept)
            : base(id, name)
        {
            Department = dept;
        }

        public override decimal CalculateFine(int daysOverdue)
        {
            return daysOverdue * 5m;
        }

        public override string GetMemberType() => "Student";
    }

    public class FacultyMember : LibraryUser
    {
        public string Designation { get; set; }

        public FacultyMember(string id, string name, string designation)
            : base(id, name)
        {
            Designation = designation;
        }

        public override decimal CalculateFine(int daysOverdue)
        {
            return daysOverdue * 3m;
        }

        public override string GetMemberType() => "Faculty";
    }
}
