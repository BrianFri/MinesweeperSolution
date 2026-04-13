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

        // ====================== NEW FLOODFILL TESTS (Milestone 3) ======================

        [Fact]
        public void FloodFill_OnZeroCell_RevealsLargeConnectedArea()
        {
            BoardModel board = new BoardModel(5);
            BoardLogic logic = new BoardLogic();

            // One bomb in the corner → most of the board is a big connected safe area
            board.Cells[4, 4].IsBomb = true;

            logic.CountBombsNearby(board);

            // Flood fill from a zero cell
            logic.FloodFill(board, 0, 0);

            // Count visited safe cells
            int visitedCount = 0;
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                    if (board.Cells[r, c].IsVisited)
                        visitedCount++;

            // Should reveal ALL 24 safe cells (recursion spreads through zeros + bordering numbers)
            Assert.Equal(24, visitedCount);
        }

        [Fact]
        public void FloodFill_OnNumberedCell_RevealsOnlyThatSingleCell()
        {
            BoardModel board = new BoardModel(5);
            BoardLogic logic = new BoardLogic();

            // Make cell (2,2) have 2 bomb neighbors
            board.Cells[1, 1].IsBomb = true;
            board.Cells[1, 3].IsBomb = true;

            logic.CountBombsNearby(board);

            Assert.Equal(2, board.Cells[2, 2].NumberOfBombNeighbors);

            // Flood fill directly on the numbered cell
            logic.FloodFill(board, 2, 2);

            // Only this one cell should be visited
            int visitedCount = 0;
            for (int r = 0; r < 5; r++)
                for (int c = 0; c < 5; c++)
                    if (board.Cells[r, c].IsVisited)
                        visitedCount++;

            Assert.Equal(1, visitedCount);
            Assert.True(board.Cells[2, 2].IsVisited);
        }

        [Fact]
        public void FloodFill_DoesNotRevealBombsOrFlaggedCells()
        {
            BoardModel board = new BoardModel(5);
            BoardLogic logic = new BoardLogic();

            board.Cells[2, 2].IsBomb = true;
            board.Cells[1, 1].IsFlagged = true;   // safe but flagged

            logic.CountBombsNearby(board);

            logic.FloodFill(board, 0, 0);

            Assert.False(board.Cells[2, 2].IsVisited);   // bomb stays hidden
            Assert.False(board.Cells[1, 1].IsVisited);   // flagged cell stays hidden
        }

        [Fact]
        public void FloodFill_CollectsRewardWhenRevealedInFloodArea()
        {
            BoardModel board = new BoardModel(5);
            BoardLogic logic = new BoardLogic();

            // Simple safe board
            logic.CountBombsNearby(board);

            // Manually place reward in the middle (will be reached by flood fill)
            board.Cells[2, 2].HasSpecialReward = true;
            board.RewardsRemaining = 1;

            logic.FloodFill(board, 0, 0);

            Assert.True(board.Cells[2, 2].IsVisited);
            Assert.False(board.Cells[2, 2].HasSpecialReward);  // reward was collected
            Assert.Equal(1, board.RewardsRemaining);
        }
    }
}