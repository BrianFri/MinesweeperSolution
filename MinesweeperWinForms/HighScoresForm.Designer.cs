namespace MinesweeperWinForms
{
    partial class HighScoresForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            sortToolStripMenuItem = new ToolStripMenuItem();
            byNameToolStripMenuItem = new ToolStripMenuItem();
            byScoreToolStripMenuItem = new ToolStripMenuItem();
            byDataToolStripMenuItem = new ToolStripMenuItem();
            dataGridView1 = new DataGridView();
            btnOK = new Button();
            lblAvgScore = new Label();
            lblAvgTime = new Label();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, sortToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(798, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { saveToolStripMenuItem, loadToolStripMenuItem, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(100, 22);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // loadToolStripMenuItem
            // 
            loadToolStripMenuItem.Name = "loadToolStripMenuItem";
            loadToolStripMenuItem.Size = new Size(100, 22);
            loadToolStripMenuItem.Text = "Load";
            loadToolStripMenuItem.Click += loadToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(100, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // sortToolStripMenuItem
            // 
            sortToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { byNameToolStripMenuItem, byScoreToolStripMenuItem, byDataToolStripMenuItem });
            sortToolStripMenuItem.Name = "sortToolStripMenuItem";
            sortToolStripMenuItem.Size = new Size(40, 20);
            sortToolStripMenuItem.Text = "Sort";
            // 
            // byNameToolStripMenuItem
            // 
            byNameToolStripMenuItem.Name = "byNameToolStripMenuItem";
            byNameToolStripMenuItem.Size = new Size(122, 22);
            byNameToolStripMenuItem.Text = "By Name";
            byNameToolStripMenuItem.Click += byNameToolStripMenuItem_Click;
            // 
            // byScoreToolStripMenuItem
            // 
            byScoreToolStripMenuItem.Name = "byScoreToolStripMenuItem";
            byScoreToolStripMenuItem.Size = new Size(122, 22);
            byScoreToolStripMenuItem.Text = "By Score";
            byScoreToolStripMenuItem.Click += byScoreToolStripMenuItem_Click;
            // 
            // byDataToolStripMenuItem
            // 
            byDataToolStripMenuItem.Name = "byDataToolStripMenuItem";
            byDataToolStripMenuItem.Size = new Size(122, 22);
            byDataToolStripMenuItem.Text = "By Date";
            byDataToolStripMenuItem.Click += byDateToolStripMenuItem_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 27);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.Size = new Size(776, 378);
            dataGridView1.TabIndex = 1;
            // 
            // btnOK
            // 
            btnOK.Location = new Point(352, 469);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 2;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // lblAvgScore
            // 
            lblAvgScore.AutoSize = true;
            lblAvgScore.Location = new Point(31, 423);
            lblAvgScore.Name = "lblAvgScore";
            lblAvgScore.Size = new Size(94, 15);
            lblAvgScore.TabIndex = 3;
            lblAvgScore.Text = "Average Score: 0";
            // 
            // lblAvgTime
            // 
            lblAvgTime.AutoSize = true;
            lblAvgTime.Location = new Point(204, 423);
            lblAvgTime.Name = "lblAvgTime";
            lblAvgTime.Size = new Size(167, 15);
            lblAvgTime.TabIndex = 4;
            lblAvgTime.Text = "Average Time per Game: 00:00";
            // 
            // HighScoresForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(798, 600);
            Controls.Add(lblAvgTime);
            Controls.Add(lblAvgScore);
            Controls.Add(btnOK);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "HighScoresForm";
            Text = "HighScoresForm";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem sortToolStripMenuItem;
        private ToolStripMenuItem byNameToolStripMenuItem;
        private ToolStripMenuItem byScoreToolStripMenuItem;
        private ToolStripMenuItem byDataToolStripMenuItem;
        private DataGridView dataGridView1;
        private Button btnOK;
        private Label lblAvgScore;
        private Label lblAvgTime;
    }
}