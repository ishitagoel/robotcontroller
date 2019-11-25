using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotController.Obstacles;

namespace RobotController.UnitTests
{
    [TestClass]
    public class HoleTests
    {
        [TestMethod]
        public void Constructor_PropertiesInitialized()
        {
            Cell connectedTo = new Cell(4, 5);
            var hole = new Hole(connectedTo);
            Assert.IsTrue(hole.ConnectedTo.Equals(connectedTo)); 
        }
    }
}
