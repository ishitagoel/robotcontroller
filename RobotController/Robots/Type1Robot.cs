using System;
using System.Collections.Generic;
using System.Text;

namespace RobotController.Robots
{
    public class Type1Robot : IRobot
    {
        public Type1Robot(Grid grid, Cell startAt, CardinalDirection facing, List<Obstacle> obstacles)
        {
            Grid = grid;
            At = startAt;
            Facing = facing;
            Obstacles = obstacles;
        }
        public Grid Grid { get; }

        public Cell At { get; }

        public CardinalDirection Facing { get; }

        public List<Obstacle> Obstacles {get;}

        public bool Move(RelativeDirection towards)
        {
            throw new NotImplementedException();
        }
    }
}
