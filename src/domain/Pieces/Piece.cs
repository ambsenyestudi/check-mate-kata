using Checkmate.Detector.Domain.Positions;

namespace Checkmate.Detector.Domain.Pieces
{
    public record Piece(string Kind, Position Position)
    {
        public static Piece FromString(string piece) =>
            new Piece(piece.Substring(0, 1), 
                Position.FromString(piece.Substring(1, 2)));

        public bool CanKill(Piece other)
        {
            if (Kind.PieceKindFromInitial() == PieceKind.Pawn)
            {

                return other.Position.IsInFrontOf(Position) && 
                    other.Position.IsInDiagonalTo(Position) &&
                    Position.GetDistance(other.Position) == 1;
            }
            if (Kind.PieceKindFromInitial() == PieceKind.Bishop)
            {
                return other.Position.IsInDiagonalTo(Position);
            }
            return false;
        }

        public override string ToString() =>
            Kind + Position.ToString();

    }
}
