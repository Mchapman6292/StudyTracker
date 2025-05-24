using Guna.UI2.WinForms;
using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;

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
        private Guna.UI2.WinForms.Guna2Panel controlPanel;
        private Guna.UI2.WinForms.Guna2GradientButton pauseButton;
        private Guna.UI2.WinForms.Guna2GradientButton stopButton;
        private Guna.UI2.WinForms.Guna2GradientButton restartButton;
        private CustomGradientButton testButton;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            sessionNameLabel = new Guna2HtmlLabel();
            controlPanel = new Guna2Panel();
            pauseButton = new Guna2GradientButton();
            stopButton = new Guna2GradientButton();
            restartButton = new Guna2GradientButton();
            testButton = new CustomGradientButton();
            mainPanel.SuspendLayout();
            controlPanel.SuspendLayout();
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
            mainPanel.BackColor = Color.Transparent;
            mainPanel.BorderColor = Color.FromArgb(225, 225, 225);
            mainPanel.BorderRadius = 12;
            mainPanel.Controls.Add(sessionNameLabel);
            mainPanel.Controls.Add(controlPanel);
            mainPanel.Controls.Add(testButton);
            mainPanel.CustomizableEdges = customizableEdges11;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 80);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges12;
            mainPanel.ShadowDecoration.Depth = 20;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(3, 3, 7, 7);
            mainPanel.Size = new Size(320, 400);
            mainPanel.TabIndex = 0;
            // 
            // sessionNameLabel
            // 
            sessionNameLabel.BackColor = Color.Transparent;
            sessionNameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sessionNameLabel.ForeColor = Color.FromArgb(120, 120, 120);
            sessionNameLabel.Location = new Point(80, 265);
            sessionNameLabel.Name = "sessionNameLabel";
            sessionNameLabel.Size = new Size(106, 19);
            sessionNameLabel.TabIndex = 3;
            sessionNameLabel.Text = "♪ By The Sea Side";
            sessionNameLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // controlPanel
            // 
            controlPanel.BackColor = Color.Transparent;
            controlPanel.Controls.Add(pauseButton);
            controlPanel.Controls.Add(stopButton);
            controlPanel.Controls.Add(restartButton);
            controlPanel.CustomizableEdges = customizableEdges7;
            controlPanel.FillColor = Color.Transparent;
            controlPanel.Location = new Point(20, 290);
            controlPanel.Name = "controlPanel";
            controlPanel.ShadowDecoration.CustomizableEdges = customizableEdges8;
            controlPanel.Size = new Size(280, 80);
            controlPanel.TabIndex = 4;
            // 
            // pauseButton
            // 
            pauseButton.BackColor = Color.Transparent;
            pauseButton.BorderColor = Color.FromArgb(210, 210, 210);
            pauseButton.BorderRadius = 24;
            pauseButton.BorderThickness = 1;
            pauseButton.CustomizableEdges = customizableEdges1;
            pauseButton.DisabledState.BorderColor = Color.DarkGray;
            pauseButton.DisabledState.CustomBorderColor = Color.DarkGray;
            pauseButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            pauseButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            pauseButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            pauseButton.FillColor = Color.FromArgb(246, 246, 246);
            pauseButton.FillColor2 = Color.FromArgb(234, 234, 234);
            pauseButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pauseButton.ForeColor = Color.DimGray;
            pauseButton.Location = new Point(40, 16);
            pauseButton.Name = "pauseButton";
            pauseButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            pauseButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            pauseButton.ShadowDecoration.Depth = 5;
            pauseButton.ShadowDecoration.Enabled = true;
            pauseButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            pauseButton.Size = new Size(48, 48);
            pauseButton.TabIndex = 5;
            pauseButton.Text = "⏸️";
            // 
            // stopButton
            // 
            stopButton.BackColor = Color.Transparent;
            stopButton.BorderRadius = 10;
            stopButton.CustomizableEdges = customizableEdges3;
            stopButton.DisabledState.BorderColor = Color.DarkGray;
            stopButton.DisabledState.CustomBorderColor = Color.DarkGray;
            stopButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            stopButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            stopButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            stopButton.FillColor = Color.FromArgb(115, 93, 185);
            stopButton.FillColor2 = Color.FromArgb(102, 82, 162);
            stopButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0);
            stopButton.ForeColor = Color.White;
            stopButton.Location = new Point(105, 16);
            stopButton.Name = "stopButton";
            stopButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            stopButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            stopButton.ShadowDecoration.Depth = 5;
            stopButton.ShadowDecoration.Enabled = true;
            stopButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            stopButton.Size = new Size(70, 48);
            stopButton.TabIndex = 6;
            stopButton.Text = "Stop";
            // 
            // restartButton
            // 
            restartButton.BackColor = Color.Transparent;
            restartButton.BorderColor = Color.FromArgb(210, 210, 210);
            restartButton.BorderRadius = 24;
            restartButton.BorderThickness = 1;
            restartButton.CustomizableEdges = customizableEdges5;
            restartButton.DisabledState.BorderColor = Color.DarkGray;
            restartButton.DisabledState.CustomBorderColor = Color.DarkGray;
            restartButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            restartButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            restartButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            restartButton.FillColor = Color.FromArgb(246, 246, 246);
            restartButton.FillColor2 = Color.FromArgb(234, 234, 234);
            restartButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            restartButton.ForeColor = Color.DimGray;
            restartButton.Location = new Point(192, 16);
            restartButton.Name = "restartButton";
            restartButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            restartButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            restartButton.ShadowDecoration.Depth = 5;
            restartButton.ShadowDecoration.Enabled = true;
            restartButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            restartButton.Size = new Size(48, 48);
            restartButton.TabIndex = 7;
            restartButton.Text = "⟳";
            // 
            // testButton
            // 
            testButton.Anchor = AnchorStyles.None;
            testButton.EnableHoverRipple = true;
            testButton.Animated = true;
            testButton.BackColor = Color.Transparent;
            testButton.ButtonMode = Guna.UI2.WinForms.Enums.ButtonMode.RadioButton;
            testButton.CheckedState.FillColor = Color.White;
            testButton.CheckedState.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            testButton.CheckedState.ForeColor = Color.DimGray;
            testButton.CustomizableEdges = customizableEdges9;
            testButton.FillColor = Color.FromArgb(24, 30, 52);
            testButton.Font = new Font("Segoe UI", 9F);
            testButton.ForeColor = Color.Gray;
            testButton.HoverState.FillColor = Color.White;
            testButton.HoverState.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            testButton.HoverState.ForeColor = Color.DimGray;
            testButton.ImageAlign = HorizontalAlignment.Left;
            testButton.ImageOffset = new Point(30, 0);
            testButton.Location = new Point(60, 122);
            testButton.Name = "testButton";
            testButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
            testButton.Size = new Size(145, 43);
            testButton.TabIndex = 2;
            testButton.Text = "Timeline";
            testButton.TextAlign = HorizontalAlignment.Left;
            testButton.TextOffset = new Point(45, 0);
            testButton.UseTransparentBackground = true;
            // 
            // TestForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(320, 400);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "TestForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Modern Timer Test";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            controlPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Guna2HtmlLabel sessionNameLabel;
    }
}