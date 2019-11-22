using System;
using System.Collections.Generic;

namespace RobotController
{
    public interface IRobot
    {
        Grid Grid {get;}
        Cell At { get; }
        CardinalDirection Facing { get; }
        Dictionary<Cell, Obstacle> Obstacles { get; }
        void Move(RelativeDirection towards);
    }
}
