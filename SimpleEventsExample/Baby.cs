using System;

namespace SimpleEventsExample
{
    public class Baby
    {
        //The model method that will be called when the event is raised "Call me back at"
        public delegate void CryHandler(object sender, CryEventArgs e);
        
        //The way that you can register for a call back
        public event CryHandler Cry;

        Random _r;

        public Baby()
        {
            _r = new Random();
        }

        public void Sleep()
        {
            //Get a value between 0 and 10 - which is indeterminate enough for this example
            int sleep = _r.Next(10);

            //Announce that the baby will be put back to bed
            Console.WriteLine("Baby will be sleeping for " + sleep + " seconds.\n");

            //Long running process
            System.Threading.Thread.Sleep(sleep * 1000);

            //Callback when the baby wakes up
            RaiseCryEvent(new CryEventArgs() { Reason = "I pissed myself again..." });
        }

        private void RaiseCryEvent(CryEventArgs e)
        {
            //This is syntactic sugar - a one liner that replaces 
            //the if statement in the prior example
            Cry?.Invoke(this, e);
        }
    }
}
