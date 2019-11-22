using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RobotController.Robots;
using RobotController.Obstacles;

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
            var obstacles = new Dictionary<Cell, Obstacle>();
            new Type1Robot(grid, startAt, facing, obstacles);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_StartAtIsNotWithinGrid_ThrowsArgumentOutOfRangeException()
        {            
            Grid grid = new Grid(2, 2);
            var startAt = new Cell(3, 3);
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();
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

        [TestMethod]
        public void Constructor_ValidParameters_AllPropertiesAreInitialized()
        {
            Grid grid = new Grid(3, 3);
            var startAt = new Cell(2, 2);
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();
            
            var robot = new Type1Robot(grid, startAt, facing, obstacles);

            Assert.IsTrue(robot.Grid.Equals(grid));
            Assert.IsTrue(robot.At.Equals(startAt));
            Assert.IsTrue(robot.Facing.Equals(facing));
            Assert.AreEqual(robot.Obstacles.Count, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Move_TowardsLeftFromExtremeLeft_ThrowsInvalidOperationException()
        {
            Grid grid = new Grid(3, 3);
            var startAt = new Cell(2, 1); //in leftmost column
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>() { };

            var robot = new Type1Robot(grid, startAt, facing, obstacles);
            
            //attempt moving to the left
            robot.Move(RelativeDirection.Left);
        }

        [TestMethod]
        public void Move_TowardsARock_RemainsAtSameLocation()
        {
            Grid grid = new Grid(3, 3);
            var startAt = new Cell(2, 2); //in leftmost column
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();
            obstacles.Add(new Cell(2, 1), new Rock()); //add a rock to the left
            
            var robot = new Type1Robot(grid, startAt, facing, obstacles);
                  
            robot.Move(RelativeDirection.Left);

            Assert.IsTrue(robot.At.Equals(startAt));
        }        
    }
}
