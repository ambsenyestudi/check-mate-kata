﻿using Checkmate.Detector.Domain.Pieces;
using Checkmate.Detector.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkmate.Detector.Domain.Game
{
    public record GameLayout
    {
        public static GameLayout Empty { get; } = new GameLayout(GameId.Empty, PieceCollection.Empty);

        public GameId GameId { get; set; }
        private PieceCollection pieces { get; }

        public GameLayout(GameId gameId, PieceCollection pieces) {
            GameId = gameId;
            this.pieces = pieces;
        }

        public string[] GetPieces() =>
            pieces.Pieces.Select(x=>x.ToString()).ToArray();

        public GameLayout(GameId gameId, string[] pieces):this(gameId, new PieceCollection(pieces))
        {            
        }

        public bool IsPieceAt(string startPosition) =>
            pieces.ContainsPosition(startPosition);

        public GameLayout Move(Move move) =>
            new GameLayout(GameId, pieces.MovePieceAt(move));
    }
}