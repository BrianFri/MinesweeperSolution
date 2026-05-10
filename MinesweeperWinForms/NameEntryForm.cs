using System;
using System.Windows.Forms;

namespace MinesweeperWinForms
{
    public partial class NameEntryForm : Form
    {
        public string PlayerName { get; private set; } = string.Empty;
        private readonly int _score;

        public NameEntryForm(int score)
        {
            InitializeComponent();
            _score = score;
            lblScore.Text = $"Score: {_score}";
        }

        /// <summary>
        /// OK event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Please enter your name.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            PlayerName = txtName.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}