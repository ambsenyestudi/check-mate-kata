using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class KnightShould
    {
        private readonly GameService gameService;
        private readonly CheckService checkService;
        public KnightShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository);
            checkService = new CheckService(gameRepository);
        }
        [Theory]
        [InlineData("Nc5", "Kd8", "d7")]
        [InlineData("Ng5", "Kd8", "e7")]
        public void Ignore_When_King_Not_At_Range(
            string knight, string king, string endPos)
        {
            var gameId = gameService.Load(knight, king);
            var startPos = knight.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.False(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Nc5", "Kd8", "b7")]
        [InlineData("Ng5", "Kd8", "f7")]
        [InlineData("Na6", "Kd7", "b8")]
        [InlineData("Ng7", "Kd7", "f8")]
        [InlineData("Nb8", "Kd8", "c6")]
        [InlineData("Nf7", "Kd8", "e6")]
        [InlineData("Na7", "Kd6", "c8")]
        [InlineData("Nf6", "Kd6", "e8")]
        public void Detect_Check_When_Moved_At_Killing_Range(string knight, string king, string endPos)
        {
            var gameId = gameService.Load(knight, king);
            var startPos = knight.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
