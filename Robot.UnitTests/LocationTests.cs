using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Robot.UnitTests
{
    [TestClass]
    public class LocationTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_RowIsZero_ThrowsArgumentOutOfRangeException()
        {
            var location = new Location(0, 2);                        
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ColumnIsNegative_ThrowsArgumentOutOfRangeException()
        {
            var location = new Location(1, -1);
        }

        [TestMethod]
        public void Constructor_RowColumnPositive_ObjectIsCreated()
        {
            var location = new Location(1, 2);
            Assert.IsNotNull(location);
        }

        [TestMethod]
        public void ToString_LocationExists_ReturnsFormattedString()
        {
            var location = new Location(1, 2);
            var result = location.ToString();
            Assert.AreEqual(result, "[1,2]");
        }


    }
}
