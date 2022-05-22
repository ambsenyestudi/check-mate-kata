using System;

namespace Checkmate.Detector.Domain.Positions
{
    public record Position(string Column, int Row)
    {
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
            return (int)Math.Sqrt(
                Math.Pow(Column[0] - other.Column[0], 2) +
                Math.Pow(Row - other.Row, 2));
        }

        public bool IsInDiagonalTo(Position other) =>
            Math.Abs(Column[0] - other.Column[0])
            .Equals(GetDistance(Row, other.Row));


        public override string ToString() =>
            Column + Row;

        public bool IsOrthogonalTo(Position other) =>
            !Equals(other) &&
            (Column[0] - other.Column[0] == 0 ||
            Row - other.Row == 0);

        public bool IsOthogonalCombined(Position other)
        {
            var rowDistance = GetDistance(Row, other.Row);
            int colDistance = GetDistance(Column, other.Column);
            return (rowDistance == 1 && colDistance == 2) ||
                (rowDistance == 2 && colDistance == 1);
        }

        private int GetDistance(string start, string end) =>
            GetDistance(start[0], end[0]);

        private int GetDistance(int start, int end) =>
            Math.Abs(start - end);

    }
}
