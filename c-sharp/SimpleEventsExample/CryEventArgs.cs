using System;

namespace SimpleEventsExample
{
    public class CryEventArgs : EventArgs
    {
        public string Reason { get; set; }
    }
}
