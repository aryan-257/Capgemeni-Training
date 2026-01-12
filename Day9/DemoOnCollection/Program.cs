// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");

using System;

namespace DemoOnCollection;
    class Program
    {
        public void ArrayDemoFunc()
        {
            int[] arrNum =new int[5];
            int[] arrNum1 =new int[3] {10,20,30};
            string[] cities={"Hyd",
                            "Banglore",
                            "Chennai",
                            "Mumbai",
                            "Delhi",
                            "Kolkata",
                            "pune",
                            "Goa"};
        Customer[]  custArray = new Customer[1];
        
        custArray[0] = new Customer();
        custArray[0].CustomerId=101;
        custArray[0].CustomerName="Ravi";
        //initialize the Adress class
        custArray[0].BillingAddress=new Address();
        custArray[0].BillingAddress.FlatNo="12-34/A";
        custArray[0].BillingAddress.BuildingName="Green Heights";
        custArray[0].BillingAddress.Street="Main Street";
        custArray[0].BillingAddress.City="Hyderabad";

        custArray[0].ShippingAddress=custArray[0].BillingAddress;
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.ArrayDemoFunc();
    
        }
    }