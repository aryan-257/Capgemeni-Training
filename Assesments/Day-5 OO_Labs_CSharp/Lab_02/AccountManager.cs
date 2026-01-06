using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    class AccountManager
    {
        /// <summary>
        /// Method to store account details into the account object passed
        /// </summary>
        /// <param name="Acc"></param>
        public void FillAccountData(Account Acc)
        {
            //Write code to fill the Account object. Use CustomConsole to accept the value from the End User
            Acc.AccNo = CustomConsole.ReadString();
            Acc.Name =  CustomConsole.ReadString();
            Acc.Balance = CustomConsole.ReadInt();
        }

        /// <summary> 
        /// Method to display account details from the account object passed
        /// </summary>
        /// <param name="Acc"></param>
        public void DisplayAccountData(Account Acc)
        {
            //Write code to display the Account object
            Console.WriteLine("accnumber:"+ Acc.AccNo);
            Console.WriteLine("name:" + Acc.Name);
            Console.WriteLine("balance" + Acc.Balance);
        }
    }
}
