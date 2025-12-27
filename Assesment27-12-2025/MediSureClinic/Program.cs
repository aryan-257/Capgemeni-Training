using System;
using MediSureClinic;

namespace MediSureClinic
{
    class Program
    {
        static void Main(string[] args)
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("================== MediSure Clinic Billing ==================");
                Console.WriteLine("1. Create New Bill (Enter Patient Details)");
                Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateBill();
                        break;

                    case "2":
                        ViewBill();
                        break;

                    case "3":
                        ClearBill();
                        break;

                    case "4":
                        Console.WriteLine("Thank you. Application closed normally.");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CreateBill()
        {
            PatientBill bill = new PatientBill();

            Console.Write("Enter Bill Id: ");
            bill.BillId = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(bill.BillId))
            {
                Console.WriteLine("Bill Id cannot be empty.");
                return;
            }

            Console.Write("Enter Patient Name: ");
            bill.PatientName = Console.ReadLine();

            Console.Write("Is the patient insured? (Y/N): ");
            string ins = Console.ReadLine();
            bill.HasInsurance = (ins == "Y" || ins == "y");

            Console.Write("Enter Consultation Fee: ");
            decimal consult;
            if (!decimal.TryParse(Console.ReadLine(), out consult) || consult <= 0)
            {
                Console.WriteLine("Consultation fee must be greater than 0.");
                return;
            }
            bill.ConsultationFee = consult;

            Console.Write("Enter Lab Charges: ");
            decimal lab;
            if (!decimal.TryParse(Console.ReadLine(), out lab) || lab < 0)
            {
                Console.WriteLine("Lab charges cannot be negative.");
                return;
            }
            bill.LabCharges = lab;

            Console.Write("Enter Medicine Charges: ");
            decimal med;
            if (!decimal.TryParse(Console.ReadLine(), out med) || med < 0)
            {
                Console.WriteLine("Medicine charges cannot be negative.");
                return;
            }
            bill.MedicineCharges = med;

            bill.CalculateBill();

            PatientBill.LastBill = bill;
            PatientBill.HasLastBill = true;

            Console.WriteLine();
            Console.WriteLine("Bill created successfully.");
            Console.WriteLine("Gross Amount: " + bill.GrossAmount.ToString("0.00"));
            Console.WriteLine("Discount Amount: " + bill.DiscountAmount.ToString("0.00"));
            Console.WriteLine("Final Payable: " + bill.FinalPayable.ToString("0.00"));
            Console.WriteLine("------------------------------------------------------------");
        }

        static void ViewBill()
        {
            if (!PatientBill.HasLastBill)
            {
                Console.WriteLine("No bill available. Please create a new bill first.");
                return;
            }

            PatientBill.LastBill.PrintBill();
        }
    static void ClearBill()
        {
            PatientBill.LastBill = null;
            PatientBill.HasLastBill = false;
            Console.WriteLine("Last bill cleared.");
        }
    }
}
