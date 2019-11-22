﻿using System;
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

        public CardinalDirection Facing { get; }

        public Dictionary<Cell, Obstacle> Obstacles {get;}

        public void Move(RelativeDirection towards)
        {
            var moveTo = Grid.AdjacentTo(At, Facing, towards);

            if (moveTo == null)
            {
                throw new InvalidOperationException("There is no cell to move to.");
            }

            if (Obstacles.ContainsKey((Cell)moveTo))
            {
                var obstacle = Obstacles[(Cell)moveTo];
                if (obstacle is Rock)
                {
                    //do nothing
                }
            }
            else
            {
                At = (Cell)moveTo;
            }
        }
    }
}
