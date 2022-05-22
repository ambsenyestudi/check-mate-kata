using Checkmate.Detector.Domain;
using Checkmate.Detector.Domain.Boards;
using Checkmate.Detector.Domain.Game;
using Xunit;

namespace Checkmate.Detector.Acceptance.Test
{
    public class PawnShould
    {
        private readonly GameService gameService;
        private readonly IPathCalculationService pathCalculationService;
        private readonly IBoardService boardService;
        private readonly CheckService checkService;
        public PawnShould()
        {
            var gameRepository = new GameRepository();
            gameService = new GameService(gameRepository); 
            pathCalculationService = new PathCalculationService();
            boardService = new BoardService();
            checkService = new CheckService(boardService, pathCalculationService, gameRepository);
        }

        [Theory]
        [InlineData("Pc7", "Kd8")]
        [InlineData("Pe7", "Kd8")]
        public void Detect_Check_When_King_At_Killing_Range(string pawn, string king)
        {

            var gameId = gameService.Load(pawn, king);
            Assert.True(checkService.IsCheck(gameId));
        }

        [Theory]
        [InlineData("Pd7", "Kd8")]
        [InlineData("Pe7", "Kd6")]
        [InlineData("Pc6", "Kd6")]
        public void Ignore_When_King_Not_At_Range(string pawn, string king)
        {

            var gameId = gameService.Load(pawn, king);
            Assert.False(checkService.IsCheck(gameId));
        }
    }
}
