using System;

interface IBillCalculator
{
    double CalculateBill(double units, double rate, double fixedCharges);
}

class ResidentialCustomer : IBillCalculator
{
    public double CalculateBill(double units, double rate, double fixedCharges)
    {
        //residential me simple hai, direct units*rate + fixed
        double bill = (units * rate) + fixedCharges;
        return bill;
    }
}

class CommercialCustomer : IBillCalculator
{
    public double CalculateBill(double units, double rate, double fixedCharges)
    {
        // commercial walo pe 20% extra surcharge lagta h
        double baseBill = (units * rate) + fixedCharges;
        double surcharge = baseBill * 0.20;
        return baseBill + surcharge;
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter Customer Type (Residential/Commercial): ");
        string customerType = Console.ReadLine();

        Console.Write("Enter Units Consumed: ");
        string unitsInput = Console.ReadLine();

        Console.Write("Enter Rate per unit: ");
        string rateInput = Console.ReadLine();

        Console.Write("Enter Fixed Charges: ");
        string fixedInput = Console.ReadLine();

        //validation for units
        if (!double.TryParse(unitsInput, out double units) || units < 0)
        {
            Console.WriteLine("Invalid units entered. Must be non-negative number.");
            return;
        }

        //validation for rate
        if (!double.TryParse(rateInput, out double rate) || rate < 0)
        {
            Console.WriteLine("Invalid rate entered. Must be non-negative number.");
            return;
        }

        //validation for fixed charges
        if (!double.TryParse(fixedInput, out double fixedCharges) || fixedCharges < 0)
        {
            Console.WriteLine("Invalid fixed charges entered.");
            return;
        }

        IBillCalculator calculator;

        //customer type ke hisab se object banate h
        if (customerType.Trim().ToLower() == "residential")
        {
            calculator = new ResidentialCustomer();
        }
        else if (customerType.Trim().ToLower() == "commercial")
        {
            calculator = new CommercialCustomer();
        }
        else
        {
            Console.WriteLine("Invalid customer type. Please enter Residential or Commercial.");
            return;
        }

        double finalBill = calculator.CalculateBill(units, rate, fixedCharges);
        finalBill = Math.Round(finalBill, 2);

        Console.WriteLine();
        Console.WriteLine("Final Bill Amount: " + finalBill);
    }
}