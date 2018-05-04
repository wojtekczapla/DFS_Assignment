namespace MicrowaveOvenLib.Tests.Unit
{
    using MicrowaveOvenLib.Hardware;
    using MicrowaveOvenLib.Hardware.Interfaces;
    using NSubstitute;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class MicrowaveDeviceTest
    {
        private IMicrowaveOvenHW microwaveOvenHW;
        private IHeatingTimer heatingTimer;
        private MicrowaveDevice microwaveDevice;

        [SetUp]
        public void Setup()
        {
            microwaveOvenHW = Substitute.For<IMicrowaveOvenHW>();
            heatingTimer = Substitute.For<IHeatingTimer>();
            microwaveDevice = new MicrowaveDevice(microwaveOvenHW, heatingTimer);
        }

        [Test]
        public void Should_Call_MicrowaveOvenHW_TurnOnHeater_When_DoorIsClose_And_HeatingIsFalse_And_StartButtonPressed()
        {
            // Arrange
            microwaveOvenHW.DoorOpen.Returns(false);

            // Act
            microwaveOvenHW.StartButtonPressed += Raise.Event<EventHandler>(this, null);

            // Assert
            microwaveOvenHW.Received(1).TurnOnHeater();
        }

        [Test]
        public void Should_Call_MicrowaveOvenHW_TurnOnHeater_When_DoorIsOpen_And_HeatingIsFalse_And_StartButtonPressed()
        {
            // Arrange
            microwaveOvenHW.DoorOpen.Returns(true);

            // Act
            microwaveOvenHW.StartButtonPressed += Raise.Event<EventHandler>(this, null);

            // Assert
            microwaveOvenHW.Received(0).TurnOnHeater();
        }

        [Test]
        public void Should_Call_MicrowaveOvenHW_TurnOffHeater_When_DoorIsOpen_And_HeatingIsTrue()
        {
            // Arrange
            microwaveOvenHW.DoorOpen.Returns(false);
            microwaveDevice.Heating.Returns(true);

            // Act
            microwaveOvenHW.StartButtonPressed += Raise.Event<EventHandler>(this, null);

            // Assert
            microwaveOvenHW.Received(1).TurnOffHeater();
        }
    }
}

