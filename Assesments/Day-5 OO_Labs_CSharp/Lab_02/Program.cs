using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab2
{
    class Program
    {
        static void Main(string[] args)
        {
            Account Acc = new Account();
		
            //Run the code, analyze the code and implement the solution accordingly
		    Console.WriteLine("Performing Account Transactions using AccManager...");
            AccountManager Manager = new AccountManager();
            Manager.FillAccountData(Acc);
            Manager.DisplayAccountData(Acc);

            Console.ReadLine();
        }
    }
}
