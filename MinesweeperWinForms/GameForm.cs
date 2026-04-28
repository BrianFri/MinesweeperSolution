using System;
using System.Drawing;
using System.Windows.Forms;
using MinesweeperLibrary.Models;
using MinesweeperLibrary.BusinessLogicLayer;

namespace MinesweeperWinForms
{
    public partial class GameForm : Form
    {
        private readonly BoardModel board;
        private readonly BoardLogic logic = new BoardLogic();
        private Button[,]? buttons;
        private System.Windows.Forms.Timer gameTimer;
        private DateTime gameStartTime;

        public GameForm(int size, int difficultyLevel)
        {
            InitializeComponent();

            board = new BoardModel(size);
            logic.SetupBombs(board, difficultyLevel);
            logic.CountBombsNearby(board);
            logic.PlaceReward(board);

            gameStartTime = DateTime.Now;
            board.StartTime = gameStartTime;

            CreateGrid();
            SetupTimer();
            UpdateAllButtons();

            SetupFormLayout();
        }

        /// <summary>
        /// Creates the dynamic grid of clickable buttons on the panel.
        /// </summary>
        private void CreateGrid()
        {
            pnlGrid.Controls.Clear();
            int btnSize = 52;
            pnlGrid.Size = new Size(board.Size * btnSize, board.Size * btnSize);

            buttons = new Button[board.Size, board.Size];

            for (int r = 0; r < board.Size; r++)
            {
                for (int c = 0; c < board.Size; c++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(btnSize, btnSize),
                        Location = new Point(c * btnSize, r * btnSize),
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        Tag = Tuple.Create(r, c)
                    };
                    btn.MouseDown += Button_MouseDown;
                    pnlGrid.Controls.Add(btn);
                    buttons[r, c] = btn;
                }
            }
        }

        /// <summary>
        /// Sets up a timer that updates the elapsed time and live score every second.
        /// </summary>
        private void SetupTimer()
        {
            gameTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            gameTimer.Tick += (s, e) =>
            {
                TimeSpan elapsed = DateTime.Now - gameStartTime;
                lblStartTime.Text = $"Start Time: {elapsed:mm\\:ss}";
                lblScore.Text = $"Score: {CalculateCurrentScore()}";
            };
            gameTimer.Start();
        }

        /// <summary>
        /// Updates game display
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">Mouse event arguments (Left or Right click)</param>
        private void Button_MouseDown(object sender, MouseEventArgs e)
        {
            if (board.GameState != GameState.InProgress) return;
            if (buttons == null) return;

            Button btn = (Button)sender;

            if (btn.Tag is Tuple<int, int> tag)
            {
                int row = tag.Item1;
                int col = tag.Item2;

                CellModel cell = board.Cells[row, col];

                if (e.Button == MouseButtons.Left)
                {
                    if (cell.IsFlagged) return;
                    if (cell.IsBomb)
                    {
                        cell.IsVisited = true;
                        board.GameState = GameState.Lost;
                    }
                    else
                    {
                        logic.FloodFill(board, row, col);
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    cell.IsFlagged = !cell.IsFlagged;
                }

                board.GameState = logic.DetermineGameState(board);
                UpdateAllButtons();

                lblScore.Text = $"Score: {CalculateCurrentScore()}";

                if (board.GameState != GameState.InProgress)
                {
                    gameTimer.Stop();
                    HandleGameOver();
                }
            }
        }

        /// <summary>
        /// Updates the appearance of every button on the board.
        /// </summary>
        private void UpdateAllButtons()
        {
            if (buttons == null) return;

            for (int r = 0; r < board.Size; r++)
                for (int c = 0; c < board.Size; c++)
                    UpdateSingleButton(r, c);
        }

        /// <summary>
        /// Updates the text, color, and enabled state of a single button.
        /// </summary>
        /// <param name="r">Row index of the cell</param>
        /// <param name="c">Column index of the cell</param>
        private void UpdateSingleButton(int r, int c)
        {
            if (buttons == null) return;

            Button btn = buttons[r, c];
            CellModel cell = board.Cells[r, c];


            btn.BackgroundImage = Properties.Resources.Tile_Flat;
            btn.BackgroundImageLayout = ImageLayout.Stretch;
            btn.Text = "";

            if (cell.IsFlagged)
            {
                btn.Text = "F";                                      
                btn.BackgroundImage = Properties.Resources.Tile_Flat;
            }
            else if (!cell.IsVisited)
            {
                btn.BackgroundImage = Properties.Resources.Tile_Flat;
            }
            else if (cell.IsBomb)
            {
                btn.BackgroundImage = Properties.Resources.Skull;
            }
            else if (cell.HasSpecialReward)
            {
                btn.BackgroundImage = Properties.Resources.Gold;     
            }
            else if (cell.NumberOfBombNeighbors > 0)
            {
                // Uses "Number 1", "Number 2", etc.
                string resourceName = "Number " + cell.NumberOfBombNeighbors;
                btn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(resourceName);
            }
            else
            {
                // Blank revealed safe cell (using one of your revealed tiles)
                btn.BackgroundImage = Properties.Resources.Tile_1;
            }

            btn.Enabled = !cell.IsVisited && !cell.IsFlagged;
        }

        /// <summary>
        /// Calculates the player's current score.
        /// </summary>
        /// <returns>Current score</returns>
        private int CalculateCurrentScore()
        {
            // Count how many safe cells have been revealed
            int revealedSafeCells = 0;
            for (int r = 0; r < board.Size; r++)
            {
                for (int c = 0; c < board.Size; c++)
                {
                    CellModel cell = board.Cells[r, c];
                    if (cell.IsVisited && !cell.IsBomb)
                        revealedSafeCells++;
                }
            }

            // Points per revealed safe cell
            int pointsPerCell = 15 * board.Difficulty;
            int scoreFromCells = revealedSafeCells * pointsPerCell;

            // Small time penalty
            TimeSpan elapsed = DateTime.Now - gameStartTime;
            int seconds = (int)elapsed.TotalSeconds;
            int timePenalty = seconds * 2;

            int currentScore = Math.Max(0, scoreFromCells - timePenalty);

            // Big bonus if you win the game
            if (board.GameState == GameState.Won)
                currentScore += board.Size * board.Size * 25;

            return currentScore;
        }

        /// <summary>
        /// Handles the end of the game (win or loss).
        /// </summary>
        private void HandleGameOver()
        {
            int finalScore = CalculateCurrentScore();
            lblScore.Text = $"Score: {finalScore}";

            if (board.GameState == GameState.Won)
            {
                using (NameEntryForm nameForm = new NameEntryForm(finalScore))
                {
                    if (nameForm.ShowDialog(this) == DialogResult.OK)
                    {
                        string playerName = nameForm.PlayerName;
                        using (HighScoresForm scoresForm = new HighScoresForm(playerName, finalScore))
                        {
                            scoresForm.ShowDialog(this);
                        }
                    }
                }
            }
            else
            {
                string message = $"Boom! You lost. Final score: {finalScore}";
                MessageBox.Show(message, "Game Over", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// Automatically positions the labels and Restart button.
        /// </summary>
        private void SetupFormLayout()
        {
            int rightX = pnlGrid.Width + 50;

            lblStartTime.Location = new Point(rightX, 80);
            lblScore.Location = new Point(rightX, 130);
            btnRestart.Location = new Point(rightX, 200);
            btnRestart.Size = new Size(130, 45);

            this.ClientSize = new Size(rightX + 180, pnlGrid.Height + 120);
            this.MinimumSize = this.ClientSize;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Event handler for the Restart button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, EventArgs e)
        {
            gameTimer?.Stop();
            this.Close();
        }
    }
}