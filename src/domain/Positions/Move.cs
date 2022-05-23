using System.Collections.Generic;

namespace Checkmate.Detector.Domain.Positions
{
    public record Move
    {
        public Position Start { get; }
        public Position End { get; }
        
        public Move(Position start, Position end) => (Start, End) = (start, end);
        
        public Move(string startPosition, string endPosition) : this(Position.FromString(startPosition), Position.FromString(endPosition))
        {
        }

        public static IEnumerable<Move> GenerateOrthogonals(Position position, int distance)
        {
            var result = new List<Move>();
            for (int i = 0; i < distance; i++)
            {
                int offset = i + 1;
                result.Add(new Move(
                    position,
                    new Position(position.Column.Add(offset), position.Row + offset)));
                result.Add(new Move(
                    position,
                    new Position(position.Column.Add(-offset), position.Row + offset)));
                result.Add(new Move(
                    position, 
                    new Position(position.Column.Add(-offset), position.Row - offset)));
                result.Add(new Move(
                    position, 
                    new Position(position.Column.Add(offset), position.Row - offset)));
            }
            return result;
        }

        public static IEnumerable<Move> GenerateDiagonals(Position position, int distance)
        {
            var result = new List<Move>();
            for (int i = 0; i < distance; i++)
            {
                int offset = i + 1;
                result.Add(new Move(
                    position,
                    position.ColumnAdd(offset)));
                result.Add(new Move(
                    position,
                    position.RowAdd(offset)));
                result.Add(new Move(
                    position,
                    position.ColumnAdd(- offset)));
                result.Add(new Move(
                    position,
                    position.RowAdd(- offset)));
            }
            return result;
        }        

    }
}
