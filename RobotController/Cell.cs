using System;

namespace RobotController
{
    /*
     * Represents a cell [row,column] on a 2-dimensional grid
     */
    public struct Cell : IEquatable<Cell>
    {
        public int Row { get; }
        public int Column { get; }

        public Cell(int row, int column)
        {            
            Row = row;
            Column = column;
        }   
        
        public bool Equals(Cell cell)
        {
            return cell.Row == this.Row && cell.Column == this.Column;
        }
    }
}
