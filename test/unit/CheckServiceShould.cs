using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Moq;
using System;
using Xunit;

namespace Checkmate.Detector.Unit.Test
{
    public class CheckServiceShould
    {
        private readonly CheckService checkService;
        private readonly Mock<IGameRepository> gameRepository;
        private GameLayout GAME_LAYOUT;
        private readonly GameId GAME_ID = new GameId(new Guid("03c9aedb-d728-4f90-8f8f-80c1348b144a"));
        private readonly string[] NO_KILLING_PIECES = new string[] { "Pd6", "Ke8" };

        public CheckServiceShould()
        {
            GAME_LAYOUT = new GameLayout(GAME_ID, NO_KILLING_PIECES);

            gameRepository = new Mock<IGameRepository>();
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);

            checkService = new CheckService(gameRepository.Object);
        }

        [Fact]
        public void Ignore_When_Not_At_Kill_Distnace_Of_King()
        {
            Assert.False(checkService.IsCheck(GAME_ID));
        }

        [Theory]
        [InlineData("Pd7", "Ke8")]
        public void Tell_When_Piece_Checks_King(string piece, string king)
        {
            GAME_LAYOUT = new GameLayout(GAME_ID, new string[] { piece, king });
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            Assert.True(checkService.IsCheck(GAME_ID));
        }

        [Theory]
        [InlineData("Kb7", "Kd8")]
        [InlineData("Kf7", "Kd8")]
        [InlineData("Kb8", "Kd7")]
        [InlineData("Kf8", "Kd7")]
        [InlineData("Kc6", "Kd8")]
        [InlineData("Ke6", "Kd8")]
        [InlineData("Kc8", "Kd6")]
        [InlineData("Ke8", "Kd6")]
        public void Tell_When_Knight_Checks_King(string knight, string king)
        {
            GAME_LAYOUT = new GameLayout(GAME_ID, new string[] { knight, king });
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            Assert.True(checkService.IsCheck(GAME_ID));
        }
    }
}
