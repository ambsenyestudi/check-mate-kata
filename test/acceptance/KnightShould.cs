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

        //todo refactor knight is N

        [Theory]
        [InlineData("Kc5", "Kd8", "b7")]
        [InlineData("Kg5", "Kd8", "f7")]
        [InlineData("Ka6", "Kd7", "b8")]
        [InlineData("Kg7", "Kd7", "f8")]
        [InlineData("Kb8", "Kd8", "c6")]
        [InlineData("Kf7", "Kd8", "e6")]
        [InlineData("Ka7", "Kd6", "c8")]
        [InlineData("Kf6", "Kd6", "e8")]
        public void Detect_Check_When_Moved_At_Killing_Range(string rook, string king, string endPos)
        {
            var gameId = gameService.Load(rook, king);
            var startPos = rook.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
