using System;

class Program
{
    static void SwapRef(ref int a,ref int b)
{
    a=a+b;
    b=a-b;
    a=a-b;
}

static void SwapOut(int x,int y,out int newX,out int newY)
{
    newX=y;
    newY=x;
}

static void Main()
    {
        int num1=10;
        int num2=20;
        Console.WriteLine("Before Swap(ref) num1="+num1+" num2="+num2);
        SwapRef(ref num1,ref num2);
        Console.WriteLine("After Swap(ref) num1="+num1+" num2="+num2);

    int a=30;
    int b=40;
    int newA,newB;
    Console.WriteLine("Before Swap(out) a="+a+" b="+b);
    SwapOut(a,b,out newA,out newB);
    Console.WriteLine("After Swap(out) newA="+newA+" newB="+newB);

    Console.ReadKey();
    }
}
