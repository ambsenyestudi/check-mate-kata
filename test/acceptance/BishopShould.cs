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
        [InlineData("Bc7", "Kd8")]
        [InlineData("Be7", "Kd8")]
        [InlineData("Ba5", "Kd8")]
        [InlineData("Bh4", "Kd8")]
        public void Detect_Check_When_King_At_Killing_Range(string bishop, string king)
        {

            var gameId = gameService.Load(bishop, king);
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
