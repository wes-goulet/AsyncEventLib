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

        [TestInitializeAttribute]
        public void TestInit()
        {
            if (TestEvent != null)
            {
                foreach (var d in TestEvent.GetInvocationList())
                {
                    TestEvent -= (d as EventHandler<AsyncEventArgs>);
                }
            }
        }

        [TestMethod]
        public async Task Test_Event_DoesWaitOnAsync()
        {
            var asyncMethodDone = false;

            TestEvent += (sender, args) =>
            {
                args.RegisterFuncToBeAwaitedByEventPublisher(async () =>
                {
                    await Task.Delay(50);
                    asyncMethodDone = true;
                });
            };

            await TestEvent.InvokeAsync(this);

            asyncMethodDone.Should().BeTrue();
        }

        [TestMethod]
        public async Task Test_Event_NonRegisteredCodeDoesNotGetAwaited()
        {
            var asyncMethodDone = false;

            TestEvent += async (sender, args) =>
            {
                await Task.Delay(50);
                asyncMethodDone = true;
            };

            await TestEvent.InvokeAsync(this);

            asyncMethodDone.Should().BeFalse();
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

        [TestMethod]
        public async Task Test_Event_NoSubscribers()
        {
            await TestEvent.InvokeAsync(this);
        }
    }
}
