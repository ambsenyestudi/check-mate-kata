using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using Moq;
using System;
using Xunit;

namespace Checkmate.Detector.Unit.Test
{
    public class GameServiceShould
    {
        private readonly GameService gameService;
        private readonly GameLayout GAME_LAYOUT;
        private readonly Mock<IGameRepository> gameRepository;
        private readonly GameId GAME_ID = new GameId(new Guid("03c9aedb-d728-4f90-8f8f-80c1348b144a"));
        private readonly string[] pieces = new string[] { "Pd6", "Ke8" };
        public GameServiceShould()
        {
            gameRepository = new Mock<IGameRepository>();
            gameService = new GameService(gameRepository.Object);
            GAME_LAYOUT = new GameLayout(GAME_ID, pieces);
        }

        [Fact]
        public void Load_Game()
        {
            gameRepository.Setup(x => x.Add(It.IsAny<GameLayout>())).Returns(GAME_ID);
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            var gameId = gameService.Load("Pd6", "Ke8");
            gameRepository.Verify(x => x.Add(It.IsAny<GameLayout>()));
            Assert.Equal(GAME_ID, gameId);
        }
        [Fact]
        public void Not_Move_If_Square_Is_Empty()
        {
            Assert.False(gameService.TryMove("a2", "a3"));
        }
    }
}
