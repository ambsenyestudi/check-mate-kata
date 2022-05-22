using System;

namespace Checkmate.Detector.Domain.Positions
{
    public record Position
    {
        public Columns Column { get; }
        public int Row { get; }
        
        public Position(string column, int row):this((Columns)Enum.Parse(typeof(Columns), column, true), row)
        {

        }
        public Position(Columns column, int row) => (Column, Row) = (column, row);
        
        public static Position FromString(string input)
        {
            if (!int.TryParse(input.Substring(1), out int row))
            {
                throw new ArgumentException($"Row in position {input} must be a number");
            }
            return new Position(input.Substring(0, 1), row);
        }
        
        public bool IsInFrontOf(Position other) =>
            Row > other.Row;
        
        public int GetDistance(Position other)
        {
            if(IsInDiagonalTo(other))
            {
                return Math.Abs(GetDistance(Row, other.Row));
            }
            var columnDistance = GetDistance(Column, other.Column);
            var result = Math.Sqrt(
                Math.Pow((float)columnDistance, 2) +
                Math.Pow((float)GetDistance(Row, other.Row), 2));
            return (int)result;
        }
        
        public bool IsInDiagonalTo(Position other) =>
            Math.Abs(GetDistance(Column, other.Column))
            .Equals(GetDistance(Row, other.Row));

        
        public override string ToString() =>
            Column.ToString() + Row;
        
        public bool IsOrthogonalTo(Position other) =>
            !Equals(other) &&
            (GetDistance(Column, other.Column) == 0 ||
            GetDistance(Row, other.Row) == 0);
        
        public bool IsOthogonalCombined(Position other)
        {
            var rowDistance = GetDistance(Row, other.Row);
            int colDistance = GetDistance(Column, other.Column);
            return (rowDistance == 1 && colDistance == 2) ||
                (rowDistance == 2 && colDistance == 1);
        }
        
        private int GetDistance(Columns end, Columns start) =>
            GetDistance((int)end, (int)start);

        private int GetDistance(int end, int start) =>
            Math.Abs(end - start);
        
    }
}
