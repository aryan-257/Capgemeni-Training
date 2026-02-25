using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabT10
{
    class Program
    {
        static void Main(string[] args)
        {
            //Employee array to hold the employees' information
            Employee[] Employees = new Employee[4];
            Employee emp = null;

            string EmpId, Name, ReportDate;
            double AllowancePercentage, Basic, HRA;
            int Role;

            //Accept employee information from the user
            Console.WriteLine("Enter employee information");

            for (int i = 0; i < Employees.Length; i++)
            {
                Console.WriteLine("Employee No : " + (i + 1));
                Console.Write("Id : ");
                EmpId = CustomConsole.ReadString();

                Console.Write("Name : ");
                Name = CustomConsole.ReadString();

                Console.Write("Basic : ");
                Basic = CustomConsole.ReadDouble();

                Console.Write("HRA : ");
                HRA = CustomConsole.ReadDouble();

                Console.Write("Percentage of Allowance : ");
                AllowancePercentage = CustomConsole.ReadDouble();

                Console.WriteLine("Enter Role Id : ");
                Console.WriteLine(Roles.DEVELOPER + ". " + RoleBuilder.GetRoleDescription(Roles.DEVELOPER));
                Console.WriteLine(Roles.TEST_ENGINEER + ". " + RoleBuilder.GetRoleDescription(Roles.TEST_ENGINEER));
                Console.WriteLine(Roles.SR_DEVELOPER + ". " + RoleBuilder.GetRoleDescription(Roles.SR_DEVELOPER));
                Console.WriteLine(Roles.DESIGNER + ". " + RoleBuilder.GetRoleDescription(Roles.DESIGNER));
                Role = CustomConsole.ReadInt();

                //Write your code here

                
            }

            Console.Write("Enter the date of the report (dd/mm/yyyy) : ");
            ReportDate = CustomConsole.ReadString();

            //Write your code here

            Console.ReadLine();
        }
    }
}
