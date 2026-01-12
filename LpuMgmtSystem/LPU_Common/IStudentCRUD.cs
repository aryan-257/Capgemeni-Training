using System;
using LPU_Entity;

namespace LPU_Common

{
    public interface IStudentCRUD
    {
        Student SearchStudentID(int rollNo);
        List<Student> SearchStudentByName(string name );
        bool EnrollStudent(Student sObj);
        bool UpdateStudentDetails(Student sObj);
        bool DropStudent(int id);

    }
}
