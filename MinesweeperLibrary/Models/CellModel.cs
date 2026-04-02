using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary.Models
{
    public class CellModel
    {
        public int Row { get; set; } = -1;
        public int Column { get; set; } = -1;
        public bool IsVisited { get; set; } = false;
        public bool IsBomb { get; set; } = false;
        public bool IsFlagged { get; set; } = false;
        public int NumberOfBombNeighbors { get; set; } = 0;
        public bool HasSpecialReward { get; set; } = false;

        public CellModel() { }

        public CellModel(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
