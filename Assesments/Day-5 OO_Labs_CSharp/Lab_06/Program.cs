using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            //Accept values from the user
            int Number1, Number2;

            //Storing the numbers accepted in SwapData object
            SwapData Obj = new SwapData();

            //Display numbers before swapping
            Obj.DisplayValues("Before Swapping :");

            //Swapping the numbers in the object
            Obj.SwapValues();

            //Display numbers after swapping
            Obj.DisplayValues("After Swapping :");
        }
    }
}
