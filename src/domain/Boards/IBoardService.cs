using Checkmate.Detector.Domain.Pieces;
using Checkmate.Detector.Domain.Positions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkmate.Detector.Domain.Boards
{
    public interface IBoardService
    {
        public IEnumerable<Position> GetPossibleMoves(Piece piece);
    }
}
