using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEventLib
{
    public class AsyncEventArgs : EventArgs
    {
        public AsyncEventArgs(List<Task> taskList) 
        {
            _handlerTasks = taskList;
        }

        private readonly List<Task> _handlerTasks;

        public void RegisterTask(Func<Task> taskFunc)
        {
            _handlerTasks.Add(taskFunc());
        }
    }
}