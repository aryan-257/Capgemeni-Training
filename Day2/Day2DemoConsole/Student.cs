using System;
using System.ComponentModel.DataAnnotations;

namespace Day2DemoConsole;

public class Student
{
#region Fields
int rollNo;
string name;
string addr;
#endregion

///<summary>
/// Default Constructor
///</summary>
public Student()
{
    rollNo = 100;
    name = "Dummy";
    addr = "Dummy City";
}
public Student(int id, string name, string city )
{
    rollNo = id;
    this.name = name;
    addr = city;
}
public void DisplayDetails(Student s1)
{
    Console.WriteLine("Roll No: " + s1.rollNo);
    Console.WriteLine("Name: " + s1.name);
    Console.WriteLine("Address: " + s1.addr);
}
}
