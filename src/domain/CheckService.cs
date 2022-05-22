using Checkmate.Detector.Domain.Boards;
using Checkmate.Detector.Domain.Game;
using Checkmate.Detector.Domain.Pieces;
using Checkmate.Detector.Domain.Positions;
using System;
using System.Linq;

namespace Checkmate.Detector.Domain
{
    public class CheckService
    {
        private readonly IBoardService boardService;
        private readonly IPathCalculationService pathCalculationService;
        private readonly IGameRepository gameRepository;

        public CheckService(IBoardService boardService, IPathCalculationService pathCalculationService, IGameRepository gameRepository)
        {
            this.boardService = boardService;
            this.pathCalculationService = pathCalculationService;
            this.gameRepository = gameRepository;
        }

        public bool IsCheck(GameId gameId)
        {
            var gameLayout = gameRepository.GetBy(gameId);
            return IsCheck(gameLayout, gameLayout.GetKing());
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
            var king = gameLayout.GetKing();
            if(!IsCheck(gameLayout, king))
            {
                return false;
            }
            var possibleMoves = boardService.GetPossibleMoves(king).ToArray();
            var isCheck = true;
            var count = 0;
            while(isCheck && count < possibleMoves.Length)
            {
                var currLayout = gameLayout.Move(new Move(king.Position, possibleMoves[count]));
                isCheck = IsCheck(currLayout, currLayout.GetKing());
                count ++;
            }
            return isCheck;
        }

        private bool IsCheck(GameLayout gameLayout, Piece king)
        {
            var pieces = gameLayout.GetPieces();
            return pieces
                .Where(x => !x.Equals(king))
                .Select(x => Piece.FromString(x))
                .Any(at => !IsPathBlocked(at, king, gameLayout) && at.CanKill(king));
        }
    }
}
