using Checkmate.Detector.Domain.Positions;
using System.Collections.Generic;
using System.Linq;

namespace Checkmate.Detector.Domain
{
    public class PathCalculationService : IPathCalculationService
    {
        public IEnumerable<Position> GetPath(Move move)
        {
            var distance = move.End.GetDistance(move.Start);
            if (distance <= 1)
            {
                return Enumerable.Empty<Position>();
            }

            var inBetweenDistnace = distance - 1;
            var (column, row) = FigureVector(move, inBetweenDistnace);
            var result = new List<Position>();
            for (int i = 0; i < inBetweenDistnace; i++)
            {
                var newColumn = OffsetColumn(move, column * (i + 1));
                var newRow = move.Start.Row + row * (i + 1);
                result.Add(new Position(newColumn, newRow));
            }
            return result;
        }

        private static Columns OffsetColumn(Move move, int offset)
        {
            return (Columns)(move.Start.Column + offset);
        }

        private (int, int) FigureVector(Move move, int distance) =>
            (Normalize(move.End.Column - move.Start.Column, distance),
                Normalize(move.End.Row - move.Start.Row, distance));

        private int Normalize(int component, int distance) =>
            component / distance;
    }
}
