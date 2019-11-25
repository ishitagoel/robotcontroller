using System;

namespace RobotController.Obstacles
{
    public class Hole : Obstacle
    {
        public Cell ConnectedTo { get; }
        
        public Hole(Cell connectedTo)
        {            
            ConnectedTo = connectedTo;
        } 
    }
}
