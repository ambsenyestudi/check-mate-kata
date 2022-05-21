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

        public int GetDistance(Position other)
        {
            return (int)Math.Sqrt(
                Math.Pow(Column[0] - other.Column[0], 2) +
                Math.Pow(Row - other.Row, 2));
        }
    }
}
