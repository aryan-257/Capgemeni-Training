using System;

namespace DigitalPettyCashLedger
{
    public class ExpenseTransaction : Transaction
    {
        public string Category { get; set; }
        public override string GetSummary()
        {
            return "EXPENSE Amount: " + Amount + ", Category: " + Category + ", Date: " + Date.ToShortDateString();
        }
    }
}