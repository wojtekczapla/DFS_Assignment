namespace MicrowaveOvenLib.Tests.Unit
{
    using MicrowaveOvenLib.Hardware;
    using NUnit.Framework;
    using System;
    using System.Diagnostics;
    using System.Threading;

    [TestFixture]
    public class HeatingTimer_Test
    {
        [Test]
        public void Test_ThatMyEventIsRaised()
        {
            HeatingTimer heatingTimer = new HeatingTimer(TimeSpan.FromMilliseconds(1));
            bool eventRaised = false;
            
            heatingTimer.TimeElapsed += delegate (object sender, EventArgs e)
            {
                eventRaised = true;
            };

            heatingTimer.Start();

            Thread.Sleep(20);
            Thread.Yield();
            Assert.AreEqual(true, eventRaised);
        }

        [TestCase(50)]
        public void Test_ThatMyEventRaisedAfterExtendedTime(double interval)
        {
            HeatingTimer heatingTimer = new HeatingTimer(TimeSpan.FromMilliseconds(interval));
            bool eventRaised = false;

            Stopwatch stopwatch = new Stopwatch();
            DateTime test2 = new DateTime();

            heatingTimer.TimeElapsed += delegate (object sender, EventArgs e)
            {
                eventRaised = true;
                stopwatch.Stop();
                test2 = DateTime.Now;
            };

            stopwatch.Start();
            var test1 = DateTime.Now;
            heatingTimer.Start();

            Thread.Sleep((int)interval/2);
            heatingTimer.Extend();
            Thread.Sleep((int)interval * 2);
            Assert.AreEqual(true, eventRaised);

            TimeSpan ts = new TimeSpan((long)(test2.Ticks - test1.Ticks));
        }
    }
}