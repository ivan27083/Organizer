using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin_test.Classes
{
    public class TouchEventArgs<T> : EventArgs
    {
        public T EventData { get; private set; }
        public TouchEventArgs(T EventData)
        {
            this.EventData = EventData;
        }
    }
}
