using Checkmate.Detector.Domain.Game;
using Checkmate.Detector.Domain.Pieces;
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
            
            var attackPiece = Piece.FromString(pieces.First());
            var king = Piece.FromString(pieces.Last());
            return attackPiece.CanKill(king);            
        }
    }
}
