using System;
using System.Collections.Generic;

namespace DigitalPettyCashLedger
{
    public class Ledger<T> where T : Transaction
    {
        private List<T> transactions=new List<T>();

        public void AddEntry(T entry)
        {
            transactions.Add(entry);
        }

        public List<T> GetTransactionsByDate(DateTime date)
        {
            List<T> result=new List<T>();

            for(int i=0;i<transactions.Count;i++)
            {
                if(transactions[i].Date.Date==date.Date)
                {
                    result.Add(transactions[i]);
                }
            }
            return result;
        }

        public decimal CalculateTotal()
        {
            decimal total=0;

            for(int i=0;i<transactions.Count;i++)
            {
                total=total+transactions[i].Amount;
            }
            return total;
        }

        public List<T> GetAllTransactions()
        {
            return transactions;
        }
    }
}
