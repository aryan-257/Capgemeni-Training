using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace LINQ_ConsoleApp
{
    public class Program
    {

        public static void LinqToObjectDemo()
        {
            int[] numArray = { 10, 2, 12, 34, 45, 65, 23, 66, 48, 8, 27 };

            string[] nameArray = { "Priya", "Ayush", "Harshita", "Manish", "Himanshu", "Mahi", "Krishma", "Aman", "Devansh", "Karan", "Pratham" };

            //foreach(var item in numArray)
            //{
            //    if(item % 2 == 0)
            //    {
            //        Console.WriteLine(item);
            //    }
            //}

            //LINQ Query
            int dataToSearch = 15;
            var result1 = from data in numArray
                          where data == dataToSearch
                          select data;

            var result = from data in numArray
                         where data % 2 == 0 && data > 20
                         select data;

            var result2 = from data in nameArray
                          where data.StartsWith("A") || data.Contains("a")
                          orderby data descending
                          select data;

            var result3 = nameArray.Where(n => n.Contains("t") || n.StartsWith("A"));

            foreach( var item in result2)
            {
                Console.WriteLine(item);
            }

        }

        public static void LinqToObjectDemoOnCustomType()
        {
            List<Customer> custList = new List<Customer>()
            {
                new Customer() {CustomerId = 101,Name = "Manish",City = "Pilibhit" },
                new Customer() {CustomerId = 102,Name = "Aman",City = "Gurgaon" },
                new Customer() {CustomerId = 103,Name = "Mahi",City = "Moradabad" },
                new Customer() {CustomerId = 104,Name = "Himanshu",City = "Pune" },
                new Customer() {CustomerId = 105,Name = "Krishma",City = "Amritsar" },
                new Customer() {CustomerId = 106,Name = "Pratham",City = "Pune" },
                new Customer() {CustomerId = 107,Name = "Devansh",City = "Pune" },
                new Customer() {CustomerId = 108,Name = "Karan",City = "Gurgaon" }
            };

            var data = new { OrderID = 1100, OrderDate = "12/01/2025", TotalAmount = 14000 };

            var result = custList.Where(cust => cust.City == "Pune");

            var result1 = custList.FindAll(cust => cust.City == "Gurgaon");

            foreach (var item in result1)
            {
                Console.WriteLine($"{item.CustomerId}\t{item.Name}\t{item.City}");
            }

        }

        public static void LambdaLookupOnStudentList()
        {
            StudentRepo sRepo = new StudentRepo();
            List<Student> tempList = sRepo.GetAllStudents();

            var query = tempList.ToLookup(s => s.Gender == "Male");

            foreach(IGrouping<Boolean,Student> group in query)
            {
                //Console.WriteLine("Key: {0}", group.Key);

                int totalFees = 0;

                if(group.Key == true)
                {
                    Console.WriteLine("Male Student details below: ");
                }
                else
                {
                    Console.WriteLine("Female Student details below: ");
                }
                foreach (Student std in group)
                {
                    Console.WriteLine($"\t{std.Name}");
                    totalFees += std.Fees;
                }

                Console.WriteLine($"Total fees paid: {totalFees}\n");

            }
        }

        static void Main(string[] args)
        {
            //LinqToObjectDemo();
            //LinqToObjectDemoOnCustomType();
            //LambdaLookupOnStudentList();

            StudentRepo sRepo = new StudentRepo();
            List<Student> tempList = sRepo.GetAllStudents();

            int total = tempList.Select(s => s.Fees).Sum();
            Console.WriteLine(total);

        }
    }
}
