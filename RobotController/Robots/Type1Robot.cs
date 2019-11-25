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
            //Hole obstacles must have connected cells within the grid
            foreach(var cellWithObstacle in obstacles.Keys)
            {
                if(obstacles[cellWithObstacle] is Hole)
                {
                    var hole = (Hole)obstacles[cellWithObstacle];
                    
                    if(!grid.Has(hole.ConnectedTo))
                    {
                        throw new ArgumentOutOfRangeException("obstacles", String.Format(
                                @"Hole at cell [{0},{1}] is connected to cell [{2},{3}], which is not within the grid boundaries.",
                                cellWithObstacle.Row,cellWithObstacle.Column,hole.ConnectedTo.Row,hole.ConnectedTo.Column));
                    }
                    
                }
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
         * Move towards a new direction. 
         * Throw exception if there is no valid cell to move into this direction. 
        */
        public void Move(RelativeDirection towards)
        {
            //Get the new cell to move into
            var to = Grid.AdjacentTo(At, Facing, towards);

            //if there is no cell available in the given direction, throw an exception
            if (to == null)
            {
                throw new InvalidOperationException(
                    String.Format("There is no cell to move in the direction of {0}.", towards));
            }
            Move((Cell)to);
        }

        /*
         * Move to a new cell. If the new cell has an obstacle, respond to obstacles as follows:
         *    Rock or unknown obstacle: Don't move
         *    Hole: Move into the cell connected to the hole.
         *    Spinner: Change direction as per the rotation of the spinner.
         */

        private void Move(Cell to)
        {
            //Check if there is an obstacle at the new cell
            if (!Obstacles.ContainsKey((Cell)to))
            {                
                At = (Cell)to;
                return;
            }
            
            var obstacle = Obstacles[(Cell)to];
            //If the obstacle is a Hole, go to the cell connected to the hole.
            if (obstacle is Hole)
            {
                Move(((Hole)obstacle).ConnectedTo);
            }
            //If obstacle is a Spinner, face the new direction after spin.
            else if (obstacle is Spinner)
            {
                Facing = ((Spinner)obstacle).Spin(Facing);
                At = (Cell)to;
            }
            //if obstacle is a Rock or an unknown type, don't move into the new cell            
            
        }
    }
}
