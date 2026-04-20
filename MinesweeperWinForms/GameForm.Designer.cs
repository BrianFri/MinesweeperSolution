namespace MinesweeperWinForms
{
    partial class GameForm
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
            pnlGrid = new Panel();
            lblStartTime = new Label();
            lblScore = new Label();
            btnRestart = new Button();
            SuspendLayout();
            // 
            // pnlGrid
            // 
            pnlGrid.BorderStyle = BorderStyle.FixedSingle;
            pnlGrid.Location = new Point(20, 60);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(400, 400);
            pnlGrid.TabIndex = 0;
            // 
            // lblStartTime
            // 
            lblStartTime.AutoSize = true;
            lblStartTime.Location = new Point(480, 80);
            lblStartTime.Name = "lblStartTime";
            lblStartTime.Size = new Size(94, 15);
            lblStartTime.TabIndex = 1;
            lblStartTime.Text = "Start Time: 00:00";
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(480, 130);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(48, 15);
            lblScore.TabIndex = 2;
            lblScore.Text = "Score: 0";
            // 
            // btnRestart
            // 
            btnRestart.Location = new Point(480, 200);
            btnRestart.Name = "btnRestart";
            btnRestart.Size = new Size(120, 40);
            btnRestart.TabIndex = 3;
            btnRestart.Text = "Restart";
            btnRestart.UseVisualStyleBackColor = true;
            btnRestart.Click += btnRestart_Click;
            // 
            // GameForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(634, 511);
            Controls.Add(btnRestart);
            Controls.Add(lblScore);
            Controls.Add(lblStartTime);
            Controls.Add(pnlGrid);
            Name = "GameForm";
            Text = "Minesweeper";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel pnlGrid;
        private Label lblStartTime;
        private Label lblScore;
        private Button btnRestart;
    }
}