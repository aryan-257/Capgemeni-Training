using System.Collections.Generic;
using WorkforceManagementSystem.Models;

namespace WorkforceManagementSystem.Data
{
    public static class DataStore
    {
        public static List<Employee> Employees = new List<Employee>()
        {
            new Employee
            {
                Id = 1,
                Name = "Aryan",
                Department = "IT",
                Salary = 80000,
                Skills = new List<string>() { "C#" , "LINQ" }
            },

            new Employee
            {
                Id = 2,
                Name = "Rohit",
                Department = "HR",
                Salary = 55000,
                Skills = new List<string>() { "Recruitment" }
            },

            new Employee
            {
                Id = 3,
                Name = "Neha",
                Department = "IT",
                Salary = 90000,
                Skills = new List<string>() { "Azure","SQL" }
            },

            new Employee
            {
                Id = 3,
                Name = "Neha Duplicate",
                Department = "IT",
                Salary = 90000,
                Skills = new List<string>() { "Azure" }
            }
        };

        public static List<Project> Projects = new List<Project>()
        {
            new Project
            {
                ProjectId = 101,
                EmployeeId = 1,
                ProjectName = "ERP System"
            },

            new Project
            {
                ProjectId = 102,
                EmployeeId = 3,
                ProjectName = "Cloud Migration"
            }
        };
    }
}
