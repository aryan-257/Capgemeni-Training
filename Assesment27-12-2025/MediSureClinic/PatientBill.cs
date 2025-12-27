using System;

namespace MediSureClinic
{
    public class PatientBill
    {
        public string BillId { get; set; }
        public string PatientName { get; set; }
        public bool HasInsurance { get; set; }
        public decimal ConsultationFee { get; set; }
        public decimal LabCharges { get; set; }
        public decimal MedicineCharges { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalPayable { get; set; }

        public static PatientBill LastBill;
        public static bool HasLastBill = false;

        public void CalculateBill()
        {
            GrossAmount = ConsultationFee + LabCharges + MedicineCharges;

            if (HasInsurance)
            {
                DiscountAmount = GrossAmount * 0.10m;
            }
            else
            {
                DiscountAmount = 0;
            }

            FinalPayable = GrossAmount - DiscountAmount;
        }

        public void PrintBill()
        {
            Console.WriteLine("----------- Last Bill -----------");
            Console.WriteLine("BillId: " + BillId);
            Console.WriteLine("Patient: " + PatientName);
            Console.WriteLine("Insured: " + (HasInsurance ? "Yes" : "No"));
            Console.WriteLine("Consultation Fee: " + ConsultationFee.ToString("0.00"));
            Console.WriteLine("Lab Charges: " + LabCharges.ToString("0.00"));
            Console.WriteLine("Medicine Charges: " + MedicineCharges.ToString("0.00"));
            Console.WriteLine("Gross Amount: " + GrossAmount.ToString("0.00"));
            Console.WriteLine("Discount Amount: " + DiscountAmount.ToString("0.00"));
            Console.WriteLine("Final Payable: " + FinalPayable.ToString("0.00"));
            Console.WriteLine("--------------------------------");
        }
    }
}
