using System;

namespace Day5;

public class Laptop : Computer
{
    public int DisplaySize { get; set; }
    public int BatteryVolt { get; set; }
    public double LaptopPriceCalculation()
    {
        double processorCost = 0;
        if (Processor == "i3")
        {
            processorCost = 2500;
        }
        else if (Processor == "i5")
        {
            processorCost = 5000;
        }
        else if (Processor == "i7")
        {
            processorCost = 6500;
        }

        double totalPrice = processorCost + (RamSize * 200) + (HardDiskSize * 1500) + (GraphicCard * 2500) + (DisplaySize * 250) + (BatteryVolt * 20);
        return totalPrice;
    }

}
