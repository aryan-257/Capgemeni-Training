using LINQ;

// Sample Data Setup
List<Employee> employees = new List<Employee>
{
    new Employee { Id = 1, Name = "Amit Sharma", Department = "IT", Salary = 75000, Skills = new List<string> { "C#", "SQL", "Azure" } },
    new Employee { Id = 2, Name = "Priya Singh", Department = "HR", Salary = 55000, Skills = new List<string> { "Recruitment", "Training" } },
    new Employee { Id = 3, Name = "Rahul Verma", Department = "IT", Salary = 82000, Skills = new List<string> { "Java", "Spring", "Microservices" } },
    new Employee { Id = 4, Name = "Sneha Patel", Department = "Finance", Salary = 68000, Skills = new List<string> { "Accounting", "Excel", "SAP" } },
    new Employee { Id = 5, Name = "Vikram Reddy", Department = "IT", Salary = 95000, Skills = new List<string> { "Python", "Machine Learning", "AWS" } },
    new Employee { Id = 6, Name = "Anjali Gupta", Department = "Marketing", Salary = 62000, Skills = new List<string> { "SEO", "Content Writing", "Analytics" } },
    new Employee { Id = 7, Name = "Karan Mehta", Department = "IT", Salary = 71000, Skills = new List<string> { "React", "Node.js", "MongoDB" } },
    new Employee { Id = 8, Name = "Neha Joshi", Department = "HR", Salary = 58000, Skills = new List<string> { "Employee Relations", "Payroll" } },
    new Employee { Id = 9, Name = "Rohan Kumar", Department = "Finance", Salary = 72000, Skills = new List<string> { "Financial Analysis", "Budgeting" } },
    new Employee { Id = 10, Name = "Pooja Desai", Department = "Marketing", Salary = 65000, Skills = new List<string> { "Digital Marketing", "Social Media" } },
    // Duplicate for testing
    new Employee { Id = 5, Name = "Vikram Reddy", Department = "IT", Salary = 95000, Skills = new List<string> { "Python", "Machine Learning", "AWS" } }
};

List<Project> projects = new List<Project>
{
    new Project { ProjectId = 101, EmployeeId = 1, ProjectName = "Cloud Migration" },
    new Project { ProjectId = 102, EmployeeId = 3, ProjectName = "ERP Implementation" },
    new Project { ProjectId = 103, EmployeeId = 5, ProjectName = "AI Chatbot" },
    new Project { ProjectId = 104, EmployeeId = 7, ProjectName = "Web Portal" },
    new Project { ProjectId = 105, EmployeeId = 1, ProjectName = "Mobile App" },
    new Project { ProjectId = 106, EmployeeId = 9, ProjectName = "Financial Dashboard" }
};

Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine("SCENARIO-BASED LINQ CODING ASSESSMENT");
Console.WriteLine("Enterprise Workforce Management System");
Console.WriteLine("=".PadRight(80, '='));
Console.WriteLine();

// 🟢 Section 1 – HR Analytics
Console.WriteLine("🟢 SECTION 1 – HR ANALYTICS");
Console.WriteLine("-".PadRight(80, '-'));

// 1.1 Salary Review Candidates
Console.WriteLine("\n1.1 Salary Review Candidates (Salary > ₹60,000):");
var salaryReviewCandidates = employees
    .Where(e => e.Salary > 60000)
    .ToList();

foreach (var emp in salaryReviewCandidates)
{
    Console.WriteLine($"  {emp.Name} - {emp.Department} - ₹{emp.Salary:N0}");
}

// 1.2 Employee Name Directory
Console.WriteLine("\n1.2 Employee Name Directory:");
var employeeNames = employees
    .Select(e => e.Name)
    .ToList();

foreach (var name in employeeNames)
{
    Console.WriteLine($"  {name}");
}

// 1.3 HR Department Presence
Console.WriteLine("\n1.3 HR Department Presence:");
var hasHRDepartment = employees
    .Any(e => e.Department == "HR");

Console.WriteLine($"  HR Department exists: {hasHRDepartment}");

// 🟡 Section 2 – Management Insights
Console.WriteLine("\n\n🟡 SECTION 2 – MANAGEMENT INSIGHTS");
Console.WriteLine("-".PadRight(80, '-'));

// 2.1 Department-Wise Headcount
Console.WriteLine("\n2.1 Department-Wise Headcount:");
var departmentHeadcount = employees
    .GroupBy(e => e.Department)
    .Select(g => new { Department = g.Key, Count = g.Count() })
    .ToList();

foreach (var dept in departmentHeadcount)
{
    Console.WriteLine($"  {dept.Department}: {dept.Count} employees");
}

// 2.2 Highest-Paid Employee
Console.WriteLine("\n2.2 Highest-Paid Employee:");
var highestPaidEmployee = employees
    .OrderByDescending(e => e.Salary)
    .FirstOrDefault();

if (highestPaidEmployee != null)
{
    Console.WriteLine($"  {highestPaidEmployee.Name} - {highestPaidEmployee.Department} - ₹{highestPaidEmployee.Salary:N0}");
}

// 2.3 Salary-Based Sorting
Console.WriteLine("\n2.3 Salary-Based Sorting (Desc by Salary, Asc by Name):");
var sortedEmployees = employees
    .OrderByDescending(e => e.Salary)
    .ThenBy(e => e.Name)
    .ToList();

foreach (var emp in sortedEmployees)
{
    Console.WriteLine($"  {emp.Name} - ₹{emp.Salary:N0}");
}

// 🔵 Section 3 – Project & Skill Intelligence
Console.WriteLine("\n\n🔵 SECTION 3 – PROJECT & SKILL INTELLIGENCE");
Console.WriteLine("-".PadRight(80, '-'));

// 3.1 Project Allocation Report
Console.WriteLine("\n3.1 Project Allocation Report:");
var projectAllocation = employees
    .Join(projects,
        emp => emp.Id,
        proj => proj.EmployeeId,
        (emp, proj) => new { EmployeeName = emp.Name, ProjectName = proj.ProjectName })
    .ToList();

foreach (var allocation in projectAllocation)
{
    Console.WriteLine($"  {allocation.EmployeeName} → {allocation.ProjectName}");
}

// 3.2 Unassigned Employees
Console.WriteLine("\n3.2 Unassigned Employees:");
var unassignedEmployees = employees
    .Where(emp => !projects.Any(proj => proj.EmployeeId == emp.Id))
    .ToList();

foreach (var emp in unassignedEmployees)
{
    Console.WriteLine($"  {emp.Name} - {emp.Department}");
}

// 3.3 Organization-Wide Skill Inventory
Console.WriteLine("\n3.3 Organization-Wide Skill Inventory:");
var allSkills = employees
    .SelectMany(e => e.Skills)
    .Distinct()
    .ToList();

foreach (var skill in allSkills)
{
    Console.WriteLine($"  {skill}");
}

// 🔴 Section 4 – Advanced Workforce Analytics
Console.WriteLine("\n\n🔴 SECTION 4 – ADVANCED WORKFORCE ANALYTICS");
Console.WriteLine("-".PadRight(80, '-'));

// 4.1 Top Earners by Department
Console.WriteLine("\n4.1 Top Earners by Department (Top 3):");
var topEarnersByDepartment = employees
    .GroupBy(e => e.Department)
    .Select(g => new
    {
        Department = g.Key,
        TopEmployees = g.OrderByDescending(e => e.Salary).Take(3).ToList()
    })
    .ToList();

foreach (var dept in topEarnersByDepartment)
{
    Console.WriteLine($"\n  {dept.Department}:");
    foreach (var emp in dept.TopEmployees)
    {
        Console.WriteLine($"    {emp.Name} - ₹{emp.Salary:N0}");
    }
}

// 4.2 Duplicate Data Cleanup
Console.WriteLine("\n4.2 Duplicate Data Cleanup (Remove duplicates by Id):");
var uniqueEmployees = employees
    .GroupBy(e => e.Id)
    .Select(g => g.First())
    .ToList();

Console.WriteLine($"  Original count: {employees.Count}");
Console.WriteLine($"  After cleanup: {uniqueEmployees.Count}");
Console.WriteLine($"  Duplicates removed: {employees.Count - uniqueEmployees.Count}");

// 4.3 Employee List Pagination
Console.WriteLine("\n4.3 Employee List Pagination:");
int pageNumber = 2;
int pageSize = 5;

var paginatedEmployees = uniqueEmployees
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();

Console.WriteLine($"  Page {pageNumber} (Page Size: {pageSize}):");
foreach (var emp in paginatedEmployees)
{
    Console.WriteLine($"    {emp.Name} - {emp.Department} - ₹{emp.Salary:N0}");
}

Console.WriteLine("\n" + "=".PadRight(80, '='));
Console.WriteLine("ASSESSMENT COMPLETED");
Console.WriteLine("=".PadRight(80, '='));
