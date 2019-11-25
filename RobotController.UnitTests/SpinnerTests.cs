using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotController.Obstacles;

namespace RobotController.UnitTests
{
    [TestClass]
    public class SpinnerTests
    {        
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Constructor_RotationNotInIncrementOf90Degrees_ThrowsArgumentException()
        {
            new Spinner(30);
        }

        [TestMethod]
        public void Constructor_RotationInIncrementOf90Degrees_PropertiesInitialized()
        {
            int rotation = 180;
            Spinner spinner = new Spinner(rotation);
            Assert.IsTrue(spinner.Rotation==rotation);
        }

        [TestMethod]
        public void Spin_FacingNorthRotation180_ReturnsSouth()
        {
            int rotation = 180;
            var facing = CardinalDirection.North;
            Spinner spinner = new Spinner(rotation);
            var newFacing = spinner.Spin(facing);
            Assert.IsTrue(newFacing == CardinalDirection.South);

        }

        [TestMethod]
        public void Spin_FacingEastRotation450_ReturnsNorth()
        {
            int rotation = 450;
            var facing = CardinalDirection.East;
            Spinner spinner = new Spinner(rotation);
            var newFacing = spinner.Spin(facing);
            Assert.IsTrue(newFacing == CardinalDirection.North);

        }
    }
}
