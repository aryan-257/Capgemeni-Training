using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab4
{
    class Cache
    {
        private static int MAX_CAPACITY = 0;

        // Create a static constructor to initialize MAX_CAPACITY. Use CustomConsole class in Constructor for fetching the data
        static Cache()
        {
            MAX_CAPACITY = CustomConsole.ReadInt();
        }
        /// <summary>
        /// Static method to get the maximum capacity of the cache
        /// </summary>
        /// <returns></returns>
        public static int GetMaxCapacity() // Don't make any changes
        {
            Console.WriteLine("Returning MAX_CAPACITY");
            return MAX_CAPACITY;
        }
    }
}
