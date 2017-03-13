using System;
using System.Threading.Tasks;

namespace AsyncEventLib
{
    public class Class1
    {
        public event EventHandler<EventArgs> MyAsyncEvent;

        private async Task DoSomethingAsync()
        {
            MyAsyncEvent?.Invoke(this, EventArgs.Empty);
        }
    }
}