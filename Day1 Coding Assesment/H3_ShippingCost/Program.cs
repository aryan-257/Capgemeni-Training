using System;

interface IShippingCost
{
    double CalculateCost(double weight, double distance);
}

class StandardPackage : IShippingCost
{
    public double CalculateCost(double weight, double distance)
    {
        // standard - simple rate
        return (weight * 5) + (distance * 0.5);
    }
}

class ExpressPackage : IShippingCost
{
    public double CalculateCost(double weight, double distance)
    {
        //express thoda mehenga hoga
        double baseCost = (weight * 8) + (distance * 0.8);
        return baseCost * 1.25; //25% extra for express
    }
}

class FragilePackage : IShippingCost
{
    public double CalculateCost(double weight, double distance)
    {
        // fragile me handling charge fix add hota h
        double baseCost = (weight * 6) + (distance * 0.6);
        return baseCost + 150; //fix handling charge
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter Package Type (Standard/Express/Fragile): ");
        string packageType = Console.ReadLine();

        Console.Write("Enter Weight (kg): ");
        string weightInput = Console.ReadLine();

        Console.Write("Enter Distance (km): ");
        string distanceInput = Console.ReadLine();

        //weight validation - koi bhi unreal high weight bhi reject
        if (!double.TryParse(weightInput, out double weight) || weight <= 0 || weight > 1000)
        {
            Console.WriteLine("Invalid weight entered. Must be between 0 and 1000 kg.");
            return;
        }

        //distance validation
        if (!double.TryParse(distanceInput, out double distance) || distance <= 0)
        {
            Console.WriteLine("Invalid distance entered. Must be a positive number.");
            return;
        }

        IShippingCost shipping;

        string type = packageType.Trim().ToLower();

        if (type == "standard")
        {
            shipping = new StandardPackage();
        }
        else if (type == "express")
        {
            shipping = new ExpressPackage();
        }
        else if (type == "fragile")
        {
            shipping = new FragilePackage();
        }
        else
        {
            Console.WriteLine("Invalid package type entered.");
            return;
        }

        double cost = shipping.CalculateCost(weight, distance);
        cost = Math.Round(cost, 2);

        Console.WriteLine();
        Console.WriteLine("Shipping Cost: " + cost);
    }
}