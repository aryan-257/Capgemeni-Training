using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        { 
            // Create an account object
            Account Acc = new Account(); // Analyse the Account class

		    Console.WriteLine("Performing Account Transactions using Account Manager 1...");
		    AccountManager1 Manager1 = new AccountManager1(); // Analyse the AccountManager1 class
            Manager1.FillAccountData(Acc);
            Manager1.DisplayAccountData(Acc);
            

            Console.WriteLine();

            Console.WriteLine("Performing Account Transactions using Account Manager 2...");
            AccountManager2 Manager2 = new AccountManager2(); // Analyse the AccountManager2 class
            Manager2.FillAccountData(Acc);
            Manager2.DisplayAccountData(Acc);
            
            Console.ReadLine();
        }
    }
}
