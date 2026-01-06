using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    /// <summary>
    /// Class to read values from the console based on the data type
    /// </summary>
    class CustomConsole
    {
        /// <summary>
        /// Method to read string values from the console
        /// </summary>
        /// <returns></returns>
        public static string ReadString()
        {
            return Console.ReadLine();
        }

        /// <summary>
        /// Method to read float values from the console
        /// </summary>
        /// <returns></returns>
        public static float ReadFloat()
        {
            return float.Parse(Console.ReadLine());
        }

        /// <summary>
        /// Method to read double values from the console
        /// </summary>
        /// <returns></returns>
        public static double ReadDouble()
        {
            return Double.Parse(Console.ReadLine());
        }

        /// <summary>
        /// Method to read integer values from the console
        /// </summary>
        /// <returns></returns>
        public static int ReadInt()
        {
            return int.Parse(Console.ReadLine());
        }

        /// <summary>
        /// Method to read char values from the console
        /// </summary>
        /// <returns></returns>
        public static char ReadChar()
        {
            return char.Parse(Console.ReadLine());
        }
    }
}
