using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MinesweeperLibrary.Models;

namespace MinesweeperWinForms
{
    public partial class HighScoresForm : Form
    {
        private List<GameStat> _stats = new List<GameStat>();
        private readonly string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "highscores.txt");

        /// <summary>
        /// Initializes a new instance of the class.
        /// Loads existing high scores from file, optionally adds a new score if player information is provided,
        /// binds the data to the grid view, and updates the statistics summary.
        /// </summary>
        /// <param name="playerName">The name of the player. If provided, a new high score entry is added.</param>
        /// <param name="score">The score achieved by the player.</param>
        /// <param name="playDurationSeconds">The duration of the game in seconds.</param>
        public HighScoresForm(string playerName = null, int score = 0, int playDurationSeconds = 0)
        {
            InitializeComponent();
            LoadHighScores();

            if (!string.IsNullOrEmpty(playerName))
            {
                AddNewScore(playerName, score, playDurationSeconds);
                SaveHighScores();
            }

            BindToGrid();
            UpdateStatisticsSummary();
        }

        /// <summary>
        /// Loads high scores from the highscores.txt file into the internal list.
        /// Corrupt or malformed lines are silently skipped.
        /// </summary>
        private void LoadHighScores()
        {
            _stats.Clear();
            if (File.Exists(_filePath))
            {
                string[] lines = File.ReadAllLines(_filePath);
                foreach (string line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split('|');
                    if (parts.Length >= 4)
                    {
                        try
                        {
                            GameStat stat = new GameStat
                            {
                                Id = int.Parse(parts[0].Trim()),
                                Name = parts[1].Trim(),
                                Score = int.Parse(parts[2].Trim()),
                                GameTime = DateTime.Parse(parts[3].Trim()),
                                PlayDurationSeconds = parts.Length > 4 ? int.Parse(parts[4].Trim()) : 0
                            };
                            _stats.Add(stat);
                        }
                        catch { }
                    }
                }
            }
        }

        /// <summary>
        /// Saves the current list of high scores to the highscores.txt file.
        /// </summary>
        private void SaveHighScores()
        {
            List<string> lines = _stats.Select(s =>
                $"{s.Id}|{s.Name}|{s.Score}|{s.GameTime}|{s.PlayDurationSeconds}").ToList();
            File.WriteAllLines(_filePath, lines);
        }

        /// <summary>
        /// Adds a new high score entry to the list with an auto-generated unique ID.
        /// </summary>
        /// <param name="playerName">The name of the player.</param>
        /// <param name="score">The score achieved.</param>
        /// <param name="playDurationSeconds">The duration of the game in seconds.</param>
        private void AddNewScore(string playerName, int score, int playDurationSeconds)
        {
            int newId = _stats.Any() ? _stats.Max(s => s.Id) + 1 : 1;
            GameStat newStat = new GameStat
            {
                Id = newId,
                Name = playerName,
                Score = score,
                GameTime = DateTime.Now,
                PlayDurationSeconds = playDurationSeconds
            };
            _stats.Add(newStat);
        }

        /// <summary>
        /// Binds the high scores list to the DataGridView control.
        /// Configures the display name and format for the GameTime column and hides the internal PlayDurationSeconds column.
        /// </summary>
        private void BindToGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _stats;

            if (dataGridView1.Columns["GameTime"] != null)
            {
                dataGridView1.Columns["GameTime"].HeaderText = "Date";
                dataGridView1.Columns["GameTime"].DefaultCellStyle.Format = "g";
            }

            if (dataGridView1.Columns["PlayDurationSeconds"] != null)
                dataGridView1.Columns["PlayDurationSeconds"].Visible = false;
        }

        /// <summary>
        /// Calculates the average score and average play duration across all high scores,
        /// then updates the corresponding summary labels on the form.
        /// </summary>
        private void UpdateStatisticsSummary()
        {
            if (_stats.Count == 0)
            {
                lblAvgScore.Text = "Average Score: 0";
                lblAvgTime.Text = "Average Time per Game: 00:00";
                return;
            }

            double avgScore = _stats.Average(s => s.Score);
            double avgSeconds = _stats.Average(s => s.PlayDurationSeconds);

            lblAvgScore.Text = $"Average Score: {avgScore:F0}";

            TimeSpan avgTime = TimeSpan.FromSeconds(avgSeconds);
            lblAvgTime.Text = $"Average Time per Game: {avgTime:mm\\:ss}";
        }

        /// <summary>
        /// Event handler for the Save menu item.
        /// Saves the current high scores to file and displays a confirmation message.
        /// </summary>
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveHighScores();
            MessageBox.Show("High scores saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the Load menu item.
        /// Reloads high scores from file, refreshes the grid, updates the statistics summary,
        /// and displays a confirmation message.
        /// </summary>
        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadHighScores();
            BindToGrid();
            UpdateStatisticsSummary();
            MessageBox.Show("High scores loaded from file.", "Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Event handler for the "By Name" sort menu item.
        /// Sorts high scores by player name and refreshes the grid and summary.
        /// </summary>
        private void byNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stats = _stats.OrderBy(s => s.Name).ToList();
            BindToGrid();
            UpdateStatisticsSummary();
        }

        /// <summary>
        /// Event handler for the "By Score" sort menu item.
        /// Sorts high scores by score and refreshes the grid and summary.
        /// </summary>
        private void byScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stats = _stats.OrderByDescending(s => s.Score).ToList();
            BindToGrid();
            UpdateStatisticsSummary();
        }

        /// <summary>
        /// Event handler for the "By Date" sort menu item.
        /// Sorts high scores by date and refreshes the grid and summary.
        /// </summary>
        private void byDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stats = _stats.OrderByDescending(s => s.GameTime).ToList();
            BindToGrid();
            UpdateStatisticsSummary();
        }

        /// <summary>
        /// Event handler for the Exit menu item.
        /// Closes the high scores form.
        /// </summary>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Event handler for the OK button.
        /// Closes the high scores form.
        /// </summary>
        private void btnOK_Click(object sender, EventArgs e) => this.Close();
    }
}