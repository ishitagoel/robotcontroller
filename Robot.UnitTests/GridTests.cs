using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Robot.UnitTests
{
    [TestClass]
    public class GridTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_RowsIsZero_ThrowsArgumentOutOfRangeException()
        {
            new Grid(0, 2);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_ColumnsIsNegative_ThrowsArgumentOutOfRangeException()
        {
            new Grid(1, -1);
        }

        [TestMethod]
        public void Constructor_RowsColumnsPositive_ObjectIsCreated()
        {
            int rows = 1, columns = 2;
            var grid = new Grid(rows, columns);
            Assert.IsNotNull(grid);
            Assert.IsTrue(grid.Rows == rows);
            Assert.IsTrue(grid.Columns == columns);            
        }
        
    }
}
