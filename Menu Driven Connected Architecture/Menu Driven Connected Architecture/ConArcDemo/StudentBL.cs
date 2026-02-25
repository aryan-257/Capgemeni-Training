using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConArcDemo
{
    public class StudentBL
    {
        StudentDAO dao = new StudentDAO();

        public bool Insert(Student s)
        {
            return dao.AddStudent(s);
        }

        public List<Student> SelectAll()
        {
            return dao.ShowAllStudents();
        }

        public Student SelectByID(int id)
        {
            return dao.ShowStudentByID(id);
        }

        public List<Student> SelectByName(string name)
        {
            return dao.ShowStudentByName(name);
        }

        public bool UpdateAddress(int id,string address)
        {
            return dao.UpdateStudentAddress(id,address);
        }

        public bool Delete(int id)
        {
            return dao.DeleteStudent(id);
        }
    }
}
