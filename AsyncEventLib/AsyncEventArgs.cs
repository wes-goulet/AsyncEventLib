using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncEventLib
{
    /// <summary>
    /// An event args class that provides a mechanism for event handlers to actually 
    /// have their async code awaited by the event publisher
    /// </summary>
    public class AsyncEventArgs : EventArgs
    {
        /// <summary>
        /// NOTE: instead of directly calling this ctor it is much easier for an event publisher
        /// to use the <see cref="EventHandlerExtensions.InvokeAsync"/> extension method,
        /// which automatically takes care of instantiating the <param name="taskList"></param>.
        /// </summary>
        /// <param name="taskList">An empty list that event publisher should await on directly after invoking event</param>
        public AsyncEventArgs(List<Task> taskList) 
        {
            _handlerTasks = taskList;
        }

        private readonly List<Task> _handlerTasks;

        /// <summary>
        /// Handlers should run any async code that they want awaited by the event publisher
        /// via passing that code into the <param name="taskFunc"></param>
        /// </summary>
        /// <param name="taskFunc">The code the handler would like to run</param>
        public void RegisterFuncToBeAwaitedByEventPublisher(Func<Task> taskFunc)
        {
            _handlerTasks.Add(taskFunc());
        }
    }
}