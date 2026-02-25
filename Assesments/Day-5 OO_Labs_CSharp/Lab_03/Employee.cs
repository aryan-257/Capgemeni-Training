using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    /// <summary>
    /// Class to represent employee information
    /// </summary>
    class Employee
    {
        /// <summary>
        /// Properties of the class
        /// </summary>
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public Address EmpAddress { get; set; } // HAS-A Modelling
    }
}
