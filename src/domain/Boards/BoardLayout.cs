using Checkmate.Detector.Domain.Pieces;
using Checkmate.Detector.Domain.Positions;
using System.Linq;

namespace Checkmate.Detector.Domain.Game
{
    public record BoardLayout
    {
        public static BoardLayout Empty { get; } = new BoardLayout(GameId.Empty, PieceCollection.Empty);

        public GameId GameId { get; set; }
        private PieceCollection pieces { get; }

        public BoardLayout(GameId gameId, PieceCollection pieces) {
            GameId = gameId;
            this.pieces = pieces;
        }

        public string[] GetPieces() =>
            pieces.Pieces.Select(x=>x.ToString()).ToArray();

        public BoardLayout(GameId gameId, string[] pieces):this(gameId, new PieceCollection(pieces))
        {            
        }

        public bool IsPieceAt(Position start) =>
            pieces.ContainsPosition(start);

        public Piece GetKing() =>
            pieces.GetPiece(PieceKind.King);

        public BoardLayout Move(Move move) =>
            new BoardLayout(GameId, pieces.MovePieceAt(move));

        public Piece GetPieceAt(Position position) =>
            pieces.GetPieceAt(position);
    }
}