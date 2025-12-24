using System;

namespace Day5;

public class Desktop : Computer
{
    public int MonitorSize { get; set; }
    public int PowerSupplyVolt { get; set; }
    public double DesktopPriceCalculation()
    {
        double processorCost = 0;
        if (Processor == "i3")
        {
            processorCost = 1500;
        }
        else if (Processor == "i5")
        {
            processorCost = 3000;
        }
        else if (Processor == "i7")
        {
            processorCost = 4500;
        }

        double totalPrice = processorCost + (RamSize * 200) + (HardDiskSize * 1500) + (GraphicCard * 2500) + (MonitorSize * 250) + (PowerSupplyVolt * 20);
        return totalPrice;
    }

}
