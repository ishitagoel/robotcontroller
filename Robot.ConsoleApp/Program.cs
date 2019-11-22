using System;

namespace Robot.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //welcome user
            Highlight(@"
Hi! I am Gridomatic the Robot. I can walk on any 2-dimensional grid. 
You can control my movement.

");            
            //take grid dimensions from user
            var grid = GetGridDimensions();

            Highlight(String.Format(@"
You have configured me to walk on a grid of size ({0} * {1}).
", grid.Rows, grid.Columns));

            //take starting location from user
            var cell = GetStartingLocation(grid);

            Highlight(String.Format(@"
You have configured my starting location as ({0}, {1}).
", cell.Row, cell.Column));

            //take starting direction from user
            var direction = GetStartingDirection();

            Highlight(String.Format(@"
You have configured my starting direction as ({0}).
", direction));
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
                Console.Write("? Please enter the number of rows in the grid: ");                
                if(!int.TryParse(Console.ReadLine(), out rows) || rows<=0)
                {
                    Console.WriteLine("Error: Number of rows must be an integer greater than zero.");
                    rows = 0;
                }              
            }
            int columns = 0;
            while (columns == 0)
            {
                Console.Write("? Please enter the number of columns in the grid: ");
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
                Console.Write("? Please enter the row index of my starting location: ");
                if (!int.TryParse(Console.ReadLine(), out row) || row <= 0 || row>grid.Rows)
                {
                    Console.WriteLine("Error: Row index must be an integer between 1 and {0}.", grid.Rows);
                    row = 0;
                }
            }
            int column = 0;
            while (column == 0)
            {
                Console.Write("? Please enter the number of columns in the grid: ");
                if (!int.TryParse(Console.ReadLine(), out column) || column <= 0 || column>grid.Columns)
                {
                    Console.WriteLine("Error: Column index must be an integer between 1 and {0}.", grid.Columns);
                    column = 0;
                }
            }

            return new Cell(row, column);
        }

        private static CardinalDirection GetStartingDirection()
        {
            CardinalDirection? direction = null;
            while (direction == null)
            {
                Console.Write("? Please enter the my starting direction as North/ South/ East/ West: ");
                var str = Console.ReadLine().ToLower().Trim();
                if (str == "n" || str == "north") { direction = CardinalDirection.North; }
                else if (str == "s" || str == "south") { direction = CardinalDirection.South; }
                else if (str == "e" || str == "east") { direction = CardinalDirection.East; }
                else if (str == "w" || str == "west") { direction = CardinalDirection.West; }
                else Console.WriteLine("Error: Starting direction must be one of North/ South/ East/ West.");               
            }
            return (CardinalDirection)direction;
        }
    }
}
