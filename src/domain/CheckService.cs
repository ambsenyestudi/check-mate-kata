using Checkmate.Detector.Domain.Game;
using Checkmate.Detector.Domain.Positions;
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
            var king = pieces.Last();
            var startPosition = Position.FromString(attackPiece.Substring(1, 2));
            var endPosition = Position.FromString(king.Substring(1, 2));
            return endPosition.GetDistance(startPosition);
        }

        private bool IsPawn(string attackPiece) =>
            attackPiece.StartsWith("P");
    }
}
