using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotController.UnitTests
{
    [TestClass]
    public class CellTests
    {       
        [TestMethod]
        public void Constructor_RowColumnGiven_PropertiesHaveSameValue()
        {
            int row = 1, column = 2;
            var cell = new Cell(row, column);
            Assert.IsTrue(cell.Row == row);
            Assert.IsTrue(cell.Column == column);
        }
    }
}
