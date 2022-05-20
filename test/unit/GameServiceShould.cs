using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Game;
using System;
using Xunit;

namespace Checkmate.Detector.Unit.Test
{
    public class GameServiceShould
    {
        private readonly GameService gameService;
        private readonly IGameRepository gameRepository;

        public GameServiceShould()
        {
            gameService = new GameService(gameRepository);
        }

        [Fact]
        public void LoadGame()
        {
            gameService.Load("Pd6", "Ke8");

            Assert.True(gameService.IsLoaded);
        }
    }
}
