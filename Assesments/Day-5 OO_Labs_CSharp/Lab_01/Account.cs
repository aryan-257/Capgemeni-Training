using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Account
    {
        /// <summary>
        /// Fields for the class
        /// </summary>
        // internal is default access specifiers for a class in C# 
        internal string _AccNo;
        internal string _Name;
        internal double _Balance;

        /// <summary>
        /// Properties for the fields
        /// </summary>
        public string AccNo
        {
            get
            {
                return _AccNo;
            }
            set
            {
                string BankCode = "ICI";
                _AccNo = BankCode + value;
            }
        }
        public string Name { get; set; }
        public double Balance
        {
            get
            {
                return _Balance + ComputeInterest();
            }
            set
            {
                _Balance = value;
            }
        }

        /// <summary>
        /// Method to calculate the interest for an account
        /// </summary>
        /// <returns>Interest</returns>
        private double ComputeInterest()
        {
            double Interest = _Balance * 8.5 / 100.0;
            return Interest;
        }      
    }
}
