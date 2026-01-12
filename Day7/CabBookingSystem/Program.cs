// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using CabBookingSystem;

public class Program()
{
    public static void Main()
    {
        CabDetails obj2=new CabDetails();
        System.Console.WriteLine("Enter YOur Booking ID");
        obj2.BookingID =Console.ReadLine();
        
        
        if (obj2.ValidateBookingID())
        {
            System.Console.WriteLine("Enter CabType");
            obj2.CabType=Console.ReadLine();
            System.Console.WriteLine("Enetr Distance");
            obj2.Distance=Double.Parse(Console.ReadLine()!);
            System.Console.WriteLine("Enter Waiting Time");
            obj2.WaitingTime=Int32.Parse(Console.ReadLine()!);
            double fare=obj2.CalculateFareAmount();
        Console.WriteLine($"Fare is : {fare}");
        
            
        }
        else
        {
            System.Console.WriteLine("Invalid ID");
        }
        

    }
}

