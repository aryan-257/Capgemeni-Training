using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class AccountManager2
    {
        /// <summary> 
        /// Method to fill account details into the account object passed
        /// </summary>
        /// <param name="Acc"></param>
        public void FillAccountData(Account Acc)
        {
            Acc.AccNo = "004701";
            Acc.Name = "Chandra";
            Acc.Balance = 45000.0;
        }

        /// <summary>
        /// Method to display account details from the account object passed
        /// </summary>
        /// <param name="Acc"></param>
        public void DisplayAccountData(Account Acc)
        {
            Console.WriteLine("AccNo : " + Acc.AccNo);
            Console.WriteLine("Name : " + Acc.Name);
            Console.WriteLine("Balance : " + Acc.Balance);
        }
    }
}
