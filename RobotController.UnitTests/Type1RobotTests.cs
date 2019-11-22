using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotController.Robots;

namespace RobotController.UnitTests
{
    [TestClass]
    public class Type1RobotTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_GridIsNull_ThrowsArgumentNullException()
        {
            Grid grid = null;
            var startAt = new Cell(3, 3);
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>() { };
            new Type1Robot(grid, startAt, facing, obstacles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_StartAtIsNotWithinGrid_ThrowsArgumentOutOfRangeException()
        {            
            Grid grid = new Grid(2, 2);
            var startAt = new Cell(3, 3);
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>() { };
            new Type1Robot(grid, startAt, facing, obstacles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Constructor_ObstaclesIsNull_ThrowsArgumentNullException()
        {
            Grid grid = new Grid(3, 3);
            var startAt = new Cell(2, 2);
            var facing = CardinalDirection.North;
            Dictionary<Cell, Obstacle> obstacles = null;
            new Type1Robot(grid, startAt, facing, obstacles);
        }
    }
}
