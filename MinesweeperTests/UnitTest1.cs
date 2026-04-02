using Xunit;
using MinesweeperLibrary.Models;
using MinesweeperLibrary.BusinessLogicLayer;

namespace MinesweeperTests
{
    public class BoardLogicTests
    {
        [Fact]
        public void SetupBombs_PlacesCorrectNumberOfBombs()
        {
            BoardModel board = new BoardModel(10);
            BoardLogic logic = new BoardLogic();
            logic.SetupBombs(board, 2);

            int bombCount = 0;
            foreach (CellModel cell in board.Cells)
                if (cell.IsBomb) bombCount++;

            Assert.True(bombCount >= 10 && bombCount <= 25);
        }

        [Fact]
        public void CountBombsNearby_CalculatesCorrectNeighborCount()
        {
            BoardModel board = new BoardModel(5);
            BoardLogic logic = new BoardLogic();

            board.Cells[1, 1].IsBomb = true;
            board.Cells[1, 2].IsBomb = true;

            logic.CountBombsNearby(board);

            Assert.Equal(1, board.Cells[0, 0].NumberOfBombNeighbors);
            Assert.Equal(9, board.Cells[1, 1].NumberOfBombNeighbors);
            Assert.Equal(1, board.Cells[0, 3].NumberOfBombNeighbors);
        }
    }
}