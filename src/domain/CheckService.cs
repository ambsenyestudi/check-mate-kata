using Checkmate.Detector.Domain.Game;
using Checkmate.Detector.Domain.Pieces;
using Checkmate.Detector.Domain.Positions;
using System;
using System.Linq;

namespace Checkmate.Detector.Domain
{
    public class CheckService
    {
        private readonly IPathCalculationService pathCalculationService;
        private readonly IGameRepository gameRepository;

        public CheckService(IPathCalculationService pathCalculationService, IGameRepository gameRepository)
        {
            this.pathCalculationService = pathCalculationService;
            this.gameRepository = gameRepository;
        }

        public bool IsCheck(GameId gameId)
        {
            var gameLayout = gameRepository.GetBy(gameId);
            var pieces = gameLayout.GetPieces();
            
            var attackPiece = Piece.FromString(pieces.First());
            var king = Piece.FromString(pieces.Last());
            
            return !IsPathBlocked(attackPiece, king, gameLayout) && attackPiece.CanKill(king);            
        }
        public bool IsPathBlocked(Piece attackPiece, Piece king, GameLayout gameLayout)
        {
            if (attackPiece.Kind is PieceKind.Pawn ||
                attackPiece.Kind is PieceKind.Knight)
            {
                return false;
            }
            var path = pathCalculationService.GetPath(
                new Move(attackPiece.Position, king.Position)); ;
            if (!path.Any())
            {
                return false;
            }
            return path.Any(x => gameLayout.GetPieceAt(x) != null);
        }

        public bool IsCheckmate(GameId gameId)
        {
            var gameLayout = gameRepository.GetBy(gameId);
            var pieces = gameLayout.GetPieces();

            var attackPiece = Piece.FromString(pieces.First());
            var king = Piece.FromString(pieces.Last());

            var isCheck = !IsPathBlocked(attackPiece, king, gameLayout) && attackPiece.CanKill(king);
            var movedGameLayout = gameLayout.Move(new Move(new Position(Columns.A, 8), new Position(Columns.B, 8)));
            var movePieces = movedGameLayout.GetPieces();
            //need to evaluate pieces for match
            var otherPiece = Piece.FromString(movePieces[1]);
            var movedKing = Piece.FromString(movePieces.Last());
            var isMovedCheck = !IsPathBlocked(otherPiece, movedKing, movedGameLayout) && otherPiece.CanKill(movedKing);
            return isCheck && isMovedCheck;
        }
    }
}
