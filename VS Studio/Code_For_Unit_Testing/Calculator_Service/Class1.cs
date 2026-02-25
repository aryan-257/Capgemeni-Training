using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Service
{
    public class Calculator
    {
        public int AddMe(int num1, int num2)
        {
            return num1 + num2;
        }

        public int SubMe(int num1, int num2) => num1 - num2;

        public int ProdMe(int num1, int num2) => num1 * num2;

        public float DivMe(int num1, int num2)
        {
            float numRes;
            if(num2>0)
            {
                numRes = num1 / num2;
            }

            else
            {
                throw new DivideByZeroException("You cant divide by zero");
            }

        }
    }
}
