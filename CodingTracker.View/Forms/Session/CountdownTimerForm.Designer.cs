using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;

namespace CodingTracker.View.TimerDisplayService
{
    partial class CountdownTimerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private Guna2CircleProgressBar progressBar;
        private Guna2CircleProgressBar trackOutline;
        private System.Windows.Forms.Timer progressTimer;
        private Guna2BorderlessForm borderlessForm;
        private Guna2Panel mainPanel;
        private Guna2GradientButton pauseButton;
        private Guna2GradientButton stopButton;
        private Guna2GradientButton restartButton;
        private Guna2HtmlLabel timeDisplayLabel;
        private Guna2HtmlLabel sessionNameLabel;
        private Guna2Panel controlPanel;
        private Guna2MessageDialog DisplayMessageBox;

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
            components = new Container();
            CustomizableEdges customizableEdges17 = new CustomizableEdges();
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges15 = new CustomizableEdges();
            CustomizableEdges customizableEdges16 = new CustomizableEdges();
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            CustomizableEdges customizableEdges13 = new CustomizableEdges();
            CustomizableEdges customizableEdges14 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            sessionNameLabel = new Guna2HtmlLabel();
            homeButton = new Guna2GradientButton();
            minimizeButton = new Guna2ControlBox();
            closeButton = new Guna2ControlBox();
            progressBar = new Guna2CircleProgressBar();
            trackOutline = new Guna2CircleProgressBar();
            percentProgressLabel = new Guna2HtmlLabel();
            timeDisplayLabel = new Guna2HtmlLabel();
            controlPanel = new Guna2Panel();
            pauseButton = new Guna2GradientButton();
            stopButton = new Guna2GradientButton();
            restartButton = new Guna2GradientButton();
            progressTimer = new System.Windows.Forms.Timer(components);
            DisplayMessageBox = new Guna2MessageDialog();
            gunaAnimationWindow = new Guna2AnimateWindow(components);
            mainPanel.SuspendLayout();
            progressBar.SuspendLayout();
            trackOutline.SuspendLayout();
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
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderRadius = 12;
            mainPanel.BorderThickness = 1;
            mainPanel.Controls.Add(sessionNameLabel);
            mainPanel.Controls.Add(homeButton);
            mainPanel.Controls.Add(minimizeButton);
            mainPanel.Controls.Add(closeButton);
            mainPanel.Controls.Add(progressBar);
            mainPanel.Controls.Add(controlPanel);
            mainPanel.CustomizableEdges = customizableEdges11;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(10);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges17;
            mainPanel.Size = new Size(320, 400);
            mainPanel.TabIndex = 0;
            // 
            // sessionNameLabel
            // 
            sessionNameLabel.BackColor = Color.Transparent;
            sessionNameLabel.Font = new Font("Segoe UI", 10F);
            sessionNameLabel.ForeColor = Color.FromArgb(180, 180, 180);
            sessionNameLabel.Location = new Point(98, 266);
            sessionNameLabel.Name = "sessionNameLabel";
            sessionNameLabel.Size = new Size(115, 19);
            sessionNameLabel.TabIndex = 3;
            sessionNameLabel.Text = "My Coding Session";
            sessionNameLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // homeButton
            // 
            homeButton.CustomizableEdges = customizableEdges1;
            homeButton.FillColor = Color.FromArgb(25, 24, 40);
            homeButton.FillColor2 = Color.FromArgb(25, 24, 40);
            homeButton.Font = new Font("Segoe UI", 9F);
            homeButton.ForeColor = Color.White;
            homeButton.Image = Properties.Resources.HomeButtonIcon;
            homeButton.Location = new Point(243, 0);
            homeButton.Name = "homeButton";
            homeButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            homeButton.Size = new Size(27, 21);
            homeButton.TabIndex = 11;
            homeButton.Click += homeButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeButton.CustomizableEdges = customizableEdges3;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.White;
            minimizeButton.Location = new Point(272, 0);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            minimizeButton.Size = new Size(27, 21);
            minimizeButton.TabIndex = 9;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BorderRadius = 12;
            closeButton.CustomClick = true;
            customizableEdges5.BottomLeft = false;
            customizableEdges5.BottomRight = false;
            customizableEdges5.TopLeft = false;
            closeButton.CustomizableEdges = customizableEdges5;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.White;
            closeButton.Location = new Point(293, 0);
            closeButton.Name = "closeButton";
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            closeButton.Size = new Size(27, 21);
            closeButton.TabIndex = 10;
            closeButton.Click += CloseButton_Click;
            // 
            // progressBar
            // 
            progressBar.AnimationSpeed = 0.5F;
            progressBar.BackColor = Color.Transparent;
            progressBar.Controls.Add(trackOutline);
            progressBar.FillColor = Color.FromArgb(48, 49, 54);
            progressBar.FillThickness = 8;
            progressBar.Font = new Font("Segoe UI", 12F);
            progressBar.ForeColor = Color.White;
            progressBar.Location = new Point(50, 40);
            progressBar.Minimum = 0;
            progressBar.Name = "progressBar";
            progressBar.ProgressColor = Color.FromArgb(255, 81, 195);
            progressBar.ProgressColor2 = Color.FromArgb(168, 228, 255);
            progressBar.ProgressThickness = 8;
            progressBar.ShadowDecoration.CustomizableEdges = customizableEdges8;
            progressBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            progressBar.Size = new Size(220, 220);
            progressBar.TabIndex = 0;
            // 
            // trackOutline
            // 
            trackOutline.BackColor = Color.Transparent;
            trackOutline.Controls.Add(percentProgressLabel);
            trackOutline.Controls.Add(timeDisplayLabel);
            trackOutline.Dock = DockStyle.Fill;
            trackOutline.FillColor = Color.Transparent;
            trackOutline.FillThickness = 1;
            trackOutline.Font = new Font("Segoe UI", 12F);
            trackOutline.ForeColor = Color.White;
            trackOutline.Location = new Point(0, 0);
            trackOutline.Minimum = 0;
            trackOutline.Name = "trackOutline";
            trackOutline.ProgressColor = Color.FromArgb(80, 80, 90);
            trackOutline.ProgressThickness = 1;
            trackOutline.ShadowDecoration.CustomizableEdges = customizableEdges7;
            trackOutline.Size = new Size(220, 220);
            trackOutline.TabIndex = 0;
            trackOutline.Value = 100;
            // 
            // percentProgressLabel
            // 
            percentProgressLabel.AutoSize = false;
            percentProgressLabel.BackColor = Color.Transparent;
            percentProgressLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            percentProgressLabel.ForeColor = Color.White;
            percentProgressLabel.Location = new Point(61, 148);
            percentProgressLabel.Name = "percentProgressLabel";
            percentProgressLabel.Size = new Size(84, 20);
            percentProgressLabel.TabIndex = 3;
            percentProgressLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // timeDisplayLabel
            // 
            timeDisplayLabel.BackColor = Color.Transparent;
            timeDisplayLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            timeDisplayLabel.ForeColor = Color.White;
            timeDisplayLabel.Location = new Point(61, 73);
            timeDisplayLabel.Name = "timeDisplayLabel";
            timeDisplayLabel.Size = new Size(84, 47);
            timeDisplayLabel.TabIndex = 2;
            timeDisplayLabel.Text = "00:00";
            timeDisplayLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // controlPanel
            // 
            controlPanel.BackColor = Color.Transparent;
            controlPanel.Controls.Add(pauseButton);
            controlPanel.Controls.Add(stopButton);
            controlPanel.Controls.Add(restartButton);
            controlPanel.CustomizableEdges = customizableEdges15;
            controlPanel.FillColor = Color.Transparent;
            controlPanel.Location = new Point(20, 290);
            controlPanel.Name = "controlPanel";
            controlPanel.ShadowDecoration.CustomizableEdges = customizableEdges16;
            controlPanel.Size = new Size(280, 80);
            controlPanel.TabIndex = 4;
            // 
            // pauseButton
            // 
            pauseButton.BackColor = Color.Transparent;
            pauseButton.BorderColor = Color.FromArgb(170, 60, 130);
            pauseButton.BorderRadius = 24;
            pauseButton.CustomizableEdges = customizableEdges9;
            pauseButton.DisabledState.BorderColor = Color.DarkGray;
            pauseButton.DisabledState.CustomBorderColor = Color.DarkGray;
            pauseButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            pauseButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            pauseButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            pauseButton.FillColor = Color.FromArgb(170, 60, 130);
            pauseButton.FillColor2 = Color.FromArgb(100, 170, 200);
            pauseButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pauseButton.ForeColor = Color.White;
            pauseButton.Location = new Point(40, 16);
            pauseButton.Name = "pauseButton";
            pauseButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            pauseButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
            pauseButton.ShadowDecoration.Depth = 5;
            pauseButton.ShadowDecoration.Enabled = true;
            pauseButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            pauseButton.Size = new Size(48, 48);
            pauseButton.TabIndex = 5;
            pauseButton.Text = "⏸";
            pauseButton.TextOffset = new Point(3, 0);
            pauseButton.Click += PauseButton_Click;
            // 
            // stopButton
            // 
            stopButton.BackColor = Color.Transparent;
            stopButton.BorderRadius = 10;
            customizableEdges11.BottomRight = false;
            customizableEdges11.TopLeft = false;
            stopButton.CustomizableEdges = customizableEdges11;
            stopButton.DisabledState.BorderColor = Color.DarkGray;
            stopButton.DisabledState.CustomBorderColor = Color.DarkGray;
            stopButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            stopButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            stopButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            stopButton.FillColor = Color.FromArgb(255, 81, 195);
            stopButton.FillColor2 = Color.FromArgb(168, 228, 255);
            stopButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            stopButton.ForeColor = Color.White;
            stopButton.Location = new Point(105, 16);
            stopButton.Name = "stopButton";
            stopButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            stopButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
            stopButton.ShadowDecoration.Depth = 5;
            stopButton.ShadowDecoration.Enabled = true;
            stopButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            stopButton.Size = new Size(70, 48);
            stopButton.TabIndex = 6;
            stopButton.Text = "Stop";
            stopButton.Click += CloseButton_Click;
            // 
            // restartButton
            // 
            restartButton.BackColor = Color.Transparent;
            restartButton.BorderRadius = 24;
            restartButton.CustomizableEdges = customizableEdges13;
            restartButton.DisabledState.BorderColor = Color.DarkGray;
            restartButton.DisabledState.CustomBorderColor = Color.DarkGray;
            restartButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            restartButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            restartButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            restartButton.FillColor = Color.FromArgb(170, 60, 130);
            restartButton.FillColor2 = Color.FromArgb(168, 228, 255);
            restartButton.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            restartButton.ForeColor = Color.White;
            restartButton.Location = new Point(192, 16);
            restartButton.Name = "restartButton";
            restartButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            restartButton.ShadowDecoration.CustomizableEdges = customizableEdges14;
            restartButton.ShadowDecoration.Depth = 5;
            restartButton.ShadowDecoration.Enabled = true;
            restartButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            restartButton.Size = new Size(48, 48);
            restartButton.TabIndex = 7;
            restartButton.Text = "⟳";
            restartButton.TextOffset = new Point(3, 0);
            // 
            // progressTimer
            // 
            progressTimer.Interval = 50;
            progressTimer.Tick += ProgressTimer_Tick;
            // 
            // DisplayMessageBox
            // 
            DisplayMessageBox.Buttons = MessageDialogButtons.OK;
            DisplayMessageBox.Caption = null;
            DisplayMessageBox.Icon = MessageDialogIcon.None;
            DisplayMessageBox.Parent = null;
            DisplayMessageBox.Style = MessageDialogStyle.Default;
            DisplayMessageBox.Text = null;
            // 
            // CountdownTimerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(320, 400);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CountdownTimerForm";
            StartPosition = FormStartPosition.Manual;
            Load += CountdownTimerForm_Load;
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            progressBar.ResumeLayout(false);
            trackOutline.ResumeLayout(false);
            trackOutline.PerformLayout();
            controlPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Guna2ControlBox closeButton;
        private Guna2ControlBox minimizeButton;
        private Guna2GradientButton homeButton;
        private Guna2AnimateWindow gunaAnimationWindow;
        private Guna2HtmlLabel percentProgressLabel;
    }
}