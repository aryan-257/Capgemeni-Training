using System;
using System.Linq;

namespace BankTransactionApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter initial balance: ");
            long initialBalance = long.Parse(Console.ReadLine());

            Console.Write("Enter transactions (space-separated): ");
            string[] input = Console.ReadLine().Split(' ');

            long[] transactions = input.Select(x => long.Parse(x)).ToArray();

            BankAccount account = new BankAccount(initialBalance);
            account.ProcessTransactions(transactions);

            Console.WriteLine("Final balance: " + account.GetBalance());
        }
    }
}
