using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using System;
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

            gameService.Load("Pd6", "Ke8");
            gameService.TryMove("d6", "d7");
            Assert.True(checkService.IsCheck());
        }
    }
}
