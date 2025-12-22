using System;
class Program{
    static void Main(){
        Console.Write("Enter value in m: ");
        double feet = double.Parse(Console.ReadLine());
        double cm = feet * 30.48;
        Console.WriteLine("Value in cm: " + cm);
    }
}