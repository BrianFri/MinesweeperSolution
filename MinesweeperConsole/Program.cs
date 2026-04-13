using System;
using MinesweeperLibrary.Models;
using MinesweeperLibrary.BusinessLogicLayer;

namespace MinesweeperConsole
{
    class Program
    {
        /// <summary>
        /// Main
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, welcome to Minesweeper\n");

            IBoardLogic logic = new BoardLogic();

            BoardModel board = new BoardModel(10);
            logic.SetupBombs(board, 2);          
            logic.CountBombsNearby(board);
            logic.PlaceReward(board);            

            PrintAnswers(board);

            Console.WriteLine("Game started! Good luck!\n");

            while (board.GameState == GameState.InProgress)
            {
                PrintBoard(board);

                Console.Write("Enter the row number: ");
                int row = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter the column number: ");
                int col = int.Parse(Console.ReadLine() ?? "0");

                Console.Write("Enter 1 to visit the cell, 2 to flag the cell, 3 to use a reward (bomb detector): ");
                int choice = int.Parse(Console.ReadLine() ?? "0");

                CellModel cell = board.Cells[row, col];

                if (choice == 1)  // Visit the cell
                {
                    if (cell.IsFlagged)
                    {
                        Console.WriteLine("That cell is flagged. Unflag it first if you want to visit.");
                        continue;
                    }
                    if (cell.IsBomb)
                    {
                        cell.IsVisited = true;   // reveal the bomb for the loss screen
                    }
                    else
                    {
                        logic.FloodFill(board, row, col);   // recursive flood fill
                    }
                }
                else if (choice == 2) // Flag / Unflag
                {
                    cell.IsFlagged = !cell.IsFlagged;
                    Console.WriteLine(cell.IsFlagged ? "Cell flagged." : "Cell unflagged.");
                }
                else if (choice == 3) // Use reward
                {
                    if (board.RewardsRemaining > 0)
                    {
                        Console.WriteLine($"Is it a bomb? {cell.IsBomb}");
                        board.RewardsRemaining = 0;
                    }
                    else
                    {
                        Console.WriteLine("You don't have a reward yet.");
                    }
                }

                // Update game state after every move
                board.GameState = logic.DetermineGameState(board);
            }

            // Game over
            PrintBoard(board);
            if (board.GameState == GameState.Won)
                Console.WriteLine("Congratulations! You won!");
            else
                Console.WriteLine("Boom! You lost.");
        }

        /// <summary>
        /// Prints the board
        /// </summary>
        /// <param name="board"></param>
        private static void PrintBoard(BoardModel board)
        {
            Console.WriteLine("\nHere is the current board");
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

                    if (cell.IsFlagged)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write(" F ");
                        Console.ResetColor();
                    }
                    else if (!cell.IsVisited)
                    {
                        Console.Write(" ? ");
                    }
                    else if (cell.HasSpecialReward)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" r ");
                        Console.ResetColor();
                    }
                    else if (cell.IsBomb && cell.IsVisited)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(" B ");
                        Console.ResetColor();
                    }
                    else if (cell.NumberOfBombNeighbors == 0)
                    {
                        Console.Write(" . ");
                    }
                    else
                    {
                        SetColor(cell.NumberOfBombNeighbors);
                        Console.Write($" {cell.NumberOfBombNeighbors} ");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Prints the Answer
        /// </summary>
        /// <param name="board"></param>
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
                    else if (cell.HasSpecialReward)
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.Write(" r ");
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
            Console.WriteLine();
        }

        /// <summary>
        /// Sets Color
        /// </summary>
        /// <param name="num"></param>
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