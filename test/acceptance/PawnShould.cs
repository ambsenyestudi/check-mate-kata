using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class PawnShould
    {
        private readonly GameService gameService;
        private readonly CheckService checkService;
        public PawnShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository);
            checkService = new CheckService(gameRepository);
        }

        [Theory]
        [InlineData("Pc7", "Kd8")]
        [InlineData("Pe7", "Kd8")]
        public void Detect_Checkmate_When_King_At_Killing_Range(string pawn, string king)
        {

            var gameId = gameService.Load(pawn, king);
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
