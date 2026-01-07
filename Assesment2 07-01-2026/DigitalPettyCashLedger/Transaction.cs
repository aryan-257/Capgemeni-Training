using System;

namespace DigitalPettyCashLedger
{
    public  abstract class Transaction : IReportable
    {
        #region Properties

        public int Id{ get; set;}
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        #endregion

        public abstract string GetSummary();

    }
}

