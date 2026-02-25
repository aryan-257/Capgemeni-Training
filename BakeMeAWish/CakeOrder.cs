using System.Collections.Generic;

public class CakeOrder
{
    private Dictionary<string, double> orderMap = new Dictionary<string, double>();

    public Dictionary<string, double> OrderMap
    {
        get { return orderMap; }
        set { orderMap = value; }
    }

    public void AddOrderDetails(string orderId, double cakeCost)
    {
        orderMap[orderId] = cakeCost;
    }

    public Dictionary<string, double> FindOrdersAboveSpecifiedCost(double cakeCost)
    {
        Dictionary<string, double> result = new Dictionary<string, double>();

        foreach (var item in orderMap)
        {
            if (item.Value > cakeCost)
            {
                result[item.Key] = item.Value;

            }
        }

        return result;
    }
}
