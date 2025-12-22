using System;

namespace InterfaceXDemoProj;

public class MathClass:IAll,IAddSub
{
    public int AddMe(int num1, int num2)
    {
        return num1 + num2;
    }
    public int SubtMe(int num1, int num2)
    {
        return num1 - num2;
    }
    public int ProdtMe(int num1, int num2)
    {
        return num1 * num2;
    }
    public int DivMe(int num1, int num2)
    {
        if (num2 == 0)
        {
            throw new DivideByZeroException("Denominator cannot be zero.");
        }
        return num1 / num2;
    }


}
