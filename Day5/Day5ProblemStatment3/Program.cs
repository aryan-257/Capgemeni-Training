// See https://aka.ms/new-console-template for more information

using System;
using Day5ProblemStatment3;
public class Program
{
    public Candy CalculateDiscountedPriece(Candy candy)
    {
        candy.TotalPriece = candy.PricePerPiece * candy.Quantity;
        double disccontPercentage= 0;
        if (candy.Flavour == "Strawberry")
        {
            disccontPercentage = 15;
        }
        else if (candy.Flavour == "Lemon")
        {
            disccontPercentage = 10;
        }
        else if (candy.Flavour == "Mint")
        {
            disccontPercentage = 5;
        }

        candy.Discount = candy.TotalPriece - (candy.TotalPriece * disccontPercentage / 100);
        return candy;

    }

    public static void Main(string[] args)
    {
        Program p = new Program();
        Candy c = new Candy();

        Console.WriteLine("Enter the Flavour:");
        c.Flavour = Console.ReadLine();

        Console.WriteLine("Enter the Quantity:");
        c.Quantity = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the Price per piece:");
        c.PricePerPiece = Convert.ToInt32(Console.ReadLine());

        if (c.ValidateCandyFloavour())
        {
            c= p.CalculateDiscountedPriece(c);
            Console.WriteLine("Flavour: " + c.Flavour);
            Console.WriteLine("Quantity: " + c.Quantity);
            Console.WriteLine("Price per piece: " + c.PricePerPiece);
            Console.WriteLine("Total Price: " + c.TotalPriece);
            Console.WriteLine("Discount Priece: " + c.Discount);
        }
        else
        {
            Console.WriteLine("Invalid Flavour");
        }
    }
}
