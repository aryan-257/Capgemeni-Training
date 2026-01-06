using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Expected to get the Cache Capacity value thrice
            Console.WriteLine("MAX_CAPACITY is " + Cache.GetMaxCapacity());


            Console.WriteLine("MAX_CAPACITY is " + Cache.GetMaxCapacity());


            Console.WriteLine("MAX_CAPACITY is " + Cache.GetMaxCapacity());

            Console.ReadLine();
        }
    }
}
