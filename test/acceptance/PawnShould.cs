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

        [Fact]
        public void Detect_Checkmate_When_King_At_Killing_Range()
        {

            var gameId = gameService.Load("Pd7", "Ke8");
            Assert.True(checkService.IsCheck(gameId));
        }
    }
}
