// See https://aka.ms/new-console-template for more information
using Day5;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("1.Desktop");
        Console.WriteLine("2.Laptop");
        Console.WriteLine("Choose the option:");
        int choice = int.Parse(Console.ReadLine()!);

        if (choice == 1)
        {
            Desktop d = new Desktop();
            Console.WriteLine("Enter Processor :");
            d.Processor = Console.ReadLine()!;
            Console.WriteLine("Enter Ram Size :");
            d.RamSize = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Hard Disk Size :");
            d.HardDiskSize = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Graphic Card :");
            d.GraphicCard = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Monitor Size :");
            d.MonitorSize = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Power Supply Volt :");
            d.PowerSupplyVolt = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Desktop price is :" + d.DesktopPriceCalculation());
        }

        else if (choice == 2)
        {
            Laptop l = new Laptop();
            Console.WriteLine("Enter Processor :");
            l.Processor = Console.ReadLine()!;
            Console.WriteLine("Enter Ram Size :");
            l.RamSize = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Hard Disk Size :");
            l.HardDiskSize = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Graphic Card :");
            l.GraphicCard = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Display Size :");
            l.DisplaySize = int.Parse(Console.ReadLine()!);
            Console.WriteLine("Enter Battery Volt :");
            l.BatteryVolt = int.Parse(Console.ReadLine()!);

            Console.WriteLine("Laptop price is :" + l.LaptopPriceCalculation());
        }
    }
}