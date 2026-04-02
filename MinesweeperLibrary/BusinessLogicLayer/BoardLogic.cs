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
    }
}