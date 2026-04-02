using System;

namespace MinesweeperLibrary.Models
{
    public class BoardModel
    {
        public int Size { get; private set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public CellModel[,] Cells { get; private set; }
        public int Difficulty { get; set; }
        public int RewardsRemaining { get; set; } = 0;
        public GameState GameState { get; set; } = GameState.InProgress;

        public BoardModel(int size)
        {
            Size = size;
            Cells = new CellModel[size, size];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    Cells[i, j] = new CellModel(i, j);
                }
            }
        }
    }
}