using System;
using System.Collections.Generic;
using System.Text;

namespace MinesweeperLibrary.Models
{
    public class GameStat
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Score { get; set; }
        public DateTime GameTime { get; set; }
    }
}
