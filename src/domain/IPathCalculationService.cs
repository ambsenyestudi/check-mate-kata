using Checkmate.Detector.Domain.Positions;
using System.Collections.Generic;

namespace Checkmate.Detector.Domain
{
    public interface IPathCalculationService
    {
        IEnumerable<Position> GetPath(Move move);
    }
}
