using Microsoft.JSInterop;
using System;
namespace MVC_Core_WebApp1.Models
{
    public class StudentRepo : IRepo<Student>
    {
        public static List<Student> studList = null;

        public StudentRepo()
        {
            if(studList == null)
            {
                //collection initilizer
                studList = new List<Student>()
                {
                    new Student(){RollNo = 101, Name = "Alok", Age = 22, Gender = "Male", Address = "Pune"},
                    new Student(){RollNo = 102,Name = "Riya", Age = 21, Gender = "Female", Address = "Thane"},
                };
            }
        }
        public bool AddData(Student obj)
        {
            bool flag = false;
            if(obj!=null)
            {
                studList.Add(obj);
                flag = true;
            }
            else
            {
                throw new NullReferenceException("Object is not defined");
            }
            return flag;
        }

        public bool DeleteData(int id)
        {
            bool flag = false;
            Student student = studList.Find(x => x.RollNo == id);
            if (student != null)
            {
                studList.Remove(student);
                flag = true;
            }
            return flag;
        }

        public List<Student> ShowAllData()
        {
            return studList;
        }

        public Student ShowDetailsByID(int id)
        {
            Student student = studList.Find(x => x.RollNo == id);
            return student;
        }

        public bool UpdateData(int id, Student obj)
        {
            bool flag = false;
            Student student = studList.Find(x => x.RollNo == id);
            if(obj!=null)
            {
                student.Name = obj.Name;
                student.Address = obj.Address;
                student.Gender = obj.Gender;
                student.Age = obj.Age;
                flag = true;
            }
            return flag;
        }
    }
}
