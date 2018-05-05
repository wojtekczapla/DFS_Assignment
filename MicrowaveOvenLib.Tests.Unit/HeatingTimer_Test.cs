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
        bool eventRaised;

        [SetUp]
        public void Setup()
        {
            eventRaised = false;
        }

        [Test]
        public void Test_ThatMyEventIsRaised()
        {
            // Arrange
            double milisecondsInterval = 1;
            HeatingTimer heatingTimer = new HeatingTimer(TimeSpan.FromMilliseconds(milisecondsInterval));
            
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