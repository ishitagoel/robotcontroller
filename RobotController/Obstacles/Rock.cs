using System;

namespace RobotController.Obstacles
{
    public class Rock : Obstacle
    {        
        public override Cell At { get; }

        public Rock(Cell at)
        {
            At = at;
        }        
    }
}
