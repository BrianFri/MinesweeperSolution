namespace MinesweeperWinForms
{
    partial class NameEntryForm
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
            lblCongratulations = new Label();
            txtName = new TextBox();
            lblScore = new Label();
            btnOK = new Button();
            SuspendLayout();
            // 
            // lblCongratulations
            // 
            lblCongratulations.AutoSize = true;
            lblCongratulations.Location = new Point(169, 114);
            lblCongratulations.Name = "lblCongratulations";
            lblCongratulations.Size = new Size(233, 15);
            lblCongratulations.TabIndex = 0;
            lblCongratulations.Text = "Congratulations you win. Enter your name.";
            // 
            // txtName
            // 
            txtName.Location = new Point(202, 141);
            txtName.Name = "txtName";
            txtName.Size = new Size(165, 23);
            txtName.TabIndex = 1;
            // 
            // lblScore
            // 
            lblScore.AutoSize = true;
            lblScore.Location = new Point(265, 86);
            lblScore.Name = "lblScore";
            lblScore.Size = new Size(36, 15);
            lblScore.TabIndex = 2;
            lblScore.Text = "Score";
            // 
            // btnOK
            // 
            btnOK.Location = new Point(246, 170);
            btnOK.Name = "btnOK";
            btnOK.Size = new Size(75, 23);
            btnOK.TabIndex = 3;
            btnOK.Text = "OK";
            btnOK.UseVisualStyleBackColor = true;
            btnOK.Click += btnOK_Click;
            // 
            // NameEntryForm
            // 
            AcceptButton = btnOK;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(625, 320);
            Controls.Add(btnOK);
            Controls.Add(lblScore);
            Controls.Add(txtName);
            Controls.Add(lblCongratulations);
            Name = "NameEntryForm";
            Text = "NameEntryForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblCongratulations;
        private TextBox txtName;
        private Label lblScore;
        private Button btnOK;
    }
}