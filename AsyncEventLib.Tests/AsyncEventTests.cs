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

            await TestEvent.InvokeAsync(this);

            asyncMethodDone.Should().BeTrue();
        }

        [TestMethod]
        public async Task Test_Event_DoesWaitOnSync()
        {
            var asyncMethodDone = false;

            TestEvent += (sender, args) =>
            {
                Thread.Sleep(50);
                asyncMethodDone = true;
            };

            await TestEvent.InvokeAsync(this);

            asyncMethodDone.Should().BeTrue();
        }
    }
}
