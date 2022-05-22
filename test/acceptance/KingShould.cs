using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class KingShould
    {
        private readonly GameService gameService;
        private readonly CheckService checkService;
        private readonly IPathCalculationService pathCalculationService;

        public KingShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository);
            pathCalculationService = new PathCalculationService();
            checkService = new CheckService(pathCalculationService, gameRepository);
        }

        [Theory]
        [InlineData("Kd8", "Pf7", "e8")]
        [InlineData("Kd8", "Pb7", "c8")]
        public void Detect_Check_When_Moved_At_Pawn_Range(string king, string pawn, string endPos)
        {
            var gameId = gameService.Load(pawn, king);
            var startPos = king.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Kd8", "Bf7", "e8")]
        [InlineData("Kd8", "Bb7", "c8")]
        [InlineData("Kd8", "Bh3", "c8")]
        [InlineData("Kd8", "Ba4", "e8")]
        public void Detect_Check_When_Moved_At_Bishop_Range(string king, string bishop, string endPos)
        {
            var gameId = gameService.Load(bishop, king);
            var startPos = king.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
