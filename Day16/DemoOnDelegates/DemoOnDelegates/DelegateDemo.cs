using System;
using System.Collections.Generic;
using System.Text;

namespace DemoOnDelegates
{
    //multicast delegate
    public delegate void GreetMsg(string msg);
    //unicast delegate
    public delegate int Calculation(int num1, int num2);
    class hindi
    {
        public void WelcomeMsg(string userName)
        {
            Console.WriteLine("Suprabhat " + userName);
        }

    }
    class Tamil
    {
        public void WelcomeMsg(string userName)
        {
            Console.WriteLine("Kaalai Vanakkam " + userName);
        }
    }
    class English
    {
        public void WelcomeMsg(string userName)
        {
            Console.WriteLine("Good Morning " + userName);
        }
    }
    class Marathiu
    { 
        public void WelcomeMsg(string userName)
        {
            Console.WriteLine("Shubh Prabhat " + userName);
        }
    }
    public class DelegateDemo
    {
        public static void DelegateDemoMain()
        {
            //multicast delegate
            GreetMsg greetMsg;
            hindi hi = new hindi();
            Tamil ta = new Tamil();
            English en = new English();
            Marathiu ma = new Marathiu();
            greetMsg = hi.WelcomeMsg;
            greetMsg += ta.WelcomeMsg;
            greetMsg += en.WelcomeMsg;
            greetMsg += ma.WelcomeMsg;
            greetMsg("Ajay");
            //unicast delegate
            Calculation calc;
            calc = Addition;
            int addResult = calc(10, 20);
            Console.WriteLine("Addition: " + addResult);
            calc = Subtraction;
            int subResult = calc(30, 15);
            Console.WriteLine("Subtraction: " + subResult);
        }


    }
}
