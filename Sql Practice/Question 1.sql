CREATE DATABASE CrudDB;
GO

USE CrudDB;
GO


CREATE TABLE Student (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    Cgpa DECIMAL(3,2) CHECK (Cgpa BETWEEN 0.00 AND 4.00),
    CreatedAt DATETIME DEFAULT GETDATE()
);

INSERT INTO Student (FirstName, LastName, Email, Cgpa)
VALUES
('Aryan', 'Dalal', 'aryan@gmail.com', 3.75),
('Rahul', 'Sharma', 'rahul@gmail.com', 3.40),
('Neha', 'Verma', 'neha@gmail.com', 3.90);

SELECT * FROM Student;

SELECT * FROM Student WHERE Id = 1;

UPDATE Student
SET Cgpa = 3.85
WHERE Id = 1;

DELETE FROM Student
WHERE Id = 2;




