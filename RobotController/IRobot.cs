using System;
using System.Collections.Generic;

namespace RobotController
{
    public interface IRobot
    {
        Grid Grid {get;}
        Cell At { get; }
        CardinalDirection Facing { get; }
        List<Obstacle> Obstacles { get; }
        bool Move(RelativeDirection towards);
    }
}
