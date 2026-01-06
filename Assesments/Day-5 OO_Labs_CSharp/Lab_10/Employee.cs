using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabT10
{
    class Employee
    {
        /// <summary>
        /// Properties of the class
        /// </summary>
        public string EmpId { get; set; }
        public string Name { get; set; }
        public double Basic { get; set; }
        public double HRA { get; set; }
        public double AllowancePercentage { get; set; }
        public int Role { get; set; }

        /// <summary>
        /// Method to get the salary of an employee
        /// </summary>
        /// <returns></returns>
        public double GetSalary()
        {
            //Delegation
            
        }

        /// <summary>
        /// Method to get the allowance of an employee
        /// </summary>
        /// <returns></returns>
        public double GetAllowance()
        {
            //Delegation
            
        }
    }
}
