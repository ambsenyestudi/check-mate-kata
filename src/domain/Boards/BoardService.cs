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
                return Move.GenerateDiagonals(position, 1)
                    .Concat(Move.GenerateOrthogonals(position, 1))
                    .Select(x=>x.End);
            }
            return Enumerable.Empty<Position>();
        }
    }
}
