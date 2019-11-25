using System;

namespace RobotController.Obstacles
{
    public class Spinner : Obstacle
    {
        public int Rotation { get; }

        public Spinner(int rotation)
        {
            if(rotation%90!=0)
            {
                throw new ArgumentException("Rotation must be in increments of 90.");
            }
            Rotation = rotation;
        }

        public CardinalDirection Spin(CardinalDirection facing)
        {           
            //To understand the code below, let's use the situation that facing=West and Rotation=270.
            var degreesFacing = (facing - CardinalDirection.North) * 90; //270
            var degreesAfterSpin = degreesFacing + Rotation; //540
            return (CardinalDirection)((degreesAfterSpin / 90) % 3);            
           
        }
    }
}
