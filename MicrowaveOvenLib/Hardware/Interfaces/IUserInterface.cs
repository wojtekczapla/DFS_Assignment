namespace MicrowaveOvenLib.Hardware.Interfaces
{
    public interface IUserInterface
    {
        void DoorOpen();
        void DoorClose();
        void PressButtonStart();
    }
}