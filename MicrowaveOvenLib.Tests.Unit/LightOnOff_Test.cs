namespace MicrowaveOvenLib.Tests.Unit
{
    using MicrowaveOvenLib.Device;
    using MicrowaveOvenLib.Device.Interfaces;
    using NUnit.Framework;

    [TestFixture]
    public class LightOnOff_Test
    {
        ILight light;

        [SetUp]
        public void Setup()
        {
            light = new Light();
        }

        [Test]
        public void IsLightOnAfterOn()
        {
            light.On();

            Assert.IsTrue(light.LightOn);
        }

        [Test]
        public void IsLightOffAfterOff()
        {
            light.Off();

            Assert.IsFalse(light.LightOn);
        }
    }
}
