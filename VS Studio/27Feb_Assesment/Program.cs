using System;
using System.Collections.Generic;
using System.Linq;
using EmployeeApp;

namespace EmployeeApp
{
 class Program
 {
  static void Main(string[] args)
  {

   List<Employee> empList = new List<Employee>()
   {
    new Employee { EmployeeID = 1001 , FirstName="Malcolm", LastName="Daruwalla", Title="Manager", DOB=new DateTime(1984,11,16), DOJ=new DateTime(2011,6,8), City="Mumbai"},
    new Employee { EmployeeID=1002, FirstName="Asdin", LastName="Dhalla", Title="AsstManager", DOB=new DateTime(1984,8,20), DOJ=new DateTime(2012,7,7), City="Mumbai"},
    new Employee { EmployeeID=1003, FirstName="Madhavi", LastName="Oza", Title="Consultant", DOB=new DateTime(1987,11,14), DOJ=new DateTime(2015,4,12), City="Pune"},
    new Employee { EmployeeID=1004, FirstName="Saba", LastName="Shaikh", Title="SE", DOB=new DateTime(1990,6,3), DOJ=new DateTime(2016,2,2), City="Pune"},
    new Employee { EmployeeID=1005, FirstName="Nazia", LastName="Shaikh", Title="SE", DOB=new DateTime(1991,3,8), DOJ=new DateTime(2016,2,2), City="Mumbai"},
    new Employee { EmployeeID=1006, FirstName="Amit", LastName="Pathak", Title="Consultant", DOB=new DateTime(1989,11,7), DOJ=new DateTime(2014,8,8), City="Chennai"},
    new Employee { EmployeeID=1007, FirstName="Vijay", LastName="Natrajan", Title="Consultant", DOB=new DateTime(1989,12,2), DOJ=new DateTime(2015,6,1), City="Mumbai"},
    new Employee { EmployeeID=1008, FirstName="Rahul", LastName="Dubey", Title="Associate", DOB=new DateTime(1993,11,11), DOJ=new DateTime(2014,11,6), City="Chennai"},
    new Employee { EmployeeID=1009, FirstName="Suresh", LastName="Mistry", Title="Associate", DOB=new DateTime(1992,8,12), DOJ=new DateTime(2014,12,3), City="Chennai"},
    new Employee { EmployeeID=1010, FirstName="Sumit", LastName="Shah", Title="Manager", DOB=new DateTime(1991,4,12), DOJ=new DateTime(2016,1,2), City="Pune"}
   };


    //a-part


   Console.WriteLine("Employee Management System\n");

   var allEmployees = empList.Select(e => e);
   foreach (var emp in allEmployees)
   {
     Console.WriteLine(emp.EmployeeID+" "+emp.FirstName+" "+emp.LastName+" "+emp.City);
   }

   //b-part

   Console.WriteLine("\nNot Mumbai Employees");
   var notMumbai = empList.Where(e => e.City != "Mumbai");
   foreach(var emp in notMumbai)
   {
    Console.WriteLine(emp.FirstName+" "+emp.LastName+" "+emp.City);
   }

   //c-part

   Console.WriteLine("\nAsstManager Employees");
   var asstManagers = empList.Where(e => e.Title=="AsstManager");
   foreach(var emp in asstManagers)
   {
    Console.WriteLine(emp.FirstName+" "+emp.LastName+" "+emp.Title);
   }

   //d-part


   Console.WriteLine("\nLast name starts with S");
   var lastNameS = empList.Where(e => e.LastName.StartsWith("S"));
   foreach(var emp in lastNameS)
   {
        Console.WriteLine(emp.FirstName+""+emp.LastName);
   }

    //e-part

   Console.WriteLine("\nJoined before 2015");
   var joinedBefore2015=empList.Where(e=>e.DOJ<new DateTime(2015,1,1));
   foreach(var emp in joinedBefore2015)
    {
        Console.WriteLine(emp.FirstName+""+emp.LastName+""+emp.DOJ.ToShortDateString());
    }

    //f-part
    Console.WriteLine("\nEmployees born after 1/1/1990");
    var dobAfter1990 = empList.Where(e => e.DOB > new DateTime(1990,1,1));
    foreach(var emp in dobAfter1990)
    {
        Console.WriteLine(emp.FirstName+" "+emp.LastName+" "+emp.DOB.ToShortDateString());
    }

    //g-part

    Console.WriteLine("\nEmployees with designation Consultant and Associate");
    var consultantsAndAssociates = empList.Where(e => e.Title == "Consultant" || e.Title == "Associate");
    foreach(var emp in consultantsAndAssociates)
    {
        Console.WriteLine(emp.FirstName+" "+emp.LastName+" "+emp.Title);
    }

    //h-part


    Console.WriteLine("\nTotal number of employees");
    int totalEmployees = empList.Count();

    Console.WriteLine("Total Employees : " + totalEmployees);

    //i-part

    Console.WriteLine("\nTotal employees from Chennai");
    int chennaiEmployees = empList.Count(e => e.City == "Chennai");
    Console.WriteLine("Total Chennai Employees : " + chennaiEmployees);

    //j-part

    Console.WriteLine("\nHighest Employee ID");
    int highestId = empList.Max(e => e.EmployeeID);
    Console.WriteLine("Highest ID : " + highestId);

    //k-part

    Console.WriteLine("\nEmployees joined after 1/1/2015");
    int joinedAfter2015 = empList.Count(e => e.DOJ > new DateTime(2015,1,1));
    Console.WriteLine("Total Employees : " + joinedAfter2015);

    //l-part

    Console.WriteLine("\nEmployees whose designation is not Associate");
    int notAssociate = empList.Count(e => e.Title != "Associate");
    Console.WriteLine("Total Employees : " + notAssociate);

    //m-part

    Console.WriteLine("\nTotal employees based on City");
    var empByCity = empList.GroupBy(e => e.City);
    foreach(var group in empByCity)
    {
        Console.WriteLine(group.Key + " : " + group.Count());
    }

    //n-part

    Console.WriteLine("\nTotal employees based on City and Title");
    var empCityTitle = empList.GroupBy(e => new { e.City, e.Title });
    foreach(var group in empCityTitle)
    {
        Console.WriteLine(group.Key.City + " - " + group.Key.Title + " : " + group.Count());
    }

    //0-part

    Console.WriteLine("\nYoungest employee");
    var youngest = empList.OrderByDescending(e => e.DOB).First();
    Console.WriteLine(youngest.FirstName+" "+youngest.LastName+" "+youngest.DOB.ToShortDateString());

    
   Console.ReadKey();
  }
 }
}