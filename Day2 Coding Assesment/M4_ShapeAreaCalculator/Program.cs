using System;

class Program
{
    static void Main()
    {
        // call 1 - default 2 decimal places
        double circleArea1 = ShapeCalculator.CalculateArea(5);
        Console.WriteLine("Circle area (default precision): " + circleArea1);

        // call 2 - rectangle overload
        double rectangleArea = ShapeCalculator.CalculateArea(4, 6);
        Console.WriteLine("Rectangle area: " + rectangleArea);

        // call 3 - triangle overload
        double triangleArea = ShapeCalculator.CalculateArea(3, 7, true);
        Console.WriteLine("Triangle area: " + triangleArea);

        // call 4 - named argument use kiya specific precision ke liye
        double circleArea2 = ShapeCalculator.CalculateArea(radius: 5, decimals: 4);
        Console.WriteLine("Circle area (custom precision): " + circleArea2);
    }
}
