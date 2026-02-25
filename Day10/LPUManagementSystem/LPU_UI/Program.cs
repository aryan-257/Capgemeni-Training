using LPU_BL;
using LPU_Entity;
using LPU_Exceptions;

namespace LPU_UI
{
    public class Program
    {

        static void Menu()
        {
            Console.WriteLine("         Student Management System           ");
            Console.WriteLine("=============================================");
            Console.WriteLine("1. Search Student by ID");
            Console.WriteLine("2. Show All Students");
            Console.WriteLine("3. Add Student Details");
            Console.WriteLine("4. Update Student Details");
            Console.WriteLine("5. Drop Student Details");
            Console.WriteLine("6. Exit");

        }
        static void Main(string[] args)
        {
            StudentBL sblObj = new StudentBL();
            do
            {
                Menu();
                int choice = 0;
                Console.Write("Please Enter Your Choice: ");
                choice = Int32.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1: //Search studnet by id
                        {
                            int id = 0;

                            try
                            {

                                Console.Write("\tEnter Student ID to search: ");
                                id = Int32.Parse(Console.ReadLine());

                                Student sObj = sblObj.SearchStudentByID(id);

                                if(sObj != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("ID | Name | Course | Address");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("=============================================");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    Console.WriteLine($"{sObj.Id} | {sObj.Name} | {sObj.Course} | {sObj.Address}");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            catch(LpuException e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(e.Message);
                            }
                            catch(Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(e.Message);
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case 2: //Show All Students
                        {
                            try
                            {
                                Console.Write("\tEnter Student Name for Search: ");
                                string name = Console.ReadLine();

                                List<Student> studList = sblObj.SearchStudentByName(name);

                                if (studList != null)
                                {
                                    Console.ForegroundColor = ConsoleColor.Cyan;
                                    Console.WriteLine("ID | Name | Course | Address");
                                    Console.ForegroundColor = ConsoleColor.White;
                                    Console.WriteLine("=============================================");
                                    Console.ForegroundColor = ConsoleColor.Yellow;
                                    foreach (var item in studList)
                                    {
                                        Console.WriteLine($"{item.Id} | {item.Name} | {item.Course} | {item.Address}");

                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            catch (LpuException e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\tUnder Construction! Sorry for the inconvenience caused.");
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("\tUnder Construction! Sorry for the inconvenience caused.");
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case 3: //Add Students
                        {
                            try
                            {
                                Student newSObj = new Student();

                                Console.Write("Enter Student ID: ");
                                newSObj.Id = Int32.Parse(Console.ReadLine());

                                Console.Write("Enter Student Name: ");
                                newSObj.Name = Console.ReadLine();

                                //Console.Write("Enter Enrolled Course: ");
                                newSObj.Course = CourseType.CSE;

                                Console.Write("Enter Student Address: ");
                                newSObj.Address = Console.ReadLine();

                                if (newSObj != null)
                                {
                                    bool check = sblObj.EnrollStudent(newSObj);

                                    if (check)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("Student Added Successfully");
                                    }
                                }
                            }
                            catch (LpuException e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(e.Message);
                            }
                            catch (Exception e)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine(e.Message);
                            }
                            Console.ForegroundColor = ConsoleColor.White;
                            break;
                        }
                    case 4:
                        {
                            break;
                        }
                    default:
                        break;
                }

            } while (true);

        }
    }
}
