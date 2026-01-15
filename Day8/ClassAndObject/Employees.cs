using System;
using System.Collections.Generic;
using System.Text;

namespace ClassAndObject
{
    public class Employees
    {
        int empId;
        string? empName;
        string? empDesig;
        string? empDept;



        public int EmpId
        {
            get { return empId; }
            set { empId = value; }
        }

        public string EmpName
        {
            get { return empName; }
            set
            {
                if (value == null)
                {
                    Console.WriteLine("Error: Employee name cannot be null");
                }
                else
                {
                    empName = value;
                }
            }
        }

        public string EmpDesig
        {
            get { return empDesig; }
            set
            {
                if (value == "developer" || value == "tester" ||
                    value == "Lead" || value == "manager")
                {
                    empDesig = value;
                }
                else
                {
                    Console.WriteLine("Invalid designation");
                }
            }
        }

        public string EmpDept
        {
            get { return empDept; }
            set
            {
                if (value == "C2" || value == "TTG" ||
                    value == "ITES" || value == "PES")
                {
                    empDept = value;
                }
                else
                {
                    Console.WriteLine("Invalid Dept");
                }
            }
        }

    }
}

