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
            UpdateDetectorStatus();
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
        /// Sets up a timer that updates the elapsed time, score, and detector status every second.
        /// </summary>
        private void SetupTimer()
        {
            gameTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            gameTimer.Tick += (s, e) =>
            {
                TimeSpan elapsed = DateTime.Now - gameStartTime;
                lblStartTime.Text = $"Start Time: {elapsed:mm\\:ss}";
                lblScore.Text = $"Score: {CalculateCurrentScore()}";
                UpdateDetectorStatus();
            };
            gameTimer.Start();
        }

        /// <summary>
        /// Updates the bomb detector status label.
        /// </summary>
        private void UpdateDetectorStatus()
        {
            if (lblDetector == null) return;
            lblDetector.Text = $"Bomb Detector: {board.RewardsRemaining}";
            lblDetector.ForeColor = board.RewardsRemaining > 0 ? Color.DarkGreen : Color.Gray;
        }

        /// <summary>
        /// Uses the one-time bomb detector when Ctrl + Left Click is performed.
        /// </summary>
        private void UseBombDetector(int row, int col)
        {
            if (board.RewardsRemaining <= 0) return;

            CellModel cell = board.Cells[row, col];
            if (cell.IsVisited || cell.IsFlagged) return;

            board.RewardsRemaining = 0;
            UpdateDetectorStatus();

            if (cell.IsBomb)
            {
                MessageBox.Show("💣 BOMB DETECTED!\n\nThis cell contains a mine.\nYou have been warned, flag it or avoid clicking it!",
                                "Bomb Detector", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                logic.FloodFill(board, row, col);
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

        /// <summary>
        /// Handles left-click and right-click actions.
        /// </summary>
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

                    if (Control.ModifierKeys == Keys.Control && board.RewardsRemaining > 0)
                    {
                        UseBombDetector(row, col);
                        return;
                    }

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
        /// Updates the visual state of a single cell button.
        /// </summary>
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
                btn.BackgroundImage = cell.HasSpecialReward ? Properties.Resources.Gold : Properties.Resources.Tile_Flat;
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
                string resourceName = "Number " + cell.NumberOfBombNeighbors;
                btn.BackgroundImage = (Image)Properties.Resources.ResourceManager.GetObject(resourceName);
            }
            else
            {
                btn.BackgroundImage = Properties.Resources.Tile_1;
            }

            btn.Enabled = !cell.IsVisited && !cell.IsFlagged;
        }

        /// <summary>
        /// Calculates the player's current score based on revealed cells and time penalty.
        /// </summary>
        private int CalculateCurrentScore()
        {
            int revealedSafeCells = 0;
            for (int r = 0; r < board.Size; r++)
                for (int c = 0; c < board.Size; c++)
                    if (board.Cells[r, c].IsVisited && !board.Cells[r, c].IsBomb)
                        revealedSafeCells++;

            int pointsPerCell = 15 * board.Difficulty;
            int scoreFromCells = revealedSafeCells * pointsPerCell;

            TimeSpan elapsed = DateTime.Now - gameStartTime;
            int timePenalty = (int)elapsed.TotalSeconds * 2;

            int currentScore = Math.Max(0, scoreFromCells - timePenalty);

            if (board.GameState == GameState.Won)
                currentScore += board.Size * board.Size * 25;

            return currentScore;
        }

        /// <summary>
        /// Handles game over logic (win or loss) and shows the high scores form when winning.
        /// </summary>
        private void HandleGameOver()
        {
            int finalScore = CalculateCurrentScore();
            lblScore.Text = $"Score: {finalScore}";

            TimeSpan playDuration = DateTime.Now - gameStartTime;

            if (board.GameState == GameState.Won)
            {
                using (NameEntryForm nameForm = new NameEntryForm(finalScore))
                {
                    if (nameForm.ShowDialog(this) == DialogResult.OK)
                    {
                        string playerName = nameForm.PlayerName;
                        using (HighScoresForm scoresForm = new HighScoresForm(
                            playerName,
                            finalScore,
                            (int)playDuration.TotalSeconds))
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
        /// Positions all controls on the form after the grid is created.
        /// </summary>
        private void SetupFormLayout()
        {
            int rightX = pnlGrid.Width + 50;

            lblStartTime.Location = new Point(rightX, 80);
            lblScore.Location = new Point(rightX, 130);
            lblDetector.Location = new Point(rightX, 165);
            btnRestart.Location = new Point(rightX, 200);
            btnRestart.Size = new Size(130, 45);

            this.ClientSize = new Size(rightX + 180, pnlGrid.Height + 120);
            this.MinimumSize = this.ClientSize;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Event handler for the Restart button.
        /// </summary>
        private void btnRestart_Click(object sender, EventArgs e)
        {
            gameTimer?.Stop();
            this.Close();
        }
    }
}