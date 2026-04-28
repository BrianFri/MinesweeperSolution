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

        public HighScoresForm(string playerName = null, int score = 0)
        {
            InitializeComponent();
            LoadHighScores();

            if (!string.IsNullOrEmpty(playerName))
            {
                AddNewScore(playerName, score);
                SaveHighScores(); // persist the new win immediately
            }

            BindToGrid();
        }

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
                    if (parts.Length == 4)
                    {
                        try
                        {
                            GameStat stat = new GameStat
                            {
                                Id = int.Parse(parts[0].Trim()),
                                Name = parts[1].Trim(),
                                Score = int.Parse(parts[2].Trim()),
                                GameTime = DateTime.Parse(parts[3].Trim())
                            };
                            _stats.Add(stat);
                        }
                        catch { /* skip corrupt lines */ }
                    }
                }
            }
        }

        private void SaveHighScores()
        {
            List<string> lines = _stats.Select(s => $"{s.Id}|{s.Name}|{s.Score}|{s.GameTime}").ToList();
            File.WriteAllLines(_filePath, lines);
        }

        private void AddNewScore(string playerName, int score)
        {
            int newId = _stats.Any() ? _stats.Max(s => s.Id) + 1 : 1;
            GameStat newStat = new GameStat
            {
                Id = newId,
                Name = playerName,
                Score = score,
                GameTime = DateTime.Now
            };
            _stats.Add(newStat);
        }

        private void BindToGrid()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = _stats;

            if (dataGridView1.Columns["GameTime"] != null)
            {
                dataGridView1.Columns["GameTime"].HeaderText = "Date";
                dataGridView1.Columns["GameTime"].DefaultCellStyle.Format = "g";
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveHighScores();
            MessageBox.Show("High scores saved successfully!", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void loadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadHighScores();
            BindToGrid();
            MessageBox.Show("High scores loaded from file.", "Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void byNameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stats = _stats.OrderBy(s => s.Name).ToList();
            BindToGrid();
        }

        private void byScoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stats = _stats.OrderByDescending(s => s.Score).ToList();
            BindToGrid();
        }

        private void byDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _stats = _stats.OrderByDescending(s => s.GameTime).ToList();
            BindToGrid();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}