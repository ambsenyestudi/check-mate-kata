namespace Checkmate.Detector.Domain.Positions
{
    public record Move
    {
        public Position Start { get; }
        public Position End { get; }
        
        public Move(Position start, Position end) => (Start, End) = (start, end);
        
        public Move(string startPosition, string endPosition) : this(Position.FromString(startPosition), Position.FromString(endPosition))
        {
        }


    }
}
