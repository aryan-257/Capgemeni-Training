using System;
using System.Collections.Generic;

namespace DigitalPettyCashLedger
{
    class Program
    {
        static void Main(string[] args)
        {
            Ledger<IncomeTransaction> incomeLedger=new Ledger<IncomeTransaction>();

            incomeLedger.AddEntry(new IncomeTransaction()
            {
                Id=1,
                Date=DateTime.Today,
                Amount=500,
                Description="Petty cash refill",
                Source="Main Cash"
            });

            Ledger<ExpenseTransaction> expenseLedger=new Ledger<ExpenseTransaction>();

            expenseLedger.AddEntry(new ExpenseTransaction()
            {
                Id=2,
                Date=DateTime.Today,
                Amount=20,
                Description="Office supplies",
                Category="Stationery"
            });

            expenseLedger.AddEntry(new ExpenseTransaction()
            {
                Id=3,
                Date=DateTime.Today,
                Amount=15,
                Description="Team snacks",
                Category="Food"
            });

            decimal totalIncome=incomeLedger.CalculateTotal();
            decimal totalExpense=expenseLedger.CalculateTotal();
            decimal netBalance=totalIncome-totalExpense;

            System.Console.WriteLine("Total Income: $" + totalIncome);
            System.Console.WriteLine("Total Expense: $" + totalExpense);
            System.Console.WriteLine("Net Balance: $" + netBalance);

            List<Transaction> allTransactions=new List<Transaction>();

            List<IncomeTransaction> incomeList=incomeLedger.GetAllTransactions();
            for(int i=0;i<incomeList.Count;i++)
            {
                allTransactions.Add(incomeList[i]);
            }

            List<ExpenseTransaction> expenseList=expenseLedger.GetAllTransactions();
            for(int i=0;i<expenseList.Count;i++)
            {
                allTransactions.Add(expenseList[i]);
            }

            System.Console.WriteLine("\n--- Transaction Summary ---");

            for(int i=0;i<allTransactions.Count;i++)
            {
                System.Console.WriteLine(allTransactions[i].GetSummary());
            }
        }
    }
}
