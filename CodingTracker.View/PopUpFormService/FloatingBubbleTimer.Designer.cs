namespace CodingTracker.View.PopUpFormService
{
    partial class FloatingBubbleTimer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // Controls that are used in the main class
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
        private Guna.UI2.WinForms.Guna2HtmlLabel timerLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel sessionLabel;
        private Guna.UI2.WinForms.Guna2Button minimizeButton;
        private Guna.UI2.WinForms.Guna2Button stopButton;

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
            this.components = new System.ComponentModel.Container();
            this.mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.timerLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.sessionLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            this.minimizeButton = new Guna.UI2.WinForms.Guna2Button();
            this.stopButton = new Guna.UI2.WinForms.Guna2Button();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainPanel
            // 
            this.mainPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(100)))), ((int)(((byte)(180)))));
            this.mainPanel.BorderRadius = 25;
            this.mainPanel.BorderThickness = 1;
            this.mainPanel.Controls.Add(this.timerLabel);
            this.mainPanel.Controls.Add(this.sessionLabel);
            this.mainPanel.Controls.Add(this.minimizeButton);
            this.mainPanel.Controls.Add(this.stopButton);
            this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainPanel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(40)))), ((int)(((byte)(60)))));
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(140, 50);
            this.mainPanel.TabIndex = 0;
            this.Controls.Add(this.mainPanel);
            this.ResumeLayout(false);
            this.PerformLayout();
            // 
            // timerLabel
            // 
            this.timerLabel.BackColor = System.Drawing.Color.Transparent;
            this.timerLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timerLabel.ForeColor = System.Drawing.Color.White;
            this.timerLabel.Location = new System.Drawing.Point(5, 7);
            this.timerLabel.Name = "timerLabel";
            this.timerLabel.Size = new System.Drawing.Size(130, 30);
            this.timerLabel.TabIndex = 0;
            this.timerLabel.Text = "00:00:00";
            this.timerLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // sessionLabel
            // 
            this.sessionLabel.BackColor = System.Drawing.Color.Transparent;
            this.sessionLabel.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sessionLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.sessionLabel.Location = new System.Drawing.Point(5, 30);
            this.sessionLabel.Name = "sessionLabel";
            this.sessionLabel.Size = new System.Drawing.Size(130, 20);
            this.sessionLabel.TabIndex = 1;
            this.sessionLabel.Text = "New Session";
            this.sessionLabel.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            // 

        }
        #endregion
    }
}