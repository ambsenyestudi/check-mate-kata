using Checkmate.Detector.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkmate.Detector.Domain.Pieces
{
    public class PieceCollection
    {
        public static PieceCollection Empty { get; } = new PieceCollection(new string[0]);
        public Piece[] Pieces { get; }

        public PieceCollection(IEnumerable<Piece> pieces) => (Pieces) = (pieces.ToArray());

        public PieceCollection(string[] pieces) : this(pieces.Select(x => Piece.FromString(x)))
        {
        }

        public PieceCollection MovePieceAt(Move move)
        {
            var foundPiece = Pieces.FirstOrDefault(x=>x.Position.Equals(move.Start));
            return new PieceCollection(UpdatePieces(move, foundPiece));
        }

        public override bool Equals(object obj)
        {
            var other = obj as PieceCollection;
            if(other == null)
            {
                return false;
            }
            return Pieces.SequenceEqual(other.Pieces);
        }

        public Piece GetPiece(PieceKind kind) =>
            Pieces.FirstOrDefault(x => x.Kind.Equals(kind));

        public bool ContainsPosition(Position position) =>
            Pieces.Any(x => x.Position.Equals(position));

        public Piece GetPieceAt(Position position)
        {
            if (!ContainsPosition(position))
            {
                return null;
            }
            var piece = Pieces.First(x => x.Position.Equals(position));
            return piece;
        }

        private Piece[] UpdatePieces(Move move, Piece foundPiece)
        {
            var movePieces = new Piece[Pieces.Length];
            for (int i = 0; i < Pieces.Length; i++)
            {
                var piece = Pieces[i];
                if (piece.Equals(foundPiece))
                {
                    piece = new Piece(piece.Kind, move.End);
                }
                movePieces[i] = piece;
            }

            return movePieces;
        }
        
    }
}
