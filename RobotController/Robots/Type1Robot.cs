using System;
using System.Collections.Generic;
using RobotController.Obstacles;

namespace RobotController.Robots
{
    public class Type1Robot : IRobot
    {
        public Type1Robot(Grid grid, Cell startAt, CardinalDirection facing, Dictionary<Cell, Obstacle> obstacles)
        {
            //No null parameters
            if(grid==null || obstacles==null)
            {
                throw new ArgumentNullException("grid");
            }
            //Starting cell must be within the grid
            if(!grid.Has(startAt))
            {
                throw new ArgumentOutOfRangeException("startAt", "Start location must be a cell within the grid.");
            }           

            Grid = grid;
            At = startAt;
            Facing = facing;
            Obstacles = obstacles;
        }
        public Grid Grid { get; }

        public Cell At { get; private set;  }

        public CardinalDirection Facing { get; private set; }

        public Dictionary<Cell, Obstacle> Obstacles {get;}

        /*
         * Move into a new cell as per the command. 
         * If the new cell has an obstacle, respond to obstacles as follows:
         *    Rock or unknown obstacle: Don't move
         *    Hole: Move into the cell connected to the hole, as long as the connected cell is within the grid.
         *    Spinner: Change direction as per the rotation of the spinner.
         */
        public void Move(RelativeDirection towards)
        {
            //Get the new cell to move into
            var moveTo = Grid.AdjacentTo(At, Facing, towards);

            //if there is no cell available in the given direction, throw an exception
            if (moveTo == null)
            {
                throw new InvalidOperationException("There is no cell to move to.");
            }

            //Check if there is an obstacle at the new cell
            if (Obstacles.ContainsKey((Cell)moveTo))
            {
                var obstacle = Obstacles[(Cell)moveTo];
                //If the obstacle is a Hole, go to the cell connected to the hole.
                if (obstacle is Hole)
                {
                    moveTo = ((Hole)obstacle).ConnectedTo;
                    //Verify that the connected cell is within the grid.
                    if (!Grid.Has((Cell)moveTo))
                    {
                        throw new InvalidOperationException(
                            "Moving into a hole that is not connected to a cell within the grid.");
                    }                    
                }      
                //If obstacle is a Spinner, face the new direction after spin.
                else if(obstacle is Spinner)
                {
                    Facing = ((Spinner)obstacle).Spin(Facing);
                }
                else
                {
                    //if obstacle is a Rock or an unknown type, don't move into the new cell
                    return;
                }
            }
            //set the new cell as the current position
            At = (Cell)moveTo;            
        }
    }
}
