namespace MicrowaveOvenLib.Device
{
    using MicrowaveOvenLib.Device.Interfaces;

    public class Light : ILight
    {
        private bool lightOn = false;

        public bool LightOn => this.lightOn;

        public void Off() => this.lightOn = false;

        public void On() => this.lightOn = true;
    }
}