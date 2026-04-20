using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MinesweeperLibrary.Models;
using MinesweeperLibrary.BusinessLogicLayer;

namespace MinesweeperWinForms
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Event handler to play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPlay_Click(object sender, EventArgs e)
        {
            int size = trackBarSize.Value;
            int difficultyLevel = trackBarDifficulty.Value;

            GameForm gameForm = new GameForm(size, difficultyLevel);
            this.Hide();
            gameForm.ShowDialog();
            this.Show();
        }
    }
}
