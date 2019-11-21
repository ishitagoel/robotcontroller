using System;
using System.Collections.Generic;
using System.Text;

namespace Robot
{
    public class Grid
    {
        public int Rows { get; }
        public int Columns { get; }

        public Grid(int rows, int columns)
        {
            if (rows <= 0 || columns <= 0)
            {
                throw new ArgumentOutOfRangeException("rows/ columns", "Cannot be less than 0");
            }
            Rows = rows;
            Columns = columns;
        }
    }
}
