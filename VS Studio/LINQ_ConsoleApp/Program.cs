// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;
using System.Linq;

namespace LINQ_ConsoleApp
{
    class Program
    {
        static void LinqToObjectDemo()
        {
            int[] numArray = { 10, 2, 12, 34, 45, 65, 23, 66, 48, 8, 27 };
            string[] nameArray = { " Alok", "Rajat", "Sumeet", "Priya", "Ayush", "Himanshu", "Aryan", "Kajal", "Prince", "Kunal", "Kapil" };


            //LINQ Query
            var result = from data in numArray
                         where data % 2 == 0 && data > 20
                         select data;
               

            foreach (var item in numArray)
            {
                if(item%2 == 0)
                {
                    Console.WriteLine(item);
                }
            }
        }

        public void Main(string[] args)
        {
            LeinqToObjectDemo();
        }
    }
}
