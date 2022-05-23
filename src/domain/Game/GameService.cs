using Checkmate.Detector.Domain.Positions;

namespace Checkmate.Detector.Domain.Game
{
    public class GameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public GameId Load(params string[] pieces)
        {
            var gameId = gameRepository.Add(new BoardLayout(GameId.Empty, pieces));
            return gameRepository.GetBy(gameId) == BoardLayout.Empty
                ? GameId.Empty
                : gameId;
        }

        public bool TryMove(string startPosition, string endPosition, GameId gameId)
        {
            var gameLayout = gameRepository.GetBy(gameId);
            var start = Position.FromString(startPosition);
            if (!gameLayout.IsPieceAt(start))
            {
                return false;
            }
            var resultGameLayout = gameLayout.Move(
                new Move(start,
                Position.FromString(endPosition)));
            gameRepository.Replace(resultGameLayout);
            return true;
        }
    }
}
