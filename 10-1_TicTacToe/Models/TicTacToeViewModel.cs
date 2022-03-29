using System.Collections.Generic;

namespace _10_1_TicTacToe.Models
{
    public class TicTacToeViewModel
    {
        public List<Cell> Cells { get; set; }
        public Cell Selected { get; set; }
        public bool IsGameOver { get; set; }
    }
}
