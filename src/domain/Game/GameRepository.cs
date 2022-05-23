using System.Collections.Generic;

namespace Checkmate.Detector.Domain.Game
{
    public class GameRepository : IGameRepository
    {
        public Dictionary<GameId, BoardLayout> Cache { get; } = new Dictionary<GameId, BoardLayout>();
        public GameId Add(BoardLayout gameLayout)
        {
            if(gameLayout.GameId == GameId.Empty)
            {
                //todo fix pi
                gameLayout = new BoardLayout(GameId.Create(), gameLayout.GetPieces());
            }
            Cache.Add(gameLayout.GameId, gameLayout);
            return gameLayout.GameId;
        }

        public BoardLayout GetBy(GameId gameId) =>
            Cache[gameId];

        public void Replace(BoardLayout gameLayout) =>
            Cache[gameLayout.GameId] = gameLayout;
    }
}
