using Checkmate.Detector.Domain.Pieces;
using Checkmate.Detector.Domain.Positions;
using System.Collections.Generic;
using System.Linq;

namespace Checkmate.Detector.Domain.Boards
{
    public class BoardService : IBoardService
    {
        private readonly Position MIN_POSITION = new Position(Columns.A, 1);
        private readonly Position MAX_POSITION = new Position(Columns.H, 8);
        public IEnumerable<Position> GetPossibleMoves(Piece piece)
        {
            if (piece.Kind is PieceKind.King)
            {
                return GetPossibleMoves(PieceKind.King, piece.Position)
                    .Where(x => IsInBounds(x));
            }

            return Enumerable.Empty<Position>();
        }

        private bool IsInBounds(Position position) =>
            position.Column >= MIN_POSITION.Column && position.Row >= MIN_POSITION.Row &&
            position.Column <= MAX_POSITION.Column && position.Row <= MAX_POSITION.Row;

        private IEnumerable<Position> GetPossibleMoves(PieceKind kind, Position position)
        {
            if (kind is PieceKind.King)
            {
                return GenerateDiagonals(position, 1).Concat(GenerateOrthogonals(position, 1));
            }
            return Enumerable.Empty<Position>();
        }

        private IEnumerable<Position> GenerateOrthogonals(Position position, int distance)
        {
            var result = new List<Position>();
            for (int i = 0; i < distance; i++)
            {
                int offset = i + 1;
                result.Add(new Position(position.Column.Add(offset), position.Row + offset));
                result.Add(new Position(position.Column.Add(-offset), position.Row + offset));
                result.Add(new Position(position.Column.Add(-offset), position.Row - offset));
                result.Add(new Position(position.Column.Add(offset), position.Row - offset));
            }
            return result;
        }

        private IEnumerable<Position> GenerateDiagonals(Position position, int distance)
        {
            var result = new List<Position>();
            for (int i = 0; i < distance; i++)
            {
                int offset = i + 1;
                result.Add(new Position(position.Column.Add(offset), position.Row));
                result.Add(new Position(position.Column, position.Row + offset));
                result.Add(new Position(position.Column.Add(-offset), position.Row));
                result.Add(new Position((position.Column), position.Row - offset));
            }
            return result;
        }
    }
}
