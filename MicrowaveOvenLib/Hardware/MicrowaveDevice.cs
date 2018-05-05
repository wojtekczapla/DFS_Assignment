namespace MicrowaveOvenLib.Hardware
{
    using MicrowaveOvenLib.Hardware.Interfaces;
    using System;

    public class MicrowaveDevice
    {
        private readonly IMicrowaveOvenHW microwaveOvenHardwareUnit;
        private readonly IHeatingTimer heatingTimer;
        private bool heating = false;

        public bool Heating
        {
            get
            {
                return this.heating;
            }
            private set
            {
                this.heating = value;
            }
        }

        public MicrowaveDevice(IMicrowaveOvenHW microwaveOvenHW, IHeatingTimer heatingTimer)
        {
            this.microwaveOvenHardwareUnit = microwaveOvenHW;
            this.heatingTimer = heatingTimer;

            this.microwaveOvenHardwareUnit.DoorOpenChanged += MicrowaveOvenHardwareUnit_DoorOpenChanged;
            this.microwaveOvenHardwareUnit.StartButtonPressed += MicrowaveOvenHardwareUnit_StartButtonPressed;

            this.heatingTimer.TimeElapsed += HeatingTimer_TimeElapsed;
        }

        private void MicrowaveOvenHardwareUnit_StartButtonPressed(object sender, EventArgs e)
        {
            if (!this.microwaveOvenHardwareUnit.DoorOpen)
            {
                this.TurnOnHeater();
            }
        }

        private void MicrowaveOvenHardwareUnit_DoorOpenChanged(bool doorOpen)
        {
            if(doorOpen && this.Heating)
                this.microwaveOvenHardwareUnit.TurnOffHeater();
        }

        private void HeatingTimer_TimeElapsed(object sender, EventArgs e)
        {
            this.microwaveOvenHardwareUnit.TurnOffHeater();
        }

        private void TurnOnHeater()
        {
            if(this.Heating)
                this.heatingTimer.Extend();
            else
            {
                this.heatingTimer.Start();
                this.microwaveOvenHardwareUnit.TurnOnHeater();
                this.Heating = true;
            }
        }
    }
}