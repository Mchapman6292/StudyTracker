using Guna.UI2.WinForms.Suite;
using Guna.UI2.WinForms;

namespace CodingTracker.View.Forms.Session
{
    partial class SessionRatingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Guna2BorderlessForm borderlessForm;

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
            components = new System.ComponentModel.Container();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            titleLabel = new Guna2HtmlLabel();
            ratingStarTool = new Guna2RatingStar();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // borderlessForm
            // 
            borderlessForm.BorderRadius = 15;
            borderlessForm.ContainerControl = this;
            borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            borderlessForm.TransparentWhileDrag = true;
            // 
            // mainPanel
            // 
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderRadius = 15;
            mainPanel.BorderThickness = 1;
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(ratingStarTool);
            mainPanel.CustomizableEdges = customizableEdges3;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(15);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges4;
            mainPanel.Size = new Size(230, 100);
            mainPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.BackColor = Color.Transparent;
            titleLabel.Font = new Font("Century Gothic", 11F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(255, 160, 210);
            titleLabel.Location = new Point(59, 12);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(118, 20);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Rate this session";
            titleLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // ratingStarTool
            // 
            ratingStarTool.BorderColor = Color.FromArgb(213, 218, 223);
            ratingStarTool.FillColor = Color.FromArgb(213, 218, 223);
            ratingStarTool.Location = new Point(44, 47);
            ratingStarTool.Name = "ratingStarTool";
            ratingStarTool.RatingColor = Color.Gold;
            ratingStarTool.Size = new Size(152, 35);
            ratingStarTool.TabIndex = 1;
            ratingStarTool.ValueChanged += starRating_ValueChanged;
            // 
            // SessionRatingForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(230, 100);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "SessionRatingForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "SessionRatingForm";
            TopMost = true;
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Guna2Panel mainPanel;
        private Guna2GradientButton confirmButton;
        private Guna2RatingStar ratingStarTool;
        private Guna2HtmlLabel titleLabel;
    }
}