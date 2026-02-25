using ConArcDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConArcDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentBL bl = new StudentBL();

            while (true)
            {
                Console.WriteLine("\n===== STUDENT MANAGEMENT SYSTEM =====");

                Console.WriteLine("1. Insert Student");

                Console.WriteLine("2. Show All Students");

                Console.WriteLine("3. Search Student By ID");

                Console.WriteLine("4. Search Student By Name");

                Console.WriteLine("5. Update Student Address");

                Console.WriteLine("6. Delete Student");

                Console.WriteLine("7. Exit");

                Console.Write("\nEnter Choice: ");

                int choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {

                    case 1:
                    { 
                        Student s = new Student();

                        Console.Write("Enter ID: ");
                        s.RollNo = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter Name: ");
                        s.Name = Console.ReadLine();

                        Console.Write("Enter Address: ");
                        s.Address = Console.ReadLine();

                        Console.Write("Enter Age: ");
                        s.Age = Convert.ToInt32(Console.ReadLine());

                        if (bl.Insert(s))
                            Console.WriteLine("Student Inserted Successfully");
                        else
                            Console.WriteLine("Insert Failed");

                        break;
                    }

                    case 2:
                    { 
                        List<Student> list = bl.SelectAll();

                        if (list.Count > 0)
                        {
                            foreach (Student stud in list)
                            {
                                Console.WriteLine(
                                $"{stud.RollNo} | {stud.Name} | {stud.Address} | {stud.Age}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Students Found");
                        }

                        break;
                    }

                    case 3:
                    { 
                        Console.Write("Enter ID: ");

                        int id = Convert.ToInt32(Console.ReadLine());

                        Student student = bl.SelectByID(id);

                        if (student != null)
                        {
                            Console.WriteLine(
                            $"{student.RollNo} | {student.Name} | {student.Address} | {student.Age}");
                        }
                        else
                        {
                            Console.WriteLine("Student Not Found");
                        }

                        break;
                    }

                    case 4:
                    { 

                        Console.Write("Enter Name: ");

                        string name = Console.ReadLine();

                        List<Student> students = bl.SelectByName(name);

                        if (students.Count > 0)
                        {
                            foreach (Student stud in students)
                            {
                                Console.WriteLine(
                                $"{stud.RollNo} | {stud.Name} | {stud.Address} | {stud.Age}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Student Found");
                        }

                        break;
                    }

                    case 5:
                    { 

                        Console.Write("Enter ID: ");

                        int uid = Convert.ToInt32(Console.ReadLine());

                        Console.Write("Enter New Address: ");

                        string address = Console.ReadLine();

                        if (bl.UpdateAddress(uid, address))
                        {
                            Console.WriteLine("Updated Successfully");
                        }
                        else
                        {
                            Console.WriteLine("Update Failed");
                        }
                        break;
                    }

                    case 6:
                    { 

                        Console.Write("Enter ID: ");

                        int did = Convert.ToInt32(Console.ReadLine());

                        if (bl.Delete(did))
                        {
                            Console.WriteLine("Deleted Successfully");
                        }
                        else
                        {
                            Console.WriteLine("Delete Failed");
                        }
                        break;

                    }
                    case 7:
                    {
                        Console.WriteLine("Exiting Application!");
                        return;
                    }


                    default:
                    {
                        Console.WriteLine("Invalid Choice");
                        break;
                    }
                }
            }

        }
    }
}