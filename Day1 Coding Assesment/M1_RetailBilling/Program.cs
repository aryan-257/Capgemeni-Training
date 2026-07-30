using System;

class RetailBilling
{
    static void Main()
    {
        Console.Write("Enter Price: ");
        string priceInput = Console.ReadLine();

        Console.Write("Enter Quantity: ");
        string qtyInput = Console.ReadLine();

        Console.Write("Enter Discount (%): ");
        string discountInput = Console.ReadLine();

        //checking price valid h ya nhi
        if (!double.TryParse(priceInput, out double price) || price < 0)
        {
            Console.WriteLine("Invalid price entered. Price must be a valid non-negative number.");
            return;
        }

        // qty check
        if (!int.TryParse(qtyInput, out int quantity) || quantity < 0)
        {
            Console.WriteLine("Invalid quantity entered. Quantity must be a valid non-negative integer.");
            return;
        }

        //discount check 0-100 ke beech me hona chahiye
        if (!double.TryParse(discountInput, out double discount) || discount < 0 || discount > 100)
        {
            Console.WriteLine("Invalid discount entered. Discount must be between 0 and 100.");
            return;
        }

        // ab calculation start
        double subtotal = price * quantity;
        double discountAmount = subtotal * (discount / 100);
        double finalPayable = subtotal - discountAmount;

        //rounding off to 2 decimal
        subtotal = Math.Round(subtotal, 2);
        discountAmount = Math.Round(discountAmount, 2);
        finalPayable = Math.Round(finalPayable, 2);

        Console.WriteLine();
        Console.WriteLine("Subtotal: " + subtotal);
        Console.WriteLine("Discount Amount: " + discountAmount);
        Console.WriteLine("Final Payable Amount: " + finalPayable);

    }
}