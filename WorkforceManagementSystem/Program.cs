using System;
using WorkforceManagementSystem.Data;
using WorkforceManagementSystem.Services;

class Program
{
    static void Main()
    {
        EmployeeService service = new EmployeeService(
            DataStore.Employees ,
            DataStore.Projects
        );

        var highSalary = service.GetSalaryReviewCandidates();
        var skills = service.GetAllSkills();
        var highestPaid = service.GetHighestPaidEmployee();

        Console.WriteLine("Application executed successfully");
        Console.WriteLine("Highest Paid Employee : " + highestPaid?.Name);
    }
}
