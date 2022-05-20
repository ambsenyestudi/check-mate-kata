using System;

namespace Checkmate.Detector.Domain.Game
{
    public record GameLayout(GameId GameId, string[] Pieces)
    {
        public static GameLayout Empty { get; } = new GameLayout(GameId.Empty, new string[0]);
    }
}