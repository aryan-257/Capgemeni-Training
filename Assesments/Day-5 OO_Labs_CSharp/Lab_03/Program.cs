using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee Emp = new Employee();
            Address adr = new Address();
            Emp.EmpAddress = adr;
            StoreData(Emp);

            ShowData(Emp);
        }

        static void StoreData(Employee Emp) // Don't change the signature of the method
        {
            // Use CustomConsole class to accept address and employee properties
            Emp.EmployeeId = CustomConsole.ReadInt();
            Emp.Name = CustomConsole.ReadString();
            Emp.Gender = CustomConsole.ReadChar();
            Emp.EmpAddress.Address1 = CustomConsole.ReadString();
            Emp.EmpAddress.Address2 = CustomConsole.ReadString();
         }

        static void ShowData(Employee Emp) // Don't change the signature of the method
        {

            //----------------Display the employee information
            Console.WriteLine("Employee Id : " + );
            Console.WriteLine("Employee Name : ");
            Console.WriteLine("Employee Gender : ");

            Console.WriteLine("Employee Address : --------------");
            Console.WriteLine("Address 1 : ");
            Console.WriteLine("Address 2 : ");
            Console.WriteLine("City : ");
            Console.WriteLine("PinCode : ");
            Console.WriteLine("----------------------------------");

            Console.ReadLine();
        }
    }
}
