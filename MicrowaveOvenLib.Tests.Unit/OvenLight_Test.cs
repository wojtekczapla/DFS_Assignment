namespace MicrowaveOvenLib.Tests.Unit
{
    using MicrowaveOvenLib.Device;
    using MicrowaveOvenLib.Device.Interfaces;
    using MicrowaveOvenLib.Hardware.Interfaces;
    using Moq;
    using NUnit.Framework;

    [TestFixture]
    public class OvenLight_Test
    {
        Mock<IMicrowaveOvenHW> microwaveOvenHWMock;
        ILight light;

        [SetUp]
        public void Setup()
        {
            microwaveOvenHWMock = new Mock<IMicrowaveOvenHW>();
            light = new Light();
        }

        [Test]
        public void Test_ThatOvenLightOnAfterDoorOpen()
        {
            IOven oven = new Oven(microwaveOvenHWMock.Object, light);
            microwaveOvenHWMock.Raise(i => i.DoorOpenChanged += null, true);

            Assert.IsTrue(light.LightOn);
        }

        [Test]
        public void Test_ThatOvenLightOffAfterDoorClose()
        {
            IOven oven = new Oven(microwaveOvenHWMock.Object, light);
            microwaveOvenHWMock.Raise(i => i.DoorOpenChanged += null, false);

            Assert.IsFalse(light.LightOn);
        }
    }
}