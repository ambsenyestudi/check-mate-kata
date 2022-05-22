using Checkmate.Detector.Domain.Pieces;
using Checkmate.Detector.Domain.Positions;
using System;
using System.Collections.Generic;

namespace Checkmate.Detector.Domain.Boards
{
    public class BoardService : IBoardService
    {
        public IEnumerable<Position> GetPossibleMoves(Piece piece)
        {
            throw new NotImplementedException();
        }
    }
}
