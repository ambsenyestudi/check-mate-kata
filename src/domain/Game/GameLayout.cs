using System;
using System.Linq;

namespace Checkmate.Detector.Domain.Game
{
    public record GameLayout(GameId GameId, string[] Pieces)
    {
        public static GameLayout Empty { get; } = new GameLayout(GameId.Empty, new string[0]);

        public bool IsPieceAt(string startPosition) =>
            Pieces.Any(x => string.Equals(x, startPosition, StringComparison.CurrentCultureIgnoreCase));
    }
}