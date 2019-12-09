using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicrowaveOvenClasses.Boundary;
using MicrowaveOvenClasses.Controllers;
using MicrowaveOvenClasses.Interfaces;
using NSubstitute;
using NUnit.Framework;

namespace Microwave.Test.Integration
{
    [TestFixture]
    class IT2_Button
    {
        private UserInterface _ut;

        private IButton _SCButton;
        private IButton _timeButton;
        private IButton _powerButton;
        private ICookController _ckController;
        private ILight _light;
        private IDisplay _display;
        private IDoor _door;
        private ITimer _timer;
        

        [SetUp]
        public void SetUp()
        {
            //includes
            _SCButton = new Button();
            _timeButton = new Button();
            _powerButton = new Button();
            _door = new Door();

            //fakes
            _ckController = Substitute.For<ICookController>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();

            _ut = new UserInterface(_powerButton, _timeButton, _SCButton, _door, _display, _light, _ckController);

        }

        [Test]
        public void PowerPresed_ShowPower()
        {
            _powerButton.Press();

            _display.Received(1).ShowPower(50);
        }

        [Test]
        public void StartCancelButtonPressed()
        {
            _powerButton.Press();
            _SCButton.Press();

            //Assert
            _light.Received(1).TurnOff();
            _display.Received(1).Clear();
        }

        [Test]
        public void TimeButton_Pressed()
        {
            _powerButton.Press();
            _timeButton.Press();
            _display.Received(1).ShowTime(1,0);
        }

        [Test]
        public void TimeButton_Pressed_two_times()
        {
            _powerButton.Press();
            _timeButton.Press();
            _timeButton.Press();
            _display.Received(1).ShowTime(2, 0);
        }

        [Test]
        public void StartButton_Pressed()
        {
            _powerButton.Press();
            _timeButton.Press();
            _SCButton.Press();
            
            _ckController.Received().StartCooking(50,60);
        }

        [Test]
        public void StartCancelButton_Pressed_Two_Times()
        {
            _powerButton.Press();
            _timeButton.Press();
            _SCButton.Press();
            _SCButton.Press();

            _ckController.Received(1).Stop();
        }
    }
}
