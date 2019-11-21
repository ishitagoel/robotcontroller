using System;

namespace Robot
{
    public class Location
    {
        readonly int _row;
        readonly int _column;
        
        public Location(int row, int column)
        {
            if(row<=0 || column<=0)
            {
                throw new ArgumentOutOfRangeException("row/ column", "Cannot be less than 0");
            }

            _row = row;
            _column = column;
        }

        public override string ToString()
        {
            return String.Format("[{0},{1}]", _row, _column);
        }
    }
}
