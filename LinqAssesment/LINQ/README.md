# LINQ Coding Assessment - Enterprise Workforce Management System

## 📋 Overview
This project implements a comprehensive LINQ-based solution for an Enterprise Workforce Management System. It demonstrates various LINQ operations for HR analytics, management insights, project intelligence, and advanced workforce analytics.

## 🎯 Assessment Details
- **Role**: C# / .NET Backend Developer
- **Time Limit**: 45 Minutes
- **Difficulty**: Intermediate → Advanced
- **Technology**: C#, LINQ (Method Syntax)

## 🏗️ Domain Model

### Employee Class
```csharp
public class Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Salary { get; set; }
    public List<string> Skills { get; set; }
}
```

### Project Class
```csharp
public class Project
{
    public int ProjectId { get; set; }
    public int EmployeeId { get; set; }
    public string ProjectName { get; set; }
}
```

## 📊 Implemented Solutions

### 🟢 Section 1 – HR Analytics

#### 1.1 Salary Review Candidates
Retrieves employees earning more than ₹60,000 for annual salary review.
```csharp
var salaryReviewCandidates = employees
    .Where(e => e.Salary > 60000)
    .ToList();
```

#### 1.2 Employee Name Directory
Generates a list of employee names for internal communication.
```csharp
var employeeNames = employees
    .Select(e => e.Name)
    .ToList();
```

#### 1.3 HR Department Presence
Checks if the organization has any employee in the HR department.
```csharp
var hasHRDepartment = employees
    .Any(e => e.Department == "HR");
```

### 🟡 Section 2 – Management Insights

#### 2.1 Department-Wise Headcount
Creates a report showing the number of employees in each department.
```csharp
var departmentHeadcount = employees
    .GroupBy(e => e.Department)
    .Select(g => new { Department = g.Key, Count = g.Count() })
    .ToList();
```

#### 2.2 Highest-Paid Employee
Identifies the highest-paid employee in the organization.
```csharp
var highestPaidEmployee = employees
    .OrderByDescending(e => e.Salary)
    .FirstOrDefault();
```

#### 2.3 Salary-Based Sorting
Produces a list of employees sorted by salary (descending) and name (ascending).
```csharp
var sortedEmployees = employees
    .OrderByDescending(e => e.Salary)
    .ThenBy(e => e.Name)
    .ToList();
```

### 🔵 Section 3 – Project & Skill Intelligence

#### 3.1 Project Allocation Report
Generates a report showing which employees are working on which projects.
```csharp
var projectAllocation = employees
    .Join(projects,
        emp => emp.Id,
        proj => proj.EmployeeId,
        (emp, proj) => new { EmployeeName = emp.Name, ProjectName = proj.ProjectName })
    .ToList();
```

#### 3.2 Unassigned Employees
Identifies employees who are not assigned to any project.
```csharp
var unassignedEmployees = employees
    .Where(emp => !projects.Any(proj => proj.EmployeeId == emp.Id))
    .ToList();
```

#### 3.3 Organization-Wide Skill Inventory
Creates a distinct list of all skills available across the organization.
```csharp
var allSkills = employees
    .SelectMany(e => e.Skills)
    .Distinct()
    .ToList();
```

### 🔴 Section 4 – Advanced Workforce Analytics

#### 4.1 Top Earners by Department
Retrieves the top 3 highest-paid employees for each department.
```csharp
var topEarnersByDepartment = employees
    .GroupBy(e => e.Department)
    .Select(g => new
    {
        Department = g.Key,
        TopEmployees = g.OrderByDescending(e => e.Salary).Take(3).ToList()
    })
    .ToList();
```

#### 4.2 Duplicate Data Cleanup
Removes duplicate employee records based on Employee.Id.
```csharp
var uniqueEmployees = employees
    .GroupBy(e => e.Id)
    .Select(g => g.First())
    .ToList();
```

#### 4.3 Employee List Pagination
Implements pagination for employee records.
```csharp
int pageNumber = 2;
int pageSize = 5;

var paginatedEmployees = uniqueEmployees
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();
```

## 🚀 How to Run

1. Navigate to the LINQ project directory:
   ```bash
   cd LINQ
   ```

2. Run the project:
   ```bash
   dotnet run
   ```

## 📦 Sample Data
The project includes pre-populated sample data with:
- 10 unique employees across IT, HR, Finance, and Marketing departments
- 6 projects assigned to various employees
- Diverse skill sets for each employee
- One duplicate employee record for testing cleanup functionality

## ✅ Key Features
- Pure LINQ implementation (no loops)
- Method syntax throughout
- Production-ready, readable code
- No mutation of original collections
- Comprehensive output formatting
- Real-world business scenarios

## 📝 Notes
- All queries use LINQ method syntax as preferred
- Code follows C# best practices and conventions
- Output is formatted for easy readability
- Includes test data for all scenarios
