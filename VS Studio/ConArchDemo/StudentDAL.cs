using ConArchDemo;
using System;
using System.Collections.Generic;
using System.Data;
//For ADO .Net
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConArcDemo
{
    /// <summary>
    /// Demo Code for Connected Architecture in StudentDAL Class 
    /// </summary>
    public class StudentDAL
    {

        SqlConnection con = null;
        SqlCommand cmd = null;
        SqlDataReader sdr = null;

        public StudentDAL()
        {
            string conStr = "Data Source=.\\sqlexpress;Initial Catalog=LPU_Db;Integrated Security=True;Trust Server Certificate=True"; // old version mein integrated security = true kaam nhi krta usmein ssip kaam ata hai
            con = new SqlConnection();
            con.ConnectionString = "Server=.\\sqlexpress;Integrated Security=True;Database=LPU_DB;TrustServerCertificate=True;";
        }


        public List<Student> ShowAllStudents()
        {
            List<Student> studList = null;
            //Code for connected Architecture below

            try
            {
                cmd = new SqlCommand();
                cmd.CommandText = "Select * from StudentInfo";
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;

                //Holding Data via Reader
                cmd.ExecuteReader();
                while (sdr.Read())
                {
                    Console.WriteLine();
                }
            catch (Exception e)
            {

            }

            return studList;
        }



        public List<Student> SearchByName(string Name)
        {
            List<Student> studList = null;

            return studList;
        }


        public Student SearchByID(int ID)
        {
            Student student = null;


            return student;

        }



    }
}