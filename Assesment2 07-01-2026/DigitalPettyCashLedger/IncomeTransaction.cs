using System;

namespace DigitalPettyCashLedger
{
    public class IncomeTransaction : Transaction
    {
        public string Source { get; set; }
        public override string GetSummary()
        {
            return "INCOME Amount: " + Amount + ", Source: " + Source + ", Date: " + Date.ToShortDateString();
        }
    }
}
