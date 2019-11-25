using System;
using System.Configuration;

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

            Console.WriteLine("Reading configuration file...\n");
            
            var grid = GetGridSize();                       
            var at = GetStartLocation(grid);                       
            var facing = GetStartDirection();

            Console.WriteLine("\nI'm configured and ready to move.\n");

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
                var moveTo = grid.AdjacentTo(at, facing, (RelativeDirection)towards);
                if(moveTo!=null)
                {
                    at = (Cell)moveTo;
                    Highlight(String.Format(@"

I have moved {0} to [{1},{2}] facing {3}.
",
                        (RelativeDirection)towards, at.Row, at.Column, facing));
                }
                else
                {
                    Console.WriteLine(@"\nError: I'm falling off the grid! Please try another move.\n");
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
         * Reads size of grid in terms of number of rows and columns from configuration file.         
         */
        private static Grid GetGridSize()
        {
            var gridRowCount = ConfigurationManager.AppSettings["GridRowCount"];
            var gridColumnCount = ConfigurationManager.AppSettings["GridColumnCount"];
            
            if(!int.TryParse(gridRowCount, out int rows) || rows<=0)
            {
                Console.WriteLine("Error: Number of rows must be an integer greater than zero.");
                Environment.Exit(1);
            }
            if (!int.TryParse(gridColumnCount, out int columns) || columns <= 0)
            {
                Console.WriteLine("Error: Number of columns must be an integer greater than zero.");
                Environment.Exit(1);
            }

            Console.WriteLine("Grid size: {0}*{1}", rows, columns);
            return new Grid(rows, columns);
        }

        /*
         * Returns starting location in terms of row index and column index from configuration file.
         * (Indices start from 1, not 0)
         */
        private static Cell GetStartLocation(Grid grid)
        {
            var startRow = ConfigurationManager.AppSettings["StartRow"];
            var startColumn = ConfigurationManager.AppSettings["StartColumn"];
            
            if (!int.TryParse(startRow, out int row) || row <= 0 || row > grid.Rows)
            {
                Console.WriteLine("Error: Start row must be an integer between 1 and {0}.", grid.Rows);
                Environment.Exit(1);
            }
            if (!int.TryParse(startColumn, out int column) || column <= 0 || column > grid.Columns)
            {
                Console.WriteLine("Error: Start column must be an integer between 1 and {0}.", grid.Columns);
                Environment.Exit(1);
            }

            Console.WriteLine("Start location: [{0},{1}]", row, column);            
            return new Cell(row, column);
        }

        /*
         * Returns the starting direction that robot is facing in North/ South/ East/ West, as specified by user 
         */
        private static CardinalDirection GetStartDirection()
        {
            var startDirection = ConfigurationManager.AppSettings["StartDirection"];
            
            if(!Enum.TryParse(startDirection, out CardinalDirection direction))            
            {
                Console.WriteLine("Error: Start direction must be one of North/ South/ East/ West.");
                Environment.Exit(1);
            }

            Console.WriteLine("Start direction: {0}", direction.ToString());
            return direction;
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
