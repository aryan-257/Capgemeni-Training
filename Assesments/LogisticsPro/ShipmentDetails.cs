using System;

public class ShipmentDetails : Shipment
{ 
    public bool ValidateShipmentCode()
    {
        if (ShipmentCode.Length == 7 &&
            ShipmentCode.StartsWith("GC#") &&
            int.TryParse(ShipmentCode.Substring(3), out _))
        {
            return true;
        }
        return false;
    }
    public double CalculateTotalCost()
    {
        double ratePerKg = 0;

        if (TransportMode == "Sea")
            ratePerKg = 15.00;
        else if (TransportMode == "Air")
            ratePerKg = 50.00;
        else if (TransportMode == "Land")
            ratePerKg = 25.00;

        double totalCost = (Weight * ratePerKg) + Math.Sqrt(StorageDays);

        return Math.Round(totalCost, 2);
    }
}
