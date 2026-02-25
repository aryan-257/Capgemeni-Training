using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinSerializationDemo
{
    [Serializable]
    public class Employee
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [NonSerialized]
        int salary;
        public int Salary
        {
            get
            {
                return salary;
            }
            set
            {
                salary = value;
            }
        }
    }
}
