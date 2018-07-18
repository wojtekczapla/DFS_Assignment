namespace MicrowaveOvenLib.Device
{
    using MicrowaveOvenLib.Device.Interfaces;
    using MicrowaveOvenLib.Hardware.Interfaces;

    public class Oven : IOven
    {
        IMicrowaveOvenHW microwaveOvenHardwareUnit;
        ILight ovenLight;

        public ILight Light
        {
            get
            {
                return ovenLight;
            }
        }

        public Oven(IMicrowaveOvenHW microwaveOvenHW, ILight light)
        {
            this.ovenLight = light;
            this.microwaveOvenHardwareUnit = microwaveOvenHW;
            this.microwaveOvenHardwareUnit.DoorOpenChanged += MicrowaveOvenHW_DoorOpenChanged;
        }

        private void MicrowaveOvenHW_DoorOpenChanged(bool doorOpen)
        {
            if (doorOpen)
                this.ovenLight.On();
            else
                this.ovenLight.Off();
        }
    }
}