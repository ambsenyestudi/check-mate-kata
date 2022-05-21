using Checkmate.Detector.Domain.Game;
using System;
using System.Linq;

namespace Checkmate.Detector.Domain
{
    public class CheckService
    {
        private IGameRepository gameRepository;

        public CheckService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public bool IsCheck(GameId gameId)
        {
            var gameLayout = gameRepository.GetBy(gameId);
            var pieces = gameLayout.GetPieces();
            var attackPiece = pieces.First();
            if(IsPawn(attackPiece))
            {
                return GetDistance(pieces) == 1;
            }
            return false;
        }

        private int GetDistance(string[] pieces)
        {
            var attackPiece = pieces.First();
            var kingPiece = pieces.Last();
            return (int)Math.Sqrt(
                Math.Pow(GetColumnDistance(attackPiece, kingPiece), 2) +
                Math.Pow(GetRowDistance(attackPiece, kingPiece), 2));
        }

        private double GetRowDistance(string attackPiece, string kingPiece) =>
            attackPiece[2] - kingPiece[2];

        private int GetColumnDistance(string attackPiece, string kingPiece) =>
            attackPiece[1] - kingPiece[1];

        private bool IsPawn(string attackPiece) =>
            attackPiece.StartsWith("P");
    }
}
