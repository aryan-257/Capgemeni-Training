using System;
using EventDrivenPhoneCall;

namespace EventDrivenPhoneCallApp
{
 class Program
 {
    static void Main(string[] args)
    {
        PhoneCall  phoneCall=new PhoneCall( );

        phoneCall.MakeAPhoneCall(true);
        Console.WriteLine( phoneCall.Message );

        Console.WriteLine();

        phoneCall.MakeAPhoneCall( false );
        Console.WriteLine(phoneCall.Message);

        Console.ReadLine( );
    }
 }
}
