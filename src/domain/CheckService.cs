﻿using Checkmate.Detector.Domain.Game;
using System;

namespace Checkmate.Detector.Domain
{
    public class CheckService
    {
        private IGameRepository gameRepository;

        public CheckService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public bool IsCheck()
        {
            throw new NotImplementedException();
        }
    }
}
