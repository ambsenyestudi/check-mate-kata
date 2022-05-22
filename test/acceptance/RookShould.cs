using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class RookShould
    {
        private readonly GameService gameService;
        private readonly CheckService checkService;

        public RookShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository);
            checkService = new CheckService(gameRepository);
        }

        [Theory]
        [InlineData("Rc8", "Kd8")]
        [InlineData("Re8", "Kd8")]
        [InlineData("Rd5", "Kd8")]
        [InlineData("Rd7", "Kd8")]
        [InlineData("Rd1", "Kd8")]
        [InlineData("Rd8", "Kd7")]
        [InlineData("Rd8", "Kd1")]
        public void Detect_Checkmate_When_King_At_Killing_Range(string pawn, string king)
        {

            var gameId = gameService.Load(pawn, king);
            Assert.True(checkService.IsCheck(gameId));
        }


    }
}
