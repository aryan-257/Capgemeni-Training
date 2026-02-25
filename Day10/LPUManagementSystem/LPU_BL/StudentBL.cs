using LPU_Common;
using LPU_DAL;
using LPU_Entity;
using LPU_Exceptions;

namespace LPU_BL
{
    public class StudentBL : IStudentCRUD
    {
        StudentDao sDao = null;

        public StudentBL()
        {
            sDao = new StudentDao();
        }
        public bool DropStudentDetails(int id)
        {
            throw new NotImplementedException();
        }

        public bool EnrollStudent(Student sObj)
        {
            return sDao.EnrollStudent(sObj);
        }

        public Student SearchStudentByID(int rollNo)
        {
            Student s1 = null;
            try
            {
                s1 = sDao.SearchStudentByID(rollNo);
            }
            catch(LpuException e)
            {
                throw e;
            }
            return s1;
        }

        public List<Student> SearchStudentByName(string name)
        {
            return sDao.SearchStudentByName(name);
        }

        public bool UpdateStudentDetails(int id, Student newObj)
        {
            throw new NotImplementedException();
        }
    }
}
