using System;

class BankBalance
{
    static void Main()
    {
        Console.Write("Enter Opening Balance: ");
        string openingInput = Console.ReadLine();

        Console.Write("Enter Total Deposits: ");
        string depositInput = Console.ReadLine();

        Console.Write("Enter Total Withdrawals: ");
        string withdrawInput = Console.ReadLine();

        if (!double.TryParse(openingInput, out double opening) || opening < 0)
        {
            Console.WriteLine("Invalid opening balance entered.");
            return;
        }

        if (!double.TryParse(depositInput, out double deposits) || deposits < 0)
        {
            Console.WriteLine("Invalid deposit amount entered.");
            return;
        }

        if (!double.TryParse(withdrawInput, out double withdrawals) || withdrawals < 0)
        {
            Console.WriteLine("Invalid withdrawal amount entered.");
            return;
        }

        double availableFunds = opening + deposits;

        //agar withdrawal available se zyada h to error
        if (withdrawals > availableFunds)
        {
            Console.WriteLine("Error: Withdrawal amount exceeds available balance.");
            return;
        }

        double finalBalance = availableFunds - withdrawals;
        finalBalance = Math.Round(finalBalance, 2);

        Console.WriteLine();
        Console.WriteLine("Updated Balance: " + finalBalance);
    }
}