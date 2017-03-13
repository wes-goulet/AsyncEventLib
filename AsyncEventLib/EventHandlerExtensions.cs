using System;
using System.Threading.Tasks;

namespace AsyncEventLib
{
    public static class EventHandlerExtensions
    {
        public static Task InvokeAsync(this EventHandler<AsyncEventArgs> handler, object sender, AsyncEventArgs e)
        {
            if(handler == null)
            {
                return Task.FromResult(0);
            }

            handler.Invoke(sender, e);
            return e.AwaitHandlers();
        }
    }
}
