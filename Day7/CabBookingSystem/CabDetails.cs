using System;
using System.Runtime.CompilerServices;

namespace CabBookingSystem;

public class CabDetails :Cab

{
    public bool ValidateBookingID()
    {
        bool val=false;
        if (BookingID.Length == 6 )
        {
            val=true;
        }
        if(BookingID.StartsWith("AC@"))
        {
            val=true;

        }
        if(Char.IsDigit(BookingID[3]) && Char.IsDigit(BookingID[4]) && Char.IsDigit(BookingID[5]))
        {
            val=true;
        }
        else 
        val=false;
        return val;

        
    }
    public double CalculateFareAmount()
    {
        int ppk=0;
        if (CabType == "Hatchback")
        {
            ppk=10;
        }
        if (CabType =="Sedan")
        {
            ppk=20;
        }
        if (CabType == "SUV")
        {
            ppk=30;
        }
        double WaitingCharge=Math.Sqrt(WaitingTime);
        double fare=Distance *ppk*WaitingCharge;

        return fare;
    }
    

}
