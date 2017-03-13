using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;
using FluentAssertions;
using System.Threading;

namespace AsyncEventLib.Tests
{
    [TestClass]
    public class AsyncEventTests
    {
        private event EventHandler<AsyncEventArgs> TestEvent;

        [TestMethod]
        public async Task Test_Event_DoesWaitOnAsync()
        {
            var asyncMethodDone = false;

            TestEvent += (sender, args) =>
            {
                args.RegisterTask(async () =>
                {
					await Task.Delay(50);
					asyncMethodDone = true;
                });
            };

            var asyncArgs = AsyncEventArgs.Create();
            TestEvent?.Invoke(this, asyncArgs);
            await asyncArgs.AwaitHandlers();

            asyncMethodDone.Should().BeTrue();
        }

        [TestMethod]
        public void Test_Event_DoesWaitOnSync()
        {
            var asyncMethodDone = false;

            TestEvent += (sender, args) =>
            {
                Thread.Sleep(50);
                asyncMethodDone = true;
            };

			var asyncArgs = AsyncEventArgs.Create();
            TestEvent?.Invoke(this, asyncArgs);

            asyncMethodDone.Should().BeTrue();
        }
    }
}
