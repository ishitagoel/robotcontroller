using System;

namespace Robot
{
    /*
     * Represents a cell [row,column] on a 2-dimensional grid
     */
    public struct Cell
    {
        public int Row { get; }
        public int Column { get; }

        public Cell(int row, int column)
        {            
            Row = row;
            Column = column;
        }       
    }
}
