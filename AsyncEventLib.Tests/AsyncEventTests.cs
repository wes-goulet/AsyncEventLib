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
        public void Test_Event_DoesWaitOnAsync()
        {
            var asyncMethodDone = false;

            TestEvent += async (sender, args) =>
            {
                await Task.Delay(50);
                asyncMethodDone = true;
            };

            TestEvent?.Invoke(this, EventArgs.Empty);

            asyncMethodDone.Should().BeFalse();
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

            TestEvent?.Invoke(this, EventArgs.Empty);

            asyncMethodDone.Should().BeTrue();
        }
    }
}