using LPU_Common;
using LPU_Entity;
using LPU_Exceptions;

namespace LPU_DAL
{
    /// <summary>
    /// Student Dao class is used for CRUD operations
    /// </summary>
    public class StudentDao : IStudentCRUD
    {

        static List<Student> studentList = null;

        public StudentDao()
        {
            //Collection Initializer
            studentList = new List<Student>()
            {
                new Student(){Id = 101,Name = "Manish", Course = CourseType.CSE, Address = "Pilibhit"},
                new Student(){Id = 102,Name = "Himanshu", Course = CourseType.CSE, Address = "Odhisa"},
                new Student(){Id = 103,Name = "Mahi", Course = CourseType.CSE, Address = "Moradabad"},
                new Student(){Id = 104,Name = "Krishma", Course = CourseType.CSE, Address = "Punjab"},
                new Student(){Id = 1011,Name = "Manish", Course = CourseType.CSE, Address = "Delhi"},
                new Student(){Id = 102,Name = "Himanshu", Course = CourseType.CSE, Address = "Hyderbad"},
                new Student(){Id = 103,Name = "Mahi", Course = CourseType.CSE, Address = "Moradabad"},
                new Student(){Id = 104,Name = "Krishma", Course = CourseType.CSE, Address = "Punjab"},
                new Student(){Id = 1012,Name = "Manish", Course = CourseType.CSE, Address = "Jhansi"},
                new Student(){Id = 102,Name = "Himanshu", Course = CourseType.CSE, Address = "Odhisa"},
                new Student(){Id = 103,Name = "Mahi", Course = CourseType.CSE, Address = "Moradabad"},
                new Student(){Id = 104,Name = "Krishma", Course = CourseType.CSE, Address = "Punjab"}
            };
        }
        public bool DropStudentDetails(int id)
        {
            throw new NotImplementedException();
        }

        public bool EnrollStudent(Student sObj)
        {
            bool flag = false;
            if(sObj !=  null)
            {
                studentList.Add(sObj);
                flag = true;
            }
            return flag;
        }

        public Student SearchStudentByID(int rollNo)
        {
            Student myStudent = null;

            if (rollNo > 0)
            {
                myStudent = studentList.Find(s => s.Id == rollNo);
                if(myStudent == null)
                {
                    throw new LpuException("Student Record Not Found!");
                }
            }
            else
            {
                throw new LpuException("Error Generated...");
            }

            return myStudent;
        }

        public List<Student> SearchStudentByName(string name)
        {
            
            List<Student> data = studentList.FindAll(p => p.Name == name);
            return data;
        }

        public bool UpdateStudentDetails(int id, Student newObj)
        {
            throw new NotImplementedException();
        }
    }
}
