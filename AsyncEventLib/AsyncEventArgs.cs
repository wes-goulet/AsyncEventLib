using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEventLib
{
    public class AsyncEventArgs : EventArgs
    {
        public static AsyncEventArgs Create()
        {
            return new AsyncEventArgs();
        }

        private AsyncEventArgs() { }

        private readonly List<Task> _handlerTasks = new List<Task>();

        public void RegisterTask(Func<Task> taskFunc)
        {
            _handlerTasks.Add(taskFunc());
        }

        public Task AwaitHandlers()
        {
            return Task.WhenAll(_handlerTasks);
        }
    }
}