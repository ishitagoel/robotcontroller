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
            var startAt = new Cell(2, 2); //at center
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();

            //add a rock to the left
            var cellWithObstacle = new Cell(2, 1);
            obstacles.Add(cellWithObstacle, new Rock()); 
            
            var robot = new Type1Robot(grid, startAt, facing, obstacles);
                  
            robot.Move(RelativeDirection.Left);

            Assert.IsTrue(robot.At.Equals(startAt));
        } 
            
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Constructor_HoleWithConnectedCellIsOutsideGrid_ThrowsArgumentOutOfRangeException()
        {
            Grid grid = new Grid(3, 3);
            var startAt = new Cell(2, 2); //at center
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();

            //add a hole to the left connected to a cell outside the grid
            var cellWithObstacle = new Cell(2, 1);
            var cellOutsideGrid = new Cell(4, 1);
            obstacles.Add(cellWithObstacle, new Hole(cellOutsideGrid)); 

            new Type1Robot(grid, startAt, facing, obstacles);                      
        }               

        [TestMethod]
        public void Move_TowardsAHole_MovesToConnectedCellFacingSame()
        {
            Grid grid = new Grid(3, 3);
            var startAt = new Cell(2, 2); //at center
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();

            //add a hole to the left connected to a cell within the grid
            var cellWithObstacle = new Cell(2, 1);
            var connectedCell = new Cell(3, 3);
            obstacles.Add(cellWithObstacle, new Hole(connectedCell));

            var robot = new Type1Robot(grid, startAt, facing, obstacles);

            //robot encounters the hole and moves to the connected cell
            //robot direction remains the same
            robot.Move(RelativeDirection.Left);
            Assert.IsTrue(robot.At.Equals(connectedCell));
            Assert.IsTrue(robot.Facing.Equals(facing));
        }

        [TestMethod]
        public void Move_TowardsASpinnerWithRotation90FacingNorth_FacesEastSameLocation()
        {
            Grid grid = new Grid(3, 3);
            var startAt = new Cell(2, 2); //at center
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();

            //add a hole to the left with 90 degrees rotation  
            var cellWithObstacle = new Cell(2, 1);
            obstacles.Add(cellWithObstacle, new Spinner(90));

            var robot = new Type1Robot(grid, startAt, facing, obstacles);

            //robot encounters the spinner and spins to face a new direction
            //robot location remains the same
            robot.Move(RelativeDirection.Left);
            Assert.IsTrue(robot.Facing.Equals(CardinalDirection.East));
            Assert.IsTrue(robot.At.Equals(cellWithObstacle));
        }

        [TestMethod]
        public void Move_TowardsAHoleLeadsToSpinnerThenTowardsRock_BehavesAsExpectedForAllObstacles()
        {
            Grid grid = new Grid(3, 3);
            var startAt = new Cell(2, 2); //at center
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();

            //add a hole to the left connected to a cell within the grid
            var cellWithHole = new Cell(2, 1);
            var cellAfterHole = new Cell(3, 3);
            obstacles.Add(cellWithHole, new Hole(cellAfterHole));

            //add a spinner with 180 degrees rotation, so that robot will face south             
            obstacles.Add(cellAfterHole, new Spinner(180));

            //add a rock to the right
            var cellWithRock = new Cell(3, 2);
            obstacles.Add(cellWithRock, new Rock());

            var robot = new Type1Robot(grid, startAt, facing, obstacles);

            //robot moved left and encounters the hole, falls to a new cell where it encounters the spinner.
            //then robot tries to move right but encounters a rock
            //Hence, robot now faces south, and remains at the cell after hole
            robot.Move(RelativeDirection.Left);
            robot.Move(RelativeDirection.Right);
            Assert.IsTrue(robot.Facing.Equals(CardinalDirection.South));
            Assert.IsTrue(robot.At.Equals(cellAfterHole));
        }
    }
}
