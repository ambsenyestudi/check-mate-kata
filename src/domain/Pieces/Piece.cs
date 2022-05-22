using Checkmate.Detector.Domain.Positions;

namespace Checkmate.Detector.Domain.Pieces
{
    public record Piece
    {
        public PieceKind Kind { get; }
        public Position Position { get; }

        public Piece(string Kind, Position Position):this(Kind.PieceKindFromInitial(), Position)
        {
        }

        public Piece(PieceKind kind, Position position) => (Kind, Position) = (kind, position);

        public static Piece FromString(string piece) =>
            new Piece(piece.Substring(0, 1), 
                Position.FromString(piece.Substring(1, 2)));

        public bool CanKill(Piece other)
        {
            if (Kind is PieceKind.Pawn)
            {

                return other.Position.IsInFrontOf(Position) && 
                    other.Position.IsInDiagonalTo(Position) &&
                    Position.GetDistance(other.Position) == 1;
            }
            if (Kind is PieceKind.Bishop)
            {
                return other.Position.IsInDiagonalTo(Position);
            }
            if (Kind is PieceKind.Rook)
            {
                return other.Position.IsOrthogonalTo(Position);
            }
            if (Kind is PieceKind.Knight)
            {
                return other.Position.IsOthogonalCombined(Position);
            }
            if(Kind is PieceKind.Queen)
            {
                return other.Position.IsOrthogonalTo(Position) ||
                    other.Position.IsInDiagonalTo(Position);
            }
            return false;
        }

        public override string ToString() =>
            Kind.GetInitial() + Position.ToString();

    }
}
