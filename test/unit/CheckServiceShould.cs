using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Boards;
using Checkmate.Detector.Domain.Game;
using Checkmate.Detector.Domain.Pieces;
using Checkmate.Detector.Domain.Positions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Checkmate.Detector.Unit.Test
{
    public class CheckServiceShould
    {
        private readonly CheckService checkService;
        private readonly Mock<IGameRepository> gameRepository;
        private readonly Mock<IPathCalculationService> pathCalculationService;
        private readonly Mock<IBoardService> boardService;
        private GameLayout GAME_LAYOUT;
        private readonly GameId GAME_ID = new GameId(new Guid("03c9aedb-d728-4f90-8f8f-80c1348b144a"));
        private readonly string[] NO_KILLING_PIECES = new string[] { "Pd6", "Ke8" };

        public CheckServiceShould()
        {
            GAME_LAYOUT = new GameLayout(GAME_ID, NO_KILLING_PIECES);

            gameRepository = new Mock<IGameRepository>();
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            pathCalculationService = new Mock<IPathCalculationService>();
            boardService = new Mock<IBoardService>();
            checkService = new CheckService(boardService.Object, pathCalculationService.Object, gameRepository.Object);
        }

        [Fact]
        public void Ignore_When_Not_At_Kill_Distnace_Of_King()
        {
            Assert.False(checkService.IsCheck(GAME_ID));
        }

        [Theory]
        [InlineData("Pd7", "Ke8")]
        public void Tell_When_Piece_Checks_King(string piece, string king)
        {
            GAME_LAYOUT = new GameLayout(GAME_ID, new string[] { piece, king });
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            Assert.True(checkService.IsCheck(GAME_ID));
        }

        [Theory]
        [InlineData("Nb7", "Kd8")]
        [InlineData("Nf7", "Kd8")]
        [InlineData("Nb8", "Kd7")]
        [InlineData("Nf8", "Kd7")]
        [InlineData("Nc6", "Kd8")]
        [InlineData("Ne6", "Kd8")]
        [InlineData("Nc8", "Kd6")]
        [InlineData("Ne8", "Kd6")]
        public void Tell_When_Knight_Checks_King(string knight, string king)
        {
            GAME_LAYOUT = new GameLayout(GAME_ID, new string[] { knight, king });
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            Assert.True(checkService.IsCheck(GAME_ID));
        }

        [Theory]
        [InlineData("Bb6", "Nc7", "Kd8")]
        [InlineData("Bf6", "Ne7", "Kd8")]
        public void Ignore_Check_When_Bishop_To_King_Is_Blocked(string piece, string blockingPiece, string king)
        {
            
            GAME_LAYOUT = new GameLayout(GAME_ID, new string[] { piece, blockingPiece, king });
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            pathCalculationService.Setup(x => x.GetPath(It.IsAny<Move>()))
                .Returns(PositionFromPiece(blockingPiece));
            Assert.False(checkService.IsCheck(GAME_ID));
        }

        private IEnumerable<Position> PositionFromPiece(string blockingPiece) =>
            new List<Position>
            {
                Position.FromString(blockingPiece.Substring(1,2))
            };

        [Theory]
        [InlineData("Ra2", "Rb1", "Ka8")]
        public void Detect_Checkmate_When_At_Killing_Range(string kingRook, string queenRook, string king)
        {
            GAME_LAYOUT = new GameLayout(GAME_ID, new string[] { kingRook, queenRook, king });
            gameRepository.Setup(x => x.GetBy(GAME_ID)).Returns(GAME_LAYOUT);
            boardService.Setup(x => x.GetPossibleMoves(Piece.FromString(king)))
                .Returns(
                new List<Position> 
                { 
                    Position.FromString("B8"), 
                    Position.FromString("B7"), 
                    Position.FromString("A7") });
            Assert.True(checkService.IsCheckmate(GAME_ID));
        }
    }
}
