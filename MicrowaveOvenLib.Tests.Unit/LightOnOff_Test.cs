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
        public void IsLightOnAfterOnFunction()
        {
            // Arrange

            // Act
            light.On();

            // Assert
            Assert.IsTrue(light.LightOn);
        }

        [Test]
        public void IsLightOffAfterOffFunction()
        {
            // Arrange

            // Act
            light.Off();

            // Assert
            Assert.IsFalse(light.LightOn);
        }
    }
}
