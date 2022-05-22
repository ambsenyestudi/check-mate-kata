using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class QueenShould
    {
        private readonly GameService gameService;
        private readonly IPathCalculationService pathCalculationService;
        private readonly CheckService checkService;
        public QueenShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository); 
            pathCalculationService = new PathCalculationService();
            checkService = new CheckService(pathCalculationService, gameRepository);
        }

        [Theory]
        [InlineData("Qa1", "Kd8", "c1")]
        [InlineData("Qe1", "Kd8", "b4")]
        public void Ignore_When_King_Not_At_Range(
                    string knight, string king, string endPos)
        {
            var gameId = gameService.Load(knight, king);
            var startPos = knight.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.False(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Qa1", "Kd8", "d1")]
        [InlineData("Qa1", "Kd8", "a8")]
        [InlineData("Qh1", "Kd8", "d1")]
        [InlineData("Qh1", "Kd8", "h8")]
        [InlineData("Qa8", "Kd1", "d8")]
        [InlineData("Qc1", "Kd8", "c7")]
        [InlineData("Qe1", "Kd8", "e7")]
        [InlineData("Qe1", "Kd8", "a5")]
        [InlineData("Qa4", "Kd8", "h4")]
        public void Detect_Check_When_Moved_At_Killing_Range(string queen, string king, string endPos)
        {
            var gameId = gameService.Load(queen, king);
            var startPos = queen.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
