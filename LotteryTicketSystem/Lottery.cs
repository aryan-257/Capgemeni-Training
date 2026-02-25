using System;
using System.Collections.Generic;
using System.Text;

namespace LotteryTicketSystem
{
    public class Lottery
    {
        CustomEventArgs custObj = null;

        public Lottery()
        {
            custObj = new CustomEventArgs();
        }

        public void gettickets()
        {
            Console.Write("Enter Quantity: ");
            custObj.Quantity = Int32.Parse(Console.ReadLine());

            try
            {
                if (custObj.Quantity < 10)
                {
                    throw new InvalidOperationException();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
