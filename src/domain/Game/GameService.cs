using Checkmate.Detector.Domain.Game;
using System;

namespace Checkmate.Detector.Domain
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
            var gameId = gameRepository.Add(new GameLayout(GameId.Empty, pieces));
            return gameRepository.GetBy(gameId) == GameLayout.Empty 
                ? GameId.Empty
                : gameId;
        }

        public bool TryMove(string startPosition, string endPosition)
        {
            throw new NotImplementedException();
        }
    }
}
