﻿using System;

namespace RobotController
{
    /*
     * Represents a 2-dimensional grid
     */
    public class Grid : IEquatable<Grid>
    {
        public int Rows { get; }
        public int Columns { get; }

        /*
         * Initializes the grid with positive number of rows and columns.
         */
        public Grid(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentOutOfRangeException("rows/ columns", "Cannot be less than 0");
            }
            Rows = rows;
            Columns = columns;
        }

        public bool Equals(Grid grid)
        {
            return this.Rows == grid.Rows && this.Columns == grid.Columns;
        }

        /*
         * Checks if the given cell exists on this grid
         * */
        public bool Has(Cell cell)
        {
            return (1 <= cell.Row && cell.Row <= this.Rows
                && 1 <= cell.Column && cell.Column <= this.Columns);
        }

        /*
         * Gets the cell adjacent to  a given cell, when facing N/S/E/W, 
         * and towards left/right/front/back.
         * Returns null if there is no such adjacent cell.
         */
        public Cell? AdjacentTo(Cell at, CardinalDirection facing, RelativeDirection towards)
        {
            /* The two arrays below represent the difference between the row/ column indices
             * of the given cell and the adjacent cell.
             * The rows of these arrays represent the relative directions Left/Right/Forward/Back.
             * The columns of these arrays represent the cardinal directions North/East/South/West.
             * For example, 
             * Grid dimension: 3*3
             * Given cell (at) is the center of the grid [2,2]
             * Adjacent cell (return) towards Left facing North would be [2,1]
             * Hence, rowDiff[Left (0), North (0)] = 0 and columnDiff[Left (0), North (0)] = -1
             */
            var rowDiff = new int[4, 4]
                {
                    {0,-1,0,1},
                    {0,1,0,-1},
                    {-1,0,1,0},
                    {1,0,-1,0}
                };

            var columnDiff = new int[4, 4]
            {
                {-1,0,1,0},
                {1,0,-1,0},
                {0,-1,0,1},
                {0,1,0,-1}
            };

            var i = (int)towards - (int)RelativeDirection.Left;
            var j = (int)facing - (int)CardinalDirection.North;            
            var adjacentRow = at.Row + rowDiff[i,j];
            var adjacentColumn = at.Column + columnDiff[i, j];

            var adjacentCell = new Cell(adjacentRow, adjacentColumn);
            if (this.Has(adjacentCell))
            {
                return adjacentCell;
            }
            return null;
        }
    }
}
