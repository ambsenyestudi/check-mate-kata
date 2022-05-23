using Checkmate.Detector.Domain.Boards;

namespace Checkmate.Detector.Domain.Game
{
    public interface IGameRepository
    {
        GameId Add(BoardLayout gameLayout);
        BoardLayout GetBy(GameId gameId);
        void Replace(BoardLayout gameLayout);
    }
}