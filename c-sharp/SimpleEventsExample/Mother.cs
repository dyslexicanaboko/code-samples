using System;

namespace SimpleEventsExample
{
    public class Mother
    {
        //Child object reference
        Baby _billy;

        public Mother()
        {
            //Child object
            _billy = new Baby();

            //Registering for a call back or listening for the baby to cry
            _billy.Cry += Billy_CryHandler;

            //Put the baby to sleep, the baby will wake up eventually
            _billy.Sleep();
        }

        //When the baby is awoken, you need to ask the baby 
        //why it woke up and put it back to sleep
        private void Billy_CryHandler(object sender, CryEventArgs e)
        {
            //The reason was passed over using our custom event argument object
            Console.WriteLine("Why are you awake Billy? - " + e.Reason + "\n");
            
            Console.WriteLine("Billy, go back to sleep!\n");

            //Put the baby back to bed
            _billy.Sleep();
        }
    }
}
