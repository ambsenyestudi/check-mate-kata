using Checkmate.Detector.Domain.Game;
using Moq;
using System;
using Xunit;

namespace Checkmate.Detector.Unit.Test
{
    public class GameServiceShould
    {
        private readonly GameService gameService;
        private readonly BoardLayout GAME_LAYOUT;
        private readonly Mock<IGameRepository> gameRepository;
        private readonly GameId GAME_ID = new GameId(new Guid("03c9aedb-d728-4f90-8f8f-80c1348b144a"));
        private readonly string[] pieces = new string[] { "Pd6", "Ke8" };
        public GameServiceShould()
        {
            GAME_LAYOUT = new BoardLayout(GAME_ID, pieces);
            
            gameRepository = new Mock<IGameRepository>();
            gameRepository.Setup(x => x.Add(It.IsAny<BoardLayout>())).Returns(GAME_ID);
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            
            gameService = new GameService(gameRepository.Object);            
        }

        [Fact]
        public void Load_Game()
        {
            
            var gameId = gameService.Load("Pd6", "Ke8");
            gameRepository.Verify(x => x.Add(It.IsAny<BoardLayout>()));
            Assert.Equal(GAME_ID, gameId);
        }

        [Theory]
        [InlineData("a2","a3")]
        [InlineData("b7", "b8")]
        public void Not_Move_If_Square_Is_Empty(string startPos, string endPos)
        {
            var gameId = gameService.Load("Pd6", "Ke8");
            Assert.False(gameService.TryMove(startPos, endPos, gameId));
        }

        [Fact]
        public void Move_Piece()
        {
            var expected = new BoardLayout(GAME_ID, new string[] { "Pd7", "Ke8" });

            var gameId = gameService.Load("Pd6", "Ke8");
            
            Assert.True(gameService.TryMove("d6", "d7", gameId));
            gameRepository.Verify(x => x.Replace(expected));
        }


    }
}
