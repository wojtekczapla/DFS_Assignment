namespace MicrowaveOvenLib.Device.Interfaces
{
    public interface ILight
    {
        bool LightOn { get; }
        void On();
        void Off();
    }
}