using Checkmate.Detector.Domain.Positions;

namespace Checkmate.Detector.Domain.Pieces
{
    public record Piece(string Kind, Position Position)
    {
        public static Piece FromString(string piece) =>
            new Piece(piece.Substring(0, 1), 
                Position.FromString(piece.Substring(1, 2)));

        internal bool IsPawn() =>
            Kind.Equals("P");

        public override string ToString() =>
            Kind + Position.ToString();

        public bool CanKill(Piece other)
        {
            if (IsPawn())
            {
                return Position.GetDistance(other.Position) == 1;
            }
            return false;
        }
    }
}
