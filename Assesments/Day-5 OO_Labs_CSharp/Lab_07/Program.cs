using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            int Marks1, Marks2, Marks3;

            //Accept the values from command line arguments
            

            //Store the values entered in the object
            ResultFinder Finder = new ResultFinder();
            

            //Display all the information with the help of get and other methods
            Console.WriteLine("Marks entered------------- ");
            Console.WriteLine("Marks 1 : " );
            Console.WriteLine("Marks 2 : " );
            Console.WriteLine("Marks 3 : " );
            Console.WriteLine("Total : " );
            Console.WriteLine("Average : " );
            Console.WriteLine("Result : " );

            Console.ReadLine();
        }
    }
}
