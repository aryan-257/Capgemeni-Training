using System;
using System.Collections.Generic;

public class UserInterface
{
    public static void Main(string[] args)
    {
        CakeOrder cakeOrder = new CakeOrder();

        Console.WriteLine("Enter number of cake orders to be added");
        int count = int.Parse(Console.ReadLine());

        Console.WriteLine("Enter the cake order details (Order Id: CakeCost)");

        for (int i = 0; i < count; i++)
        {
            string input = Console.ReadLine();
            string[] data = input.Split(':');

            string orderId = data[0];
            double cost = double.Parse(data[1]);

            cakeOrder.AddOrderDetails(orderId, cost);
        }

        Console.WriteLine("Enter the cost to search the cake orders");
        double searchCost = double.Parse(Console.ReadLine());

        Dictionary<string, double> result =
            cakeOrder.FindOrdersAboveSpecifiedCost(searchCost);

        if (result.Count == 0)
        {
            Console.WriteLine("No cake orders found");
        }
        else
        {
            Console.WriteLine("Cake Orders above the specified cost");

            foreach (var item in result)
            {
                Console.WriteLine(
                    "Order ID: " + item.Key + ", Cake Cost: " + item.Value
                );
            }
        }
    }
}
