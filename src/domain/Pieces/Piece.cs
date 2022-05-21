using Checkmate.Detector.Domain.Positions;
using System;

namespace Checkmate.Detector.Domain.Pieces
{
    public record Piece(string Kind, Position Position)
    {
        public static Piece FromString(string piece) =>
            new Piece(piece.Substring(0, 1), 
                Position.FromString(piece.Substring(1, 2)));

        internal bool IsPawn() =>
            Kind.Equals("P");
    }
}
