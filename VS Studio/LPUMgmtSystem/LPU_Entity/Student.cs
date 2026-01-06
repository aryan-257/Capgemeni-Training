using System;

namespace LPU_Entity
{
    public enum CourseType
    {
        Mechenical=10,
        Electrical=20,
        Civil=30,
        CSE=40,
        IT=50
    }


    public class Student
    {
        public int StudentId { get; set; }
        public string Name { get; set; }
        public CourseType Course { get; set; }//enum type property
        public string Address { get; set; }

    }
}
