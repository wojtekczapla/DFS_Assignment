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
            // Arrange
            HeatingTimer heatingTimer = new HeatingTimer(TimeSpan.FromMilliseconds(1));
            bool eventRaised = false;
            
            heatingTimer.TimeElapsed += delegate (object sender, EventArgs e)
            {
                eventRaised = true;
            };

            // Act
            heatingTimer.Start();

            Thread.Sleep(20);
            Assert.AreEqual(true, eventRaised);
        }

        [Test]
        public void Test_ThatMyEventRaisedAfterExtendedTime()
        {
            // Arrange
            double milisecondsInterval = 1000;
            HeatingTimer heatingTimer = new HeatingTimer(TimeSpan.FromMilliseconds(milisecondsInterval));
            bool eventRaised = false;

            Stopwatch stopwatch = new Stopwatch();
            DateTime test2 = new DateTime();

            heatingTimer.TimeElapsed += delegate (object sender, EventArgs e)
            {
                eventRaised = true;
                stopwatch.Stop();
                test2 = DateTime.Now;
            };

            // Act
            stopwatch.Start();
            var test1 = DateTime.Now;
            heatingTimer.Start();

            Thread.Sleep((int)milisecondsInterval / 2);
            heatingTimer.Extend();
            heatingTimer.Extend();
            Thread.Sleep((int)milisecondsInterval * 3);

            // Assert
            Assert.AreEqual(true, eventRaised);
        }

        [Test]
        public void Test_ThatMyEventRaisedAfterExtendedTimeInTimeGreaterThanExtension()
        {
            // Arrange
            double milisecondsInterval = 1000;
            HeatingTimer heatingTimer = new HeatingTimer(TimeSpan.FromMilliseconds(milisecondsInterval));
            bool eventRaised = false;

            Stopwatch stopwatch = new Stopwatch();

            heatingTimer.TimeElapsed += delegate (object sender, EventArgs e)
            {
                eventRaised = true;
                stopwatch.Stop();
            };

            // Act
            stopwatch.Start();
            heatingTimer.Start();

            Thread.Sleep((int)milisecondsInterval / 2);
            heatingTimer.Extend();
            Thread.Sleep((int)milisecondsInterval / 2);
            heatingTimer.Extend();
            Thread.Sleep((int)milisecondsInterval * 3);

            // Assert
            Assert.GreaterOrEqual(stopwatch.Elapsed.TotalMilliseconds, 3000);
        }
    }
}