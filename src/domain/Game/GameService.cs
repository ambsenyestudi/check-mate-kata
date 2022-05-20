using Checkmate.Detector.Domain.Game;
using System;

namespace Checkmate.Detector.Domain
{
    public class GameService
    {
        private readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public bool IsLoaded { get; set; }

        public void Load(string v1, string v2)
        {
            throw new NotImplementedException();
        }

        public void Move(string v1, string v2)
        {
            throw new NotImplementedException();
        }
    }
}
