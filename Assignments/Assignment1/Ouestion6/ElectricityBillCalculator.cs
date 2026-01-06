using System;

namespace Question6
{
    class ElectricityBillCalculator
    {
        public static void CalculateBill()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Units: ");
            int unitsConsumed = Convert.ToInt32(Console.ReadLine());
            
            double billAmount = 0;

            if (unitsConsumed <= 199)
            {
                billAmount = unitsConsumed * 1.20;
            }
            else if (unitsConsumed >= 200 && unitsConsumed < 400)
            {
                billAmount = unitsConsumed * 1.50;
            }
            else if (unitsConsumed >= 400 && unitsConsumed < 600)
            {
                billAmount = unitsConsumed * 1.80;
            }
            else
            {
                billAmount = unitsConsumed * 2.00;
            }

            if (billAmount > 400)
            {
                billAmount = billAmount + (billAmount * 0.15);
            }

            Console.WriteLine("Customer Name: " + name);
            Console.WriteLine("Units Consumed: " + unitsConsumed);
            Console.WriteLine("Total Bill: " + billAmount + " Rs");
        }
    }
}