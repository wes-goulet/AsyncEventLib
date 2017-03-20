using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEventLib
{
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// Invokes the event handlers if there are any. Safe to call even if there aren't any
        /// handlers registered. If a handler has registered an async Task then this call will
        /// properly await all tasks.
        /// NOTE: the registered async Tasks will run in parallel.
        /// </summary>
        /// <param name="handlerToInvoke"></param>
        /// <param name="sender"></param>
        /// <returns></returns>
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
