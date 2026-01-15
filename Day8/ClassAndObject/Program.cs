// See https://aka.ms/new-console-template for more information
using ClassAndObject;

class Program
{
    static void Main()
    {
        Employees emp = new Employees();

        emp.EmpId = 101;
        emp.EmpName = "Preeti";      
        emp.EmpDesig = "developer";  
        emp.EmpDept = "C2";         

        Console.WriteLine("\nEmployee Details:");
        Console.WriteLine("ID: " + emp.EmpId);
        Console.WriteLine("Name: " + emp.EmpName);
        Console.WriteLine("Designation: " + emp.EmpDesig);
        Console.WriteLine("Department: " + emp.EmpDept);
    }
}

