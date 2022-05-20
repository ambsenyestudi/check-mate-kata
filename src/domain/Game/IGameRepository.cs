namespace Checkmate.Detector.Domain.Game
{
    public interface IGameRepository
    {
        GameId Add(GameLayout gameLayout);
        GameLayout GetBy(GameId gameId);
        void Replace(GameLayout gameLayout);
    }
}