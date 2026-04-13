using System;
using MinesweeperLibrary.Models;

namespace MinesweeperLibrary.BusinessLogicLayer
{
    public class BoardLogic : BaseBoardLogic, IBoardLogic
    {
        private readonly Random _random = new Random();

        public void SetupBombs(BoardModel board, int difficultyLevel)
        {
            board.StartTime = DateTime.Now;
            board.Difficulty = difficultyLevel;

            int numBombs = (int)(board.Size * board.Size * (0.08 + difficultyLevel * 0.05));
            int placed = 0;

            while (placed < numBombs)
            {
                int r = _random.Next(board.Size);
                int c = _random.Next(board.Size);
                if (!board.Cells[r, c].IsBomb)
                {
                    board.Cells[r, c].IsBomb = true;
                    placed++;
                }
            }

            PerformPostSetupActions(board);
        }

        public void CountBombsNearby(BoardModel board)
        {
            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    CellModel cell = board.Cells[i, j];
                    if (cell.IsBomb)
                    {
                        cell.NumberOfBombNeighbors = 9;
                        continue;
                    }
                    cell.NumberOfBombNeighbors = GetNeighborCount(board, i, j);
                }
            }
        }

        private int GetNeighborCount(BoardModel board, int row, int col)
        {
            int count = 0;
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i >= 0 && i < board.Size && j >= 0 && j < board.Size && !(i == row && j == col) && board.Cells[i, j].IsBomb)
                        count++;
                }
            }
            return count;
        }

        public override void PerformPostSetupActions(BoardModel board)
        {
            Console.WriteLine($"\nBoardLogic: Post-setup actions completed for a {board.Size}x{board.Size} board with difficulty {board.Difficulty}\n");
        }

        public void PlaceReward(BoardModel board)
        {
            List<(int row, int col)> safeCells = new List<(int row, int col)>();

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    if (!board.Cells[i, j].IsBomb)
                        safeCells.Add((i, j));
                }
            }

            if (safeCells.Count > 0)
            {
                var randomIndex = _random.Next(safeCells.Count);
                var (r, c) = safeCells[randomIndex];
                board.Cells[r, c].HasSpecialReward = true;
                board.RewardsRemaining = 1;
                Console.WriteLine($"Reward placed at cell {r},{c}");
            }
        }

        // RECURSIVE FLOOD FILL
        public void FloodFill(BoardModel board, int row, int col)
        {
            // out of bounds
            if (row < 0 || row >= board.Size || col < 0 || col >= board.Size)
                return;

            CellModel cell = board.Cells[row, col];

            // already visited, flagged, or bomb → stop
            if (cell.IsVisited || cell.IsFlagged || cell.IsBomb)
                return;

            // Reveal this cell
            cell.IsVisited = true;

            // If this cell contains the special reward, give it to the player
            if (cell.HasSpecialReward)
            {
                Console.WriteLine("You found a reward! You can now use the bomb detector once.");
                cell.HasSpecialReward = false;
                board.RewardsRemaining = 1;
            }

            // If the cell has neighboring bombs, stop recursion here (standard Minesweeper behavior)
            if (cell.NumberOfBombNeighbors > 0)
                return;

            // Recursively reveal all 8 neighbors (this is the recursive part)
            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i == row && j == col) continue;
                    FloodFill(board, i, j);
                }
            }
        }


        public GameState DetermineGameState(BoardModel board)
        {
            bool lost = false;
            int totalSafeCells = 0;
            int visitedSafeCells = 0;

            for (int i = 0; i < board.Size; i++)
            {
                for (int j = 0; j < board.Size; j++)
                {
                    CellModel cell = board.Cells[i, j];

                    if (cell.IsVisited && cell.IsBomb)
                        lost = true;

                    if (!cell.IsBomb)
                    {
                        totalSafeCells++;
                        if (cell.IsVisited)
                            visitedSafeCells++;
                    }
                }
            }

            if (lost)
                return GameState.Lost;

            if (visitedSafeCells == totalSafeCells)
                return GameState.Won;

            return GameState.InProgress;
        }
    }
}