using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkmate.Detector.Domain.Game
{
    public record GameLayout(GameId GameId, string[] Pieces)
    {
        public static GameLayout Empty { get; } = new GameLayout(GameId.Empty, new string[0]);

        public bool IsPieceAt(string startPosition) =>
            Pieces.Any(x => x.Contains(startPosition, StringComparison.CurrentCultureIgnoreCase));

        public GameLayout Move(Move move)
        {
            var foundPiece = GetPiece(move.startPosition);
            string[] pieces = NewMethod(move, foundPiece);
            return new GameLayout(GameId, pieces);
        }

        private string[] NewMethod(Move move, string foundPiece)
        {
            var pieces = new string[Pieces.Length];
            for (int i = 0; i < Pieces.Length; i++)
            {
                var piece = pieces[i];
                if (piece.Equals(foundPiece))
                {
                    piece = MovePiece(move, foundPiece);
                }
                pieces[i] = piece;
            }

            return pieces;
        }

        private string MovePiece(Move move, string foundPiece) =>
            foundPiece.Replace(move.startPosition, move.endPosition);

        private string GetPiece(string position)
        {
            if(!IsPieceAt(position))
            {
                return string.Empty;
            }
            return Pieces.First(x => x.Contains(position, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}