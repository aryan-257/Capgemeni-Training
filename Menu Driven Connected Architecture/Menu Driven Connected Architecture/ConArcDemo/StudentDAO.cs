using ConArcDemo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ConArcDemo
{
    /// <summary>
    /// Demo for connected ARCHITECTURE in DAO class
    /// </summary>
    public class StudentDAO
    {
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataReader sdr = null;

        public StudentDAO()
        {
            conn = new SqlConnection();
            conn.ConnectionString = "Server=.\\Sqlexpress;Integrated Security = SSPI;Database = LPU_DB;TrustServerCertificate=true";
        }
        public List<Student> ShowAllStudents()
        {
            List<Student> studList = new List<Student>();
            //Code for Connected Architecture below
            try
            {
                conn.Open();

                cmd = new SqlCommand();
                cmd.CommandText = "Select * from StudentInfo";
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                //holding data via reader(forward only control)
                sdr = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(sdr);

                //convert Table into List
                
                foreach (DataRow drow in dt.Rows)
                {
                    Student student = new Student()
                    {
                        RollNo = Convert.ToInt32(drow[0].ToString()),
                        Name = drow[1].ToString(),
                        Address = drow[2].ToString(),
                        Age = Convert.ToInt32(drow[3])
                    };
                    if (student != null)
                    {
                        studList.Add(student);
                    }

                }

            }
            finally
            {
                conn.Close();
            }

            return studList;
        }

        public List<Student> ShowStudentByName(string name)
        {
            List<Student> studList = new List<Student>();
            SqlParameter param1 = new SqlParameter("@Name",name);
            //Code for Connected Architecture below
            try
            {
                conn.Open();

                cmd = new SqlCommand();
                cmd.CommandText = $"Select * from StudentInfo where StudentName = @Name";
                                // "Select * from StudentInfo where StudentName = '"+name+"'";
                                // $"Select * from StudentInfo where StudentName = '{name}'";

                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                //Parameter is to be added to the command
                cmd.Parameters.Add(param1);


                //holding data via reader(forward only control)
                sdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(sdr);

                //convert Table into List
                
                foreach (DataRow drow in dt.Rows)
                {
                    Student student = new Student()
                    {
                        RollNo = Convert.ToInt32(drow[0].ToString()),
                        Name = drow[1].ToString(),
                        Address = drow[2].ToString(),
                        Age = Convert.ToInt32(drow[3].ToString()),
                    };
                    if (student != null)
                    {
                        studList.Add(student);
                    }

                }
            }
            finally
            {
                conn.Close();
            }

            return studList;
        }

        public Student ShowStudentByID(int rollNo)
        {
            Student student = null;

            try
            {
                conn.Open();

                cmd = new SqlCommand(
                "Select * from StudentInfo where StudentID=@id",
                conn);

                cmd.Parameters.AddWithValue("@id", rollNo);

                sdr = cmd.ExecuteReader();

                if (sdr.Read())
                {
                    student = new Student()
                    {
                        RollNo = Convert.ToInt32(sdr[0]),
                        Name = sdr[1].ToString(),
                        Address = sdr[2].ToString(),
                        Age = Convert.ToInt32(sdr[3])
                    };
                }
            }
            finally
            {
                conn.Close();
            }

            return student;
        }

        public bool AddStudent(Student student)
        {
            bool flag = false;

            try
            {
                conn.Open();

                SqlParameter[] param2 = new SqlParameter[4];

                param2[0] = new SqlParameter("@RollNo", student.RollNo);
                param2[1] = new SqlParameter("@Name", student.Name);
                param2[2] = new SqlParameter("@Address", student.Address);
                param2[3] = new SqlParameter("@Age", student.Age);

                cmd = new SqlCommand();

                cmd.Connection = conn;

                cmd.CommandType = CommandType.Text;

                cmd.CommandText ="Insert into StudentInfo(StudentID,StudentName,StudentAddress,Age) values(@RollNo,@Name,@Address,@Age)";

                cmd.Parameters.AddRange(param2);

                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                    flag = true;
            }
            finally
            {
                conn.Close();
            }

            return flag;
        }

        public bool DeleteStudent(int Id)
        {
            SqlParameter paramId = new SqlParameter("@ID", Id);
            try
            {
                conn.Open();

                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "Delete from StudentInfo where StudentID = @ID";

                cmd.Parameters.Add(paramId);

                int rowsAffected = cmd.ExecuteNonQuery();

                return rowsAffected > 0;
            }
            finally
            {
                conn.Close();
            }
        }

        public bool UpdateStudentAddress(int id,string address)
        {
            SqlParameter[] paramID = new SqlParameter[2];

            paramID[0] = new SqlParameter("@ID", id);
            paramID[1] = new SqlParameter("@Address", address);
            try
            {
                conn.Open();

                cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandType = CommandType.Text;

                cmd.CommandText = "Update StudentInfo set StudentAddress=@Address where StudentID=@ID";
                
                cmd.Parameters.AddRange(paramID);

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0;
            }
            finally
            {
                conn.Close(); 
            }
        }
    }
}