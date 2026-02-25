USE LPU_Db;
GO


CREATE TABLE Department (
    DeptId INT PRIMARY KEY,
    DeptName NVARCHAR(50) NOT NULL
);

CREATE TABLE Employees (
    EmpId INT PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Salary INT NOT NULL,
    DeptId INT,
    CONSTRAINT FK_Employees_Department
        FOREIGN KEY (DeptId)
        REFERENCES Department(DeptId)
);

INSERT INTO Department (DeptId, DeptName)
VALUES
(1, 'IT'),
(2, 'HR'),
(3, 'Finance'),
(4, 'Marketing');


INSERT INTO Employees (EmpId, Name, Salary, DeptId)
VALUES
(101, 'Aryan', 95000, 1),
(102, 'Rohit', 72000, 1),
(103, 'Neha', 82000, 2),
(104, 'Simran', 60000, 2),
(105, 'Amit', 110000, 3),
(106, 'Pooja', 65000, 3),
(107, 'Rahul', 55000, 4);


SELECT * FROM Department;
SELECT * FROM Employees;


SELECT 
    d.DeptName,
    e.Name AS EmployeeName,
    e.Salary AS HighestSalary
FROM Employees e
JOIN Department d
    ON e.DeptId = d.DeptId
WHERE e.Salary = (
        SELECT MAX(e2.Salary)
        FROM Employees e2
        WHERE e2.DeptId = e.DeptId
    )
AND e.DeptId IN (
        SELECT DeptId
        FROM Employees
        GROUP BY DeptId
        HAVING AVG(Salary) > 70000
    );
