using System;
using MinesweeperLibrary.Models;
using MinesweeperLibrary.BusinessLogicLayer;

namespace MinesweeperConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to Minesweeper\n");

            IBoardLogic logic = new BoardLogic();

            // Board 1 – size 10
            BoardModel board1 = new BoardModel(10);
            logic.SetupBombs(board1, 2);
            logic.CountBombsNearby(board1);
            PrintAnswers(board1);

            // Board 2 – size 15
            BoardModel board2 = new BoardModel(15);
            logic.SetupBombs(board2, 3);
            logic.CountBombsNearby(board2);
            PrintAnswers(board2);
        }

        private static void PrintAnswers(BoardModel board)
        {
            // Column headers
            Console.Write("   ");
            for (int c = 0; c < board.Size; c++)
                Console.Write($"{c,2} ");
            Console.WriteLine();

            for (int r = 0; r < board.Size; r++)
            {
                Console.Write($"{r,2} ");
                for (int c = 0; c < board.Size; c++)
                {
                    CellModel cell = board.Cells[r, c];

                    if (cell.IsBomb)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" B ");
                    }
                    else if (cell.NumberOfBombNeighbors == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Gray;
                        Console.Write(" . ");
                    }
                    else
                    {
                        SetColor(cell.NumberOfBombNeighbors);
                        Console.Write($" {cell.NumberOfBombNeighbors} ");
                    }
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
            Console.ResetColor();
        }

        private static void SetColor(int num)
        {
            switch (num)
            {
                case 1: Console.ForegroundColor = ConsoleColor.Blue; break;
                case 2: Console.ForegroundColor = ConsoleColor.Green; break;
                case 3: Console.ForegroundColor = ConsoleColor.Red; break;
                case 4: Console.ForegroundColor = ConsoleColor.DarkCyan; break;
                default: Console.ForegroundColor = ConsoleColor.Yellow; break;
            }
        }
    }
}