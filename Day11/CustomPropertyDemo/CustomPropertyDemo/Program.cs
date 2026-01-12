using System;
namespace CustomPropertyDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Custom Property Demo");
            Customer cust = new Customer();
            cust.CustomerId = 101;
            cust.CustomerName = "John Doe";
            Address addr = new Address();
            addr.FlatNumber = 12;
            addr.BuilingName = "Sunshine Apartments";
            addr.Street = "Maple Street";
            addr.City = "Springfield";
            addr.Locality = "Downtown";
            cust.MyProperty = addr;
            Console.WriteLine($"Customer ID: {cust.CustomerId}");
            Console.WriteLine($"Customer Name: {cust.CustomerName}");
            Console.WriteLine("Address:");
            Console.WriteLine($"  Flat Number: {cust.MyProperty.FlatNumber}");
            Console.WriteLine($"  Building Name: {cust.MyProperty.BuilingName}");
            Console.WriteLine($"  Street: {cust.MyProperty.Street}");
            Console.WriteLine($"  City: {cust.MyProperty.City}");
            Console.WriteLine($"  Locality: {cust.MyProperty.Locality}");
        }
    }
}