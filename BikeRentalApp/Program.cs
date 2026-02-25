using System;
using System.Collections.Generic;

namespace BikeRentalApp
{
    class Program
    {
        public static SortedDictionary<int,Bike> bikeDetails=
            new SortedDictionary<int,Bike>();

        static void Main(string[] args)
        {
            BikeUtility util=new BikeUtility();
            int choice=0;

            while(choice!=3)
            {
                Console.WriteLine("1. Add Bike Details");
                Console.WriteLine("2. Group Bikes By Brand");
                Console.WriteLine("3. Exit");
                Console.WriteLine();
                Console.WriteLine("Enter your choice");
                choice=int.Parse(Console.ReadLine());

                switch(choice)
                {
                    case 1:
                        Console.WriteLine("Enter the model");
                        string model=Console.ReadLine();

                        Console.WriteLine("Enter the brand");
                        string brand=Console.ReadLine();

                        Console.WriteLine("Enter the price per day");
                        int price=int.Parse(Console.ReadLine());

                        util.AddBikeDetails(model,brand,price);
                        Console.WriteLine("Bike details added successfully");
                        Console.WriteLine();
                        break;

                    case 2:
                        var data=util.GroupBikesByBrand();

                        if(data.Count==0)
                        {
                            Console.WriteLine("No bikes available");
                            Console.WriteLine();
                        }
                        else
                        {
                            foreach(var item in data)
                            {
                                Console.WriteLine(item.Key);
                                foreach(var b in item.Value)
                                {
                                    Console.WriteLine(b.Model);
                                }
                                Console.WriteLine();
                            }
                        }
                        break;

                    case 3:
                        break;
                }
            }
        }
    }
}
