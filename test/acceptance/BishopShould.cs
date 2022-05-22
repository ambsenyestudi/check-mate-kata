using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Boards;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class BishopShould
    {
        private readonly GameService gameService;
        private readonly IPathCalculationService pathCalculationService;
        private readonly IBoardService boardService;
        private readonly CheckService checkService;
        public BishopShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository); 
            pathCalculationService = new PathCalculationService();
            boardService = new BoardService();
            checkService = new CheckService(boardService, pathCalculationService, gameRepository);
        }

        [Theory]
        [InlineData("Bh2", "Kd8", "c7")]
        [InlineData("Bh4", "Kd8", "e7")]
        [InlineData("Be1", "Kd8", "a5" )]
        [InlineData("Be1", "Kd8", "h4")]
        public void Detect_Check_When_King_At_Killing_Range(string bishop, string king, string endPos)
        {

            var gameId = gameService.Load(bishop, king);
            var startPos = bishop.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.True(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Bg2", "Nc7", "Kd8", "b6")]
        [InlineData("Ba1", "Ne7", "Kd8", "f6")]
        public void Ignore_Check_When_Bishop_To_King_Is_Blocked(string bishop, string knight, string king, string endPos)
        {

            var gameId = gameService.Load(bishop, knight, king);
            var startPos = bishop.Substring(1, 2);
            gameService.TryMove(startPos, endPos, gameId);
            Assert.False(checkService.IsCheck(gameId));
        }
    }
}
