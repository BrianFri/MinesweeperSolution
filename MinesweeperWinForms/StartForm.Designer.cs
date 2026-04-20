namespace MinesweeperWinForms
{
    partial class StartForm
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
            label1 = new Label();
            lblSize = new Label();
            lblDiffuculty = new Label();
            trackBarSize = new TrackBar();
            trackBarDifficulty = new TrackBar();
            btnPlay = new Button();
            ((System.ComponentModel.ISupportInitialize)trackBarSize).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trackBarDifficulty).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(103, 15);
            label1.TabIndex = 0;
            label1.Text = "Play MineSweeper";
            // 
            // lblSize
            // 
            lblSize.AutoSize = true;
            lblSize.Location = new Point(354, 153);
            lblSize.Name = "lblSize";
            lblSize.Size = new Size(27, 15);
            lblSize.TabIndex = 1;
            lblSize.Text = "Size";
            // 
            // lblDiffuculty
            // 
            lblDiffuculty.AutoSize = true;
            lblDiffuculty.Location = new Point(354, 219);
            lblDiffuculty.Name = "lblDiffuculty";
            lblDiffuculty.Size = new Size(55, 15);
            lblDiffuculty.TabIndex = 2;
            lblDiffuculty.Text = "Difficulty";
            // 
            // trackBarSize
            // 
            trackBarSize.Location = new Point(322, 171);
            trackBarSize.Maximum = 20;
            trackBarSize.Minimum = 5;
            trackBarSize.Name = "trackBarSize";
            trackBarSize.Size = new Size(104, 45);
            trackBarSize.TabIndex = 3;
            trackBarSize.TabStop = false;
            trackBarSize.Value = 10;
            // 
            // trackBarDifficulty
            // 
            trackBarDifficulty.Location = new Point(322, 237);
            trackBarDifficulty.Maximum = 5;
            trackBarDifficulty.Minimum = 1;
            trackBarDifficulty.Name = "trackBarDifficulty";
            trackBarDifficulty.Size = new Size(104, 45);
            trackBarDifficulty.TabIndex = 4;
            trackBarDifficulty.Value = 2;
            // 
            // btnPlay
            // 
            btnPlay.Location = new Point(334, 288);
            btnPlay.Name = "btnPlay";
            btnPlay.Size = new Size(75, 23);
            btnPlay.TabIndex = 5;
            btnPlay.Text = "Play";
            btnPlay.UseVisualStyleBackColor = true;
            btnPlay.Click += btnPlay_Click;
            // 
            // StartForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnPlay);
            Controls.Add(trackBarDifficulty);
            Controls.Add(trackBarSize);
            Controls.Add(lblDiffuculty);
            Controls.Add(lblSize);
            Controls.Add(label1);
            Name = "StartForm";
            Text = "StartForm";
            ((System.ComponentModel.ISupportInitialize)trackBarSize).EndInit();
            ((System.ComponentModel.ISupportInitialize)trackBarDifficulty).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label lblSize;
        private Label lblDiffuculty;
        private TrackBar trackBarSize;
        private TrackBar trackBarDifficulty;
        private Button btnPlay;
    }
}