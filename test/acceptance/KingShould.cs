using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class KingShould
    {
        private readonly GameService gameService;
        private readonly CheckService checkService;
        public KingShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository);
            checkService = new CheckService(gameRepository);
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
    }
}
