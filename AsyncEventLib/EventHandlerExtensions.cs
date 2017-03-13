using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEventLib
{
    public static class EventHandlerExtensions
    {
        public static Task InvokeAsync(this EventHandler<AsyncEventArgs> handler, object sender)
        {
            if(handler == null)
            {
                return Task.FromResult(0);
            }

            var taskList = new List<Task>();
            var args = new AsyncEventArgs(taskList);
            handler.Invoke(sender, args);
            return Task.WhenAll(taskList);
        }
    }
}
