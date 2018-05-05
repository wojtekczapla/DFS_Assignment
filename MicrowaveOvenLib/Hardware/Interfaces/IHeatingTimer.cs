namespace MicrowaveOvenLib.Hardware.Interfaces
{
    using System;

    public interface IHeatingTimer
    {
        event EventHandler TimeElapsed;
        void Start();
        void Extend();
    }
}
