using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Boards;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class RookShould
    {
        private readonly GameService gameService;
        private readonly IPathCalculationService pathCalculationService;
        private readonly IBoardService boardService;
        private readonly CheckService checkService;

        public RookShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository); 
            pathCalculationService = new PathCalculationService();
            boardService = new BoardService();
            checkService = new CheckService(boardService, pathCalculationService, gameRepository);
        }

        [Theory]
        [InlineData("Rc8", "Kd8")]
        [InlineData("Re8", "Kd8")]
        [InlineData("Rd5", "Kd8")]
        [InlineData("Rd7", "Kd8")]
        [InlineData("Rd1", "Kd8")]
        [InlineData("Rd8", "Kd7")]
        [InlineData("Rd8", "Kd1")]
        public void Detect_Check_When_King_At_Killing_Range(string rook, string king)
        {

            var gameId = gameService.Load(rook, king);
            Assert.True(checkService.IsCheck(gameId));
        }


        [Theory]
        [InlineData("Rc7", "Kd8")]
        [InlineData("Re7", "Kd8")]
        [InlineData("Ra5", "Kd8")]
        [InlineData("Rh4", "Kd8")]
        public void Ignore_When_King_Not_At_Range(string rook, string king)
        {

            var gameId = gameService.Load(rook, king);
            Assert.False(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Ra1", "Kd8", "d1")]
        [InlineData("Ra1", "Kd8", "a8")]
        [InlineData("Rh1", "Kd8", "d1")]
        [InlineData("Rh1", "Kd8", "h8")]
        [InlineData("Ra8", "Kd1", "d8")]
        public void Detect_Check_When_Moved_At_Killing_Range(string rook, string king, string endPos)
        {
            var gameId = gameService.Load(rook, king);
            var startPos = rook.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Ra1", "Nd7", "Kd8", "d1")]
        [InlineData("Ra1", "Nc8", "Kd8", "a8")]
        [InlineData("Rh1", "Ne8", "Kd8", "h8")]
        public void Ignore_Check_When_Rook_To_King_Is_Blocked(string rook, string knight, string king, string endPos)
        {

            var gameId = gameService.Load(rook, knight, king);
            var startPos = rook.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.False(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Rh2", "Rb1", "Ka8", "a2")]
        public void Detect_Checkmate_When_Moved_At_Killing_Range(string kingRook, string queenRook, string king, string endPos)
        {
            var gameId = gameService.Load(kingRook, queenRook, king);
            var startPos = kingRook.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheckmate(gameId));
        }
    }
}
