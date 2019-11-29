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
    public class IT1_Door
    {
        private UserInterface _ut;
        private IButton _SCButton;
        private IButton _timeButton;
        private IButton _powerButton;
        private ICookController _ckController;
        private ILight _light;
        private IDisplay _display;
        private IDoor _door;
        private IPowerTube _pwrTube;

        [SetUp]
        public void SetUp()
        {
            //includes
            _SCButton = new Button();
            _timeButton = new Button();
            _powerButton = new Button();
            _door = new Door();
            _ut = new UserInterface(_powerButton, _timeButton, _SCButton, _door, _display, _light, _ckController);

            //fakes
            _ckController = Substitute.For<ICookController>();
            _light = Substitute.For<ILight>();
            _display = Substitute.For<IDisplay>();
        }

        [Test]
        public void OpenDoor_LightOn()
        {
            _door.Open();
            _light.Received(1).TurnOn();
        }
    }
}
