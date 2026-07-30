using System;

static class ShapeCalculator
{
    // circle - default decimal places 2
    public static double CalculateArea(double radius, int decimals = 2)
    {
        double area = Math.PI * radius * radius;
        return Math.Round(area, decimals);
    }

    // rectangle - alag overload, 2 parameters
    public static double CalculateArea(double length, double width)
    {
        return length * width;
    }

    // triangle - teen parameters se overload alag ho jata h
    public static double CalculateArea(double baseLength, double height, bool isTriangle)
    {
        return 0.5 * baseLength * height;
    }
}
