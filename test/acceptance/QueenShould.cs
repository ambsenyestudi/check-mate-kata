using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class QueenShould
    {
        private readonly GameService gameService;
        private readonly CheckService checkService;
        public QueenShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository);
            checkService = new CheckService(gameRepository);
        }

        [Theory]
        [InlineData("Qa1", "Kd8", "d1")]
        [InlineData("Qa1", "Kd8", "a8")]
        [InlineData("Qh1", "Kd8", "d1")]
        [InlineData("Qh1", "Kd8", "h8")]
        [InlineData("Qa8", "Kd1", "d8")]
        public void Detect_Check_When_Moved_At_Killing_Range(string queen, string king, string endPos)
        {
            var gameId = gameService.Load(queen, king);
            var startPos = queen.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
