using System;

namespace EventDrivenPhoneCall
{
   public delegate void Notify( );

   public class PhoneCall
   {
      public event Notify   PhoneCallEvent;

      public string Message{ get; private set; }

      bool isSubscribed=false;

      private void OnSubscribe( )
      {
         Message = "Subscribed to Call";
         Console.WriteLine( "["+ DateTime.Now +" ] Subscription Activated" );
      }

      private void OnUnSubscribe()
      {
        Message="UnSubscribed to Call";
        Console.WriteLine("["+DateTime.Now+"] Subscription Deactivated");
      }

      public void MakeAPhoneCall( bool notify )
      {
         if( notify==true )
         {
            if(!isSubscribed)
            {
              PhoneCallEvent += OnSubscribe;
              isSubscribed = true;
            }
         }
         else
         {
            PhoneCallEvent+= OnUnSubscribe;
         }

         PhoneCallEvent?.Invoke( );

         PhoneCallEvent = null;
      }
   }
}
