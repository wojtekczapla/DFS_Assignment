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
        public void Should_Call_MicrowaveOvenHW_TurnOnHeater_When_DoorIsClosed_And_HeatingIsTrue_And_StartButtonPressed()
        {
            // Arrange
            microwaveOvenHW.DoorOpen.Returns(false);

            // Act
            microwaveOvenHW.StartButtonPressed += Raise.Event<EventHandler>(this, null);

            //Heating is true
            microwaveOvenHW.StartButtonPressed += Raise.Event<EventHandler>(this, null);

            // Assert
            microwaveOvenHW.Received(1).TurnOnHeater();
        }

        [Test]
        public void Should_Call_heatingTimer_Extend_When_DoorIsClosed_And_HeatingIsTrue_And_StartButtonPressed()
        {
            // Arrange
            microwaveOvenHW.DoorOpen.Returns(false);

            // Act

            //Heating is true
            microwaveOvenHW.StartButtonPressed += Raise.Event<EventHandler>(this, null);

            microwaveOvenHW.StartButtonPressed += Raise.Event<EventHandler>(this, null);

            // Assert
            heatingTimer.Received(1).Extend();
        }

        [Test]
        public void Should_Call_MicrowaveOvenHW_TurnOffHeater_When__HeatingIsTrue_And_DoorAreOpened()
        {
            // Arrange
            microwaveOvenHW.DoorOpen.Returns(false);

            // Act
            //Heating is true
            microwaveOvenHW.StartButtonPressed += Raise.Event<EventHandler>(this, null);
            microwaveOvenHW.DoorOpenChanged += Raise.Event<Action<bool>>(true);

            // Assert
            microwaveOvenHW.Received(1).TurnOffHeater();
        }
    }
}