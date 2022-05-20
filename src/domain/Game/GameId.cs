using System;

namespace Checkmate.Detector.Domain.Game
{
    public record GameId(Guid Value)
    {
        public static GameId Empty = new GameId(Guid.Empty);
        public static GameId Create() => new GameId(Guid.NewGuid());
    }
}
