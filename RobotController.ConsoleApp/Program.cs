using System;

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
Please answer the following questions so that we can get started.
--------------------------------------------------------------------

");            
            //take grid dimensions from user
            var grid = GetGridDimensions();

            Highlight(String.Format(@"
You have configured me to move on a grid of size ({0} * {1}).
", grid.Rows, grid.Columns));

            //take starting location from user
            var at = GetStartingLocation(grid);

            Highlight(String.Format(@"
You have configured my starting location as ({0}, {1}).
", at.Row, at.Column));

            //take starting direction from user
            var facing = GetStartingDirection();

            Highlight(String.Format(@"
You have configured my starting direction as ({0}).
", facing));

            Highlight(@"
---------------------------------------------------------------------
Great! I am now ready to start moving. 
Before each move, I shall ask you where to go. 
You can tell me to go left (L)/ right (R)/ forward (F)/ back (B).  
Or press ESC key to stop.
---------------------------------------------------------------------

");

            //take series of commands from user to move, for example. Stop when user presses ESC
            while (true)
            {
                RelativeDirection? towards = GetNextMove();
                if (towards == null) {
                    Highlight(@"
--------------------------------------------------------------------
I'm done for the day. Good bye!
--------------------------------------------------------------------
");
                    break; 
                }
                var moveTo = grid.AdjacentTo(at, facing, (RelativeDirection)towards);
                if(moveTo!=null)
                {
                    at = (Cell)moveTo;
                    Highlight(String.Format(@"

I have moved to ({0}, {1}) facing ({2}).
", 
                        at.Row, at.Column, facing));
                }
                else
                {
                    Console.WriteLine(@"
Error: I'm falling off the grid! Please try another move.
");
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
         * Returns a grid of dimensions specified by the user in terms of number of rows and columns.         
         */
        private static Grid GetGridDimensions()
        {
            int rows=0;
            while(rows==0)
            {
                Console.Write("? Enter the number of rows in the grid: ");                
                if(!int.TryParse(Console.ReadLine(), out rows) || rows<=0)
                {
                    Console.WriteLine("Error: Number of rows must be an integer greater than zero.");
                    rows = 0;
                }              
            }
            int columns = 0;
            while (columns == 0)
            {
                Console.Write("? Enter the number of columns in the grid: ");
                if (!int.TryParse(Console.ReadLine(), out columns) || columns<=0)
                {
                    Console.WriteLine("Error: Number of columns must be an integer.");
                    columns = 0;
                }
            }
            
            return new Grid(rows, columns);
        }

        /*
         * Returns starting location of the robot as specified by the user in terms of row index and column index.
         * (Indices start from 1, not 0)
         */
        private static Cell GetStartingLocation(Grid grid)
        {
            int row = 0;
            while (row == 0)
            {
                Console.Write("? Enter the row index of my starting location: ");
                if (!int.TryParse(Console.ReadLine(), out row) || row <= 0 || row>grid.Rows)
                {
                    Console.WriteLine("Error: Row index must be an integer between 1 and {0}.", grid.Rows);
                    row = 0;
                }
            }
            int column = 0;
            while (column == 0)
            {
                Console.Write("? Enter the number of columns in the grid: ");
                if (!int.TryParse(Console.ReadLine(), out column) || column <= 0 || column>grid.Columns)
                {
                    Console.WriteLine("Error: Column index must be an integer between 1 and {0}.", grid.Columns);
                    column = 0;
                }
            }

            return new Cell(row, column);
        }

        /*
         * Returns the starting direction that robot is facing in North/ South/ East/ West, as specified by user 
         */
        private static CardinalDirection GetStartingDirection()
        {
            CardinalDirection? direction = null;
            while (direction == null)
            {
                Console.Write("? Enter the my starting direction as North/ South/ East/ West: ");
                var str = Console.ReadLine().ToLower().Trim();
                if (str == "n" || str == "north") { direction = CardinalDirection.North; }
                else if (str == "s" || str == "south") { direction = CardinalDirection.South; }
                else if (str == "e" || str == "east") { direction = CardinalDirection.East; }
                else if (str == "w" || str == "west") { direction = CardinalDirection.West; }
                else Console.WriteLine("Error: Starting direction must be one of North/ South/ East/ West.");               
            }
            return (CardinalDirection)direction;
        }

        /*
         * Takes a single move command from the user. Either L/R/F/B. Stop if user presses ESC key.
         */
        private static RelativeDirection? GetNextMove()
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
                else Console.WriteLine("Error: Enter either L or R or F or B, or press ESC to stop.");
            }
            return move;
        }
    }
}
