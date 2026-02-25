using System;
using System.Collections.Generic;
using System.Text;

namespace LINQ_ConsoleApp
{
    public class StudentRepo
    {
        static List<Student> studList = null;

        public StudentRepo()
        {
            if(studList == null)
            {
                studList = new List<Student>()
                {
                    new Student(){RollNo = 1,Name = "Manish",Gender = "Male",Marks = 99,Fees = 10000},
                    new Student(){RollNo = 2,Name = "Himanshu",Gender = "Female",Marks = 09,Fees = 20000},
                    new Student(){RollNo = 3,Name = "Pratham",Gender = "Male",Marks = 80,Fees = 30000},
                    new Student(){RollNo = 4,Name = "Mahi",Gender = "Female",Marks = 01,Fees = 40000},
                    new Student(){RollNo = 5,Name = "Krishma",Gender = "Female",Marks = 45,Fees = 50000},
                    new Student(){RollNo = 6,Name = "Devansh",Gender = "Male",Marks = 89,Fees = 60000}
                };
            }
        }

        public List<Student> GetAllStudents()
        {
            return studList;
        }
    }
}
