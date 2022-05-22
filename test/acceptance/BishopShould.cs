using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class BishopShould
    {
        private readonly GameService gameService;
        private readonly CheckService checkService;
        public BishopShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository);
            checkService = new CheckService(gameRepository);
        }

        [Theory]
        [InlineData("Bh2", "Kd8", "c7")]
        [InlineData("Bh4", "Kd8", "e7")]
        [InlineData("Be1", "Kd8", "a5" )]
        [InlineData("Be1", "Kd8", "h4")]
        public void Detect_Check_When_King_At_Killing_Range(string bishop, string king, string endPos)
        {

            var gameId = gameService.Load(bishop, king);
            var startPos = bishop.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
