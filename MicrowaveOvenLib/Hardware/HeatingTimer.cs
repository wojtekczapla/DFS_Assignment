namespace MicrowaveOvenLib.Hardware
{
    using MicrowaveOvenLib.Hardware.Interfaces;
    using System;
    using System.Diagnostics;
    using System.Threading;

    public class HeatingTimer : IHeatingTimer
    {
        private readonly TimeSpan intervalTime;
        private TimeSpan timeSpanExtension;
        private Timer localTimer;
        Stopwatch sw = new Stopwatch();

        public event EventHandler TimeElapsed;

        public HeatingTimer(TimeSpan interval)
        {
            this.intervalTime = interval;
            TimerCallback tc = new TimerCallback(TimerElapsed);
            this.localTimer = new Timer(tc, null, Timeout.Infinite, Timeout.Infinite);
        }

        public void Start()
        {
            this.timeSpanExtension = intervalTime;
            this.localTimer.Change(intervalTime, intervalTime);
            sw.Start();
        }

        public void Extend()
        {
            sw.Stop();
            this.timeSpanExtension += (intervalTime- TimeSpan.FromMilliseconds(sw.Elapsed.TotalMilliseconds));
            this.localTimer.Change(timeSpanExtension, timeSpanExtension);
            sw.Restart();
        }

        private void Reset()
        {
            this.localTimer.Change(Timeout.Infinite, Timeout.Infinite);
        }

        protected virtual void OnTimeElapsed()
        {
            this.TimeElapsed?.Invoke(this, null);
        }

        private void TimerElapsed(object state)
        {
            this.Reset(); 
            this.OnTimeElapsed();
        }
    }
}