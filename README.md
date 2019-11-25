# Robot Controller

The Robot Controller navigates 2-dimensional grids under the user''s commands, and responds to obstacles in its way. 

## Getting Started

1. Open the solution file RobotController.sln in Visual Studio.
2. Go to "Build" -> "Build Solution" to build all three projects in this solution, the main class library, the unit tests and a console app.
3. Go to "Tests" -> "Run All Tests" to verify that all unit tests are successful.
4. Press F5 to run a simple console app that configures the grid and obstacles, takes a series of commands L/R/F/B from the user and shows the route that the robot takes.

## Design

[UML Class Diagram](./RobotController/ClassDiagram.cd)

### Cell
The Cell struct represents a cell in a 2-dimensional grid. 

### CardinalDirection
The CardinalDirection enum represents North, East, South and West.

### RelativeDirection
The RelativeDirection enum represents Left, Right, Front and Back.

### Grid
The Grid class repesents a two dimensional grid. It can verify whether a cell is within its boundaries or not. It can also find the adjacent cells of any given cell.

### Obstacle
The Obstacle class is an abstract class and represents an obstacle in a cell.

### Rock
The Rock class is a type of obstacle, it has no properties. 

### Hole
The Hole class is a type of obstacle, and has a cell to which it is connected.

### Spinner
The Spinner class is a type of Obstacle, and has a rotation property and a Spin method to spin by a certain degree.

### IRobot
The IRobot interface represents the contract a robot must adhere to. It has following members:
* Grid: The grid on which the robot must navigate
* At: The current location of the robot within the grid
* Facing: The current direction the robot is facing (N/E/S/W)
* Obstacles: The obstacles the robot must overcome while navigating
* Move: A method to give the robot a command to move left, right, front or back

### Type1Robot
The Type1Robot class is an implementation of the IRobot interface. What is unique about it is its response to obstacles, which is as follows:
* Hole: It falls into the hole and moves to the cell connected to the hole.
* Spinner: It changes direction on encountering a spinner.
* Rock or any other type of obstacle: It stays put and doesn't move into the cell with this obstacle.
