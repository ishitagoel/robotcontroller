using System;
using System.Collections.Generic;

namespace RobotController
{
    public interface IRobot
    {
        Grid Grid {get;}
        Cell CurrentCell { get; }
        CardinalDirection CurrentDirection { get; }
        List<Obstacle> Obstacles { get; }
        void Move(RelativeDirection movement);
    }
}
