using System;

static class MathOperations
{
    // simple 2-number overload
    public static int Add(int a, int b)
    {
        return a + b;
    }

    // params wala overload - kitne bhi numbers le sakta h
    public static int Add(params int[] numbers)
    {
        int sum = 0;
        foreach (int n in numbers)
        {
            sum = sum + n;
        }
        return sum;
    }

    public static int Multiply(int a, int b)
    {
        return a * b;
    }

    public static int Multiply(params int[] numbers)
    {
        int product = 1;
        foreach (int n in numbers)
        {
            product = product * n;
        }
        return product;
    }
}
