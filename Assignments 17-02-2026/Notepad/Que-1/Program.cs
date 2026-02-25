using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static SortedDictionary<string,long> itemDetails=
        new SortedDictionary<string,long>();

    public static void Main(string[] args)
    {
        itemDetails.Add("Laptop",50);
        itemDetails.Add("Mouse",150);
        itemDetails.Add("Keyboard",100);
        itemDetails.Add("Monitor",20);

        long countToFind=Convert.ToInt64(Console.ReadLine());

        Program p=new Program();

        var foundItems=p.FindItemDetails(countToFind);
        if(foundItems.Count==0)
        {
            Console.WriteLine("Invalid sold count");
        }
        else
        {
            foreach(var item in foundItems)
                Console.WriteLine(item.Key+" "+item.Value);
        }

        List<string> minMax=p.FindMinandMaxSoldItems();
        if(minMax.Count>=2)
        {
            Console.WriteLine(minMax[0]);
            Console.WriteLine(minMax[1]);
        }

        var sortedDict=p.SortByCount();
        foreach(var item in sortedDict)
            Console.WriteLine(item.Key+" "+item.Value);
    }

    public SortedDictionary<string,long> FindItemDetails(long soldCount)
    {
        var result=itemDetails.Where(x=>x.Value==soldCount)
                              .ToDictionary(x=>x.Key,x=>x.Value);

        return new SortedDictionary<string,long>(result);
    }

    public List<string> FindMinandMaxSoldItems()
    {
        List<string> result=new List<string>();
        var sortedList=itemDetails.OrderBy(x=>x.Value).ToList();

        if(sortedList.Count>0)
        {
            result.Add(sortedList.First().Key);
            result.Add(sortedList.Last().Key);
        }
        return result;
    }

    public Dictionary<string,long> SortByCount()
    {
        return itemDetails.OrderBy(x=>x.Value)
                          .ToDictionary(p=>p.Key,p=>p.Value);
    }
}
