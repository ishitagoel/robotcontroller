using RobotController.Obstacles;
using RobotController.Robots;
using System;
using System.Collections.Generic;

namespace RobotController.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //welcome user
            Highlight(@"
--------------------------------------------------------------------
Hi! I am Gridomatic the Robot. I can move on any 2-dimensional grid. 
You can control my movement.
--------------------------------------------------------------------
");

            Console.WriteLine(@"I've been configured as follows...

Grid size: 3*3
Start location: [2,2]
Start direction: North

Obstacles:
    - Hole at location [2,1] connected to [3,3]
    - Spinner at location [3,3]
    - Rock at location [3,2]
");

            var grid = new Grid(3, 3);
            var startAt = new Cell(2, 2);
            var facing = CardinalDirection.North;
            var obstacles = new Dictionary<Cell, Obstacle>();            
            obstacles.Add(new Cell(2, 1), new Hole(new Cell(3, 3)));           
            obstacles.Add(new Cell(3, 3), new Spinner(180));            
            obstacles.Add(new Cell(3, 2), new Rock());

            var robot = new Type1Robot(grid, startAt, facing, obstacles);

            Highlight(@"
---------------------------------------------------------------------
Please command me as follows:
-- Press L for left
-- Press R for right
-- Press F for front
-- Press B for back
-- Press ESC to stop
---------------------------------------------------------------------

");

            //take series of commands from user to move, for example. Stop when user presses ESC
            while (true)
            {
                RelativeDirection? towards = GetNextCommand();
                if (towards == null) {
                    Highlight(@"
--------------------------------------------------------------------
I'm done for the day. Good bye!
--------------------------------------------------------------------
");
                    break; 
                }
                try
                { 
                robot.Move((RelativeDirection)towards);                
                    Highlight(String.Format(@"
I'm now at [{0},{1}] facing {2}.
",
                        robot.At.Row, robot.At.Column, robot.Facing));
                }
                catch(InvalidOperationException)
                {
                    Console.WriteLine("\nError: I'm falling off the grid! Please try another move.\n");
                }
            }
        }

        /*
         * Writes to console with highlighted text
         */
        private static void Highlight(string value)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;

        }
       
        /*
         * Takes a single move command from the user. Either L/R/F/B. Stop if user presses ESC key.
         */
        private static RelativeDirection? GetNextCommand()
        {
            RelativeDirection? move = null;
            while (move == null)
            {
                Console.Write("? Where should I go next (L/R/F/B): ");
                var key = Console.ReadKey(false).KeyChar;
                if (key == (int)ConsoleKey.Escape) { break; }
                if (key == 'l' || key == 'L') { move = RelativeDirection.Left; }
                else if (key == 'r' || key == 'R') { move = RelativeDirection.Right; }
                else if (key == 'f' || key == 'F') { move = RelativeDirection.Forward; }
                else if (key == 'b' || key == 'B') { move = RelativeDirection.Back; }                
                else Console.WriteLine("\nError: Enter either L or R or F or B, or press ESC to stop.");
            }
            return move;
        }
    }
}
