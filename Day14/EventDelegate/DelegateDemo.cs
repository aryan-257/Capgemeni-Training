using System;

namespace EventDelegate;

    //Multicast Delegate
    public delegate void GreetMsg(string msg);
    //Unicast Delegate
    public delegate int Calculation(int num1, int num2);



public class DelegateDemo
{
    public static void DelegateDemoMain()
    {
        Tamil tObj = new Tamil();
        GreetMsg GreetInTamil = new GreetMsg(tObj.WelcomeMsg);
        GreetInTamil(" Aryan");
    }
    
}
