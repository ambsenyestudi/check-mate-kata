using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Boards;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class QueenShould
    {
        private readonly GameService gameService;
        private readonly IPathCalculationService pathCalculationService;
        private readonly IBoardService boardService;
        private readonly CheckService checkService;
        public QueenShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository); 
            pathCalculationService = new PathCalculationService();
            boardService = new BoardService();
            checkService = new CheckService(boardService, pathCalculationService, gameRepository);
        }

        [Theory]
        [InlineData("Qa1", "Kd8", "c1")]
        [InlineData("Qe1", "Kd8", "b4")]
        public void Ignore_When_King_Not_At_Range(
                    string knight, string king, string endPos)
        {
            var gameId = gameService.Load(knight, king);
            var startPos = knight.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.False(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Qa1", "Kd8", "d1")]
        [InlineData("Qa1", "Kd8", "a8")]
        [InlineData("Qh1", "Kd8", "d1")]
        [InlineData("Qh1", "Kd8", "h8")]
        [InlineData("Qa8", "Kd1", "d8")]
        [InlineData("Qc1", "Kd8", "c7")]
        [InlineData("Qe1", "Kd8", "e7")]
        [InlineData("Qe1", "Kd8", "a5")]
        [InlineData("Qa4", "Kd8", "h4")]
        public void Detect_Check_When_Moved_At_Killing_Range(string queen, string king, string endPos)
        {
            var gameId = gameService.Load(queen, king);
            var startPos = queen.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Qa1", "Nd7", "Kd8", "d1")]
        [InlineData("Qa1", "Nc8", "Kd8", "a8")]
        [InlineData("Qh1", "Ne8", "Kd8", "h8")]
        [InlineData("Qg2", "Nc7", "Kd8", "b6")]
        [InlineData("Qa1", "Ne7", "Kd8", "f6")]
        public void Ignore_Check_When_Queen_To_King_Is_Blocked(string rook, string knight, string king, string endPos)
        {

            var gameId = gameService.Load(rook, knight, king);
            var startPos = rook.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.False(checkService.IsCheck(gameId));
        }
    }
}
