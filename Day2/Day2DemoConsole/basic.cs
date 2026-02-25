using System;

namespace Day2DemoConsole;

public class Basic
{
    public static void Show()
    {
        int i =0;
        string[] cities={"Pune","Chennai","Agra","Amritisar","mumbai"};
        while(i<cities.Length)
        {
            System.Console.WriteLine(cities[i]);
            i++;
            }
    }

    public static void Main()
    {
        Show();
    }
}
