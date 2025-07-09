using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using LiveCharts;
using LiveCharts.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp.Views.Desktop;

namespace CodingTracker.View.Forms
{
    partial class TestForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        // Control declarations
        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;
        private Guna.UI2.WinForms.Guna2Panel mainPanel;
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
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            newTestButton = new Guna2GradientButton();
            skControlTest = new SKControl();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            timeDisplayLabel = new Guna2HtmlLabel();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // borderlessForm
            // 
            borderlessForm.BorderRadius = 12;
            borderlessForm.ContainerControl = this;
            borderlessForm.DockIndicatorTransparencyValue = 0.6D;
            borderlessForm.TransparentWhileDrag = true;
            // 
            // mainPanel
            // 
            mainPanel.Anchor = AnchorStyles.None;
            mainPanel.BackColor = Color.Transparent;
            mainPanel.BorderColor = Color.FromArgb(225, 225, 225);
            mainPanel.BorderRadius = 12;
            mainPanel.Controls.Add(timeDisplayLabel);
            mainPanel.Controls.Add(newTestButton);
            mainPanel.Controls.Add(skControlTest);
            mainPanel.CustomizableEdges = customizableEdges7;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges8;
            mainPanel.ShadowDecoration.Depth = 20;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(3, 3, 7, 7);
            mainPanel.Size = new Size(1300, 720);
            mainPanel.TabIndex = 0;
            // 
            // newTestButton
            // 
            newTestButton.CustomizableEdges = customizableEdges5;
            newTestButton.DisabledState.BorderColor = Color.DarkGray;
            newTestButton.DisabledState.CustomBorderColor = Color.DarkGray;
            newTestButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            newTestButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            newTestButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            newTestButton.Font = new Font("Segoe UI", 9F);
            newTestButton.ForeColor = Color.White;
            newTestButton.Location = new Point(0, 3);
            newTestButton.Name = "newTestButton";
            newTestButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            newTestButton.Size = new Size(72, 45);
            newTestButton.TabIndex = 7;
            newTestButton.Text = "Test Button";
            newTestButton.Click += newTestButton_Click;
            // 
            // skControlTest
            // 
            skControlTest.BackColor = Color.FromArgb(35, 34, 50);
            skControlTest.ForeColor = Color.FromArgb(35, 34, 50);
            skControlTest.Location = new Point(51, 92);
            skControlTest.Name = "skControlTest";
            skControlTest.Size = new Size(1046, 590);
            skControlTest.TabIndex = 0;
            // 
            // timeDisplayLabel
            // 
            timeDisplayLabel.AutoSize = false;
            timeDisplayLabel.BackColor = Color.Transparent;
            timeDisplayLabel.Font = new Font("Segoe UI", 13F);
            timeDisplayLabel.ForeColor = Color.HotPink;
            timeDisplayLabel.Location = new Point(1071, 47);
            timeDisplayLabel.Name = "timeDisplayLabel";
            timeDisplayLabel.Size = new Size(157, 47);
            timeDisplayLabel.TabIndex = 8;
            timeDisplayLabel.Text = "guna2HtmlLabel1";
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(1300, 720);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TestForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Modern Timer Test";
            mainPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private CustomGradientButton testButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Services.MainPageService.RecentActivityService.Panels.DurationPanel durationPanel1;


        private SKControl skControlTest;
        private Guna2GradientButton newTestButton;
        private Guna2HtmlLabel timeDisplayLabel;
    }
}