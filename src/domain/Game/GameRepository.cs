using System.Collections.Generic;

namespace Checkmate.Detector.Domain.Game
{
    public class GameRepository : IGameRepository
    {
        public Dictionary<GameId, GameLayout> Cache { get; } = new Dictionary<GameId, GameLayout>();
        public GameId Add(GameLayout gameLayout)
        {
            if(gameLayout.GameId == GameId.Empty)
            {
                //todo fix pi
                gameLayout = new GameLayout(GameId.Create(), gameLayout.GetPieces());
            }
            Cache.Add(gameLayout.GameId, gameLayout);
            return gameLayout.GameId;
        }

        public GameLayout GetBy(GameId gameId) =>
            Cache[gameId];

        public void Replace(GameLayout gameLayout) =>
            Cache[gameLayout.GameId] = gameLayout;
    }
}
