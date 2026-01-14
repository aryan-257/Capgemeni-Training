using System.Collections.Generic;
using System.Linq;
using WorkforceManagementSystem.Models;

namespace WorkforceManagementSystem.Services
{
    public class EmployeeService
    {
        private List<Employee> _employees;
        private List<Project> _projects;

        public EmployeeService(List<Employee> employees , List<Project> projects)
        {
            _employees = employees;
            _projects = projects;
        }

        // 1. Salary > 60000
        public List<Employee> GetSalaryReviewCandidates()
        {
            return _employees
                .Where(e => e.Salary > 60000)
                .ToList();
        }

        // 2. Employee Names
        public List<string> GetEmployeeNames()
        {
            return _employees
                .Select(e => e.Name)
                .ToList();
        }

        // 3. HR department exists
        public bool HasHRDepartment()
        {
            return _employees.Any(e => e.Department == "HR");
        }

        // 4. Department headcount
        public List<object> GetDepartmentHeadcount()
        {
            return _employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    Count = g.Count()
                })
                .Cast<object>()
                .ToList();
        }

        // 5. Highest paid employee
        public Employee GetHighestPaidEmployee()
        {
            return _employees
                .OrderByDescending(e => e.Salary)
                .FirstOrDefault();
        }

        // 6. Sorted by salary then name
        public List<Employee> GetSortedEmployees()
        {
            return _employees
                .OrderByDescending(e => e.Salary)
                .ThenBy(e => e.Name)
                .ToList();
        }

        // 7. Project allocation
        public List<object> GetProjectAllocation()
        {
            return _employees
                .Join(_projects,
                      e => e.Id,
                      p => p.EmployeeId,
                      (e,p) => new
                      {
                          EmployeeName = e.Name,
                          ProjectName = p.ProjectName
                      })
                .Cast<object>()
                .ToList();
        }

        // 8. Unassigned employees
        public List<Employee> GetUnassignedEmployees()
        {
            return _employees
                .Where(e => !_projects.Any(p => p.EmployeeId == e.Id))
                .ToList();
        }

        // 9. All skills
        public List<string> GetAllSkills()
        {
            return _employees
                .SelectMany(e => e.Skills)
                .Distinct()
                .ToList();
        }

        // 10. Top 3 by department
        public List<object> GetTopEarnersByDepartment()
        {
            return _employees
                .GroupBy(e => e.Department)
                .Select(g => new
                {
                    Department = g.Key,
                    TopEmployees = g
                        .OrderByDescending(e => e.Salary)
                        .Take(3)
                        .ToList()
                })
                .Cast<object>()
                .ToList();
        }

        // 11. Remove duplicates
        public List<Employee> GetUniqueEmployees()
        {
            return _employees
                .GroupBy(e => e.Id)
                .Select(g => g.First())
                .ToList();
        }

        // 12. Pagination
        public List<Employee> GetPaginatedEmployees(int pageNumber , int pageSize)
        {
            return _employees
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}
