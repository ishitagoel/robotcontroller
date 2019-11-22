using System;

namespace Robot.ConsoleApp
{
    class Program
    {
        private static ConsoleColor _infoColor = ConsoleColor.Cyan;
        private static ConsoleColor _inputColor = ConsoleColor.White;

        static void Main(string[] args)
        {
            Console.ForegroundColor = _infoColor;
            Console.WriteLine(@"
Hi! I am Gridomatic the Robot. I can walk on any 2-dimensional grid. 
You can control my movement.

");
            Console.ForegroundColor = _inputColor;
            var grid = ReadGridDimensions();

            Console.ForegroundColor = _infoColor;
            Console.Write(@"
Great! You have configured me to walk on a grid of size {0} * {1}.
", grid.Rows, grid.Columns);

            Console.ForegroundColor = _inputColor;

        }

        private static Grid ReadGridDimensions()
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
    }
}
