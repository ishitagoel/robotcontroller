using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RobotController.UnitTests
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
        
        [TestMethod]
        public void Has_CellExistsOnGrid_ReturnsTrue()
        {
            var grid = new Grid(2, 3);
            Assert.IsTrue(grid.Has(new Cell(1, 3)));
            Assert.IsTrue(grid.Has(new Cell(2, 1)));            
        }

        [TestMethod]
        public void Has_CellDoesNotExistOnGrid_ReturnsFalse()
        {
            var grid = new Grid(2, 3);
            Assert.IsFalse(grid.Has(new Cell(3, 2)));            
        }

        [TestMethod]
        public void MoveTo_AtCenterOfGridFacingNorthTowardsLeft_ReturnsSameRowOneColumnLess()
        {
            var grid = new Grid(3, 3);
            var at = new Cell(2, 2); //center of grid
            var facing = CardinalDirection.North;
            var towards = RelativeDirection.Left;
            var left = grid.AdjacentTo(at, facing, towards);
            Assert.IsNotNull(left);
            Assert.AreEqual( ((Cell)left).Row, at.Row);
            Assert.AreEqual( ((Cell)left).Column, at.Column-1);
        }

        [TestMethod]
        public void MoveTo_AtCenterOfGridFacingSouthTowardsLeft_ReturnsSameRowOneColumnMore()
        {
            var grid = new Grid(3, 3);
            var at = new Cell(2, 2); //center of grid
            var facing = CardinalDirection.South;
            var towards = RelativeDirection.Left;
            var left = grid.AdjacentTo(at, facing, towards);
            Assert.IsNotNull(left);
            Assert.AreEqual(((Cell)left).Row, at.Row);
            Assert.AreEqual(((Cell)left).Column, at.Column + 1);
        }

        [TestMethod]
        public void MoveTo_AtCenterOfGridFacingEastTowardsLeft_ReturnsOneRowLessSameColumn()
        {
            var grid = new Grid(3, 3);
            var at = new Cell(2, 2); //center of grid
            var facing = CardinalDirection.East;
            var towards = RelativeDirection.Left;
            var left = grid.AdjacentTo(at, facing, towards);
            Assert.IsNotNull(left);
            Assert.AreEqual(((Cell)left).Row, at.Row-1);
            Assert.AreEqual(((Cell)left).Column, at.Column);            
        }

        [TestMethod]
        public void MoveTo_AtCenterOfGridFacingWestTowardsLeft_ReturnsOneRowMoreSameColumn()
        {
            var grid = new Grid(3, 3);
            var at = new Cell(2, 2); //center of grid
            var facing = CardinalDirection.West;
            var towards = RelativeDirection.Left;
            var left = grid.AdjacentTo(at, facing, towards);
            Assert.IsNotNull(left);
            Assert.AreEqual(((Cell)left).Row, at.Row + 1);
            Assert.AreEqual(((Cell)left).Column, at.Column);                  
        }

        [TestMethod]
        public void MoveTo_AtExtremeLeftOfGridFacingNorthTowardsLeft_ReturnsNull()
        {
            var grid = new Grid(3, 3);
            var at = new Cell(2, 1); //extreme left of grid
            var facing = CardinalDirection.North;
            var towards = RelativeDirection.Left;
            var left = grid.AdjacentTo(at, facing, towards);
            Assert.IsNull(left);
        }

        [TestMethod]
        public void OnLeft_AtExtremeBottomOfGridFacingWest_ReturnsNull()
        {
            var grid = new Grid(3, 3);
            var at = new Cell(3, 2); //extreme bottom of grid
            var facing = CardinalDirection.West;
            var towards = RelativeDirection.Left;
            var left = grid.AdjacentTo(at, facing, towards);
            Assert.IsNull(left);
        }

        [TestMethod]
        public void Equals_RowsIsDifferent_ReturnsFalse()
        {
            int rows = 1, columns = 2;
            var grid1 = new Grid(rows, columns);
            var grid2 = new Grid(rows + 1, columns);
            Assert.IsFalse(grid1.Equals(grid2));
        }

        [TestMethod]
        public void Equals_ColumnsIsDifferent_ReturnsFalse()
        {
            int rows = 1, columns = 2;
            var grid1 = new Grid(rows, columns);
            var grid2 = new Grid(rows, columns + 1);
            Assert.IsFalse(grid1.Equals(grid2));
        }

        [TestMethod]
        public void Equals_RowsColumnsIsSame_ReturnsTrue()
        {
            int rows = 1, columns = 2;
            var grid1 = new Grid(rows, columns);
            var grid2 = new Grid(rows, columns);
            Assert.IsTrue(grid1.Equals(grid2));
        }
    }
}
