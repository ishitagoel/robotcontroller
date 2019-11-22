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

        [TestMethod]
        public void Equals_RowIsDifferent_ReturnsFalse()
        {
            int row = 1, column = 2;
            var cell1 = new Cell(row, column);
            var cell2 = new Cell(row + 1, column);
            Assert.IsFalse(cell1.Equals(cell2));
        }

        [TestMethod]
        public void Equals_ColumnIsDifferent_ReturnsFalse()
        {
            int row = 1, column = 2;
            var cell1 = new Cell(row, column);
            var cell2 = new Cell(row, column+1);
            Assert.IsFalse(cell1.Equals(cell2));
        }

        [TestMethod]
        public void Equals_RowColumnIsSame_ReturnsTrue()
        {
            int row = 1, column = 2;
            var cell1 = new Cell(row, column);
            var cell2 = new Cell(row, column);
            Assert.IsTrue(cell1.Equals(cell2));
        }
    }
}
