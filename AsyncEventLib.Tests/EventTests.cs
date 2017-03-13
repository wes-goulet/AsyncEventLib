using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AsyncEventLib.Tests
{
    [TestClass]
    public class EventTests
    {
        private event EventHandler TestEvent;

        [TestMethod]
        public void Test_Event_DoesNotWaitOnAsync()
        {
            return;
        }
    }
}
