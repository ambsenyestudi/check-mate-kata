using System;
using System.Linq;

namespace Checkmate.Detector.Domain.Pieces
{
    public class PieceCollection
    {
        public static PieceCollection Empty { get; } = new PieceCollection(new string[0]);
        public string[] Pieces { get; }

        public PieceCollection(string[] pieces)
        {
            Pieces = pieces;
        }

        public bool ContainsPosition(string startPosition) =>
            Pieces.Any(x => x.Contains(startPosition, StringComparison.CurrentCultureIgnoreCase));

        public PieceCollection MovePieceAt(Move move)
        {
            var foundPiece = GetPiece(move.startPosition);
            string[] movedPieces = UpdatePieces(move, foundPiece);
            return new PieceCollection(movedPieces);
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
        private string[] UpdatePieces(Move move, string foundPiece)
        {
            var movePieces = new string[Pieces.Length];
            for (int i = 0; i < Pieces.Length; i++)
            {
                var piece = Pieces[i];
                if (piece.Equals(foundPiece))
                {
                    piece = MovePiece(move, foundPiece);
                }
                movePieces[i] = piece;
            }

            return movePieces;
        }

        private string MovePiece(Move move, string foundPiece) =>
           foundPiece.Replace(move.startPosition, move.endPosition);

        private string GetPiece(string position)
        {
            if (!ContainsPosition(position))
            {
                return string.Empty;
            }
            return Pieces.First(x => x.Contains(position, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
