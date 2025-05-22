using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace CodingTracker.View.Forms.Session
{
    partial class ElapsedTimerPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Timer winFormsTimer;
        private Guna2BorderlessForm borderlessForm;
        private Guna2Panel mainPanel;
        private Guna2GradientButton elapsedTimerPauseButton;
        private Guna2GradientButton restartSessionButton;
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
            CustomizableEdges customizableEdges31 = new CustomizableEdges();
            CustomizableEdges customizableEdges32 = new CustomizableEdges();
            CustomizableEdges customizableEdges17 = new CustomizableEdges();
            CustomizableEdges customizableEdges18 = new CustomizableEdges();
            CustomizableEdges customizableEdges19 = new CustomizableEdges();
            CustomizableEdges customizableEdges20 = new CustomizableEdges();
            CustomizableEdges customizableEdges21 = new CustomizableEdges();
            CustomizableEdges customizableEdges22 = new CustomizableEdges();
            CustomizableEdges customizableEdges29 = new CustomizableEdges();
            CustomizableEdges customizableEdges30 = new CustomizableEdges();
            CustomizableEdges customizableEdges23 = new CustomizableEdges();
            CustomizableEdges customizableEdges24 = new CustomizableEdges();
            CustomizableEdges customizableEdges25 = new CustomizableEdges();
            CustomizableEdges customizableEdges26 = new CustomizableEdges();
            CustomizableEdges customizableEdges27 = new CustomizableEdges();
            CustomizableEdges customizableEdges28 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            durationLabel = new Guna2HtmlLabel();
            sessionNameLabel = new Guna2HtmlLabel();
            homeButton = new Guna2GradientButton();
            elapsedTimerMinimizeButton = new Guna2ControlBox();
            closeButton = new Guna2ControlBox();
            controlPanel = new Guna2Panel();
            elapsedTimerPauseButton = new Guna2GradientButton();
            restartSessionButton = new Guna2GradientButton();
            elapsedPageStopButton = new Guna2GradientButton();
            winFormsTimer = new System.Windows.Forms.Timer(components);
            DisplayMessageBox = new Guna2MessageDialog();
            gunaAnimationWindow = new Guna2AnimateWindow(components);
            timeDisplayLabel = new Guna2HtmlLabel();
            elapsedTimeLabel = new Guna2HtmlLabel();
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
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderRadius = 12;
            mainPanel.BorderThickness = 1;
            mainPanel.Controls.Add(durationLabel);
            mainPanel.Controls.Add(sessionNameLabel);
            mainPanel.Controls.Add(homeButton);
            mainPanel.Controls.Add(elapsedTimerMinimizeButton);
            mainPanel.Controls.Add(closeButton);
            mainPanel.Controls.Add(controlPanel);
            customizableEdges31.BottomRight = false;
            customizableEdges31.TopLeft = false;
            mainPanel.CustomizableEdges = customizableEdges31;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(10);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges32;
            mainPanel.Size = new Size(320, 400);
            mainPanel.TabIndex = 0;
            // 
            // durationLabel
            // 
            durationLabel.AutoSize = false;
            durationLabel.BackColor = Color.Transparent;
            durationLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            durationLabel.ForeColor = Color.White;
            durationLabel.Location = new Point(60, 130);
            durationLabel.Name = "durationLabel";
            durationLabel.Size = new Size(185, 55);
            durationLabel.TabIndex = 12;
            durationLabel.Text = "00 : 00 : 00";
            durationLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // sessionNameLabel
            // 
            sessionNameLabel.BackColor = Color.Transparent;
            sessionNameLabel.Font = new Font("Segoe UI", 10F);
            sessionNameLabel.ForeColor = Color.FromArgb(180, 180, 180);
            sessionNameLabel.Location = new Point(97, 242);
            sessionNameLabel.Name = "sessionNameLabel";
            sessionNameLabel.Size = new Size(115, 19);
            sessionNameLabel.TabIndex = 3;
            sessionNameLabel.Text = "My Coding Session";
            sessionNameLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // homeButton
            // 
            homeButton.CustomizableEdges = customizableEdges17;
            homeButton.FillColor = Color.FromArgb(25, 24, 40);
            homeButton.FillColor2 = Color.FromArgb(25, 24, 40);
            homeButton.Font = new Font("Segoe UI", 9F);
            homeButton.ForeColor = Color.White;
            homeButton.Image = Properties.Resources.HomeButtonIcon;
            homeButton.Location = new Point(245, 0);
            homeButton.Name = "homeButton";
            homeButton.ShadowDecoration.CustomizableEdges = customizableEdges18;
            homeButton.Size = new Size(27, 21);
            homeButton.TabIndex = 11;
            homeButton.Click += homeButton_Click;
            // 
            // elapsedTimerMinimizeButton
            // 
            elapsedTimerMinimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            elapsedTimerMinimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            elapsedTimerMinimizeButton.CustomizableEdges = customizableEdges19;
            elapsedTimerMinimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            elapsedTimerMinimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            elapsedTimerMinimizeButton.HoverState.IconColor = Color.White;
            elapsedTimerMinimizeButton.IconColor = Color.White;
            elapsedTimerMinimizeButton.Location = new Point(272, 0);
            elapsedTimerMinimizeButton.Name = "elapsedTimerMinimizeButton";
            elapsedTimerMinimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges20;
            elapsedTimerMinimizeButton.Size = new Size(27, 21);
            elapsedTimerMinimizeButton.TabIndex = 9;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BorderRadius = 12;
            closeButton.CustomClick = true;
            customizableEdges21.BottomLeft = false;
            customizableEdges21.BottomRight = false;
            customizableEdges21.TopLeft = false;
            closeButton.CustomizableEdges = customizableEdges21;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.White;
            closeButton.Location = new Point(293, 0);
            closeButton.Name = "closeButton";
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges22;
            closeButton.Size = new Size(27, 21);
            closeButton.TabIndex = 10;
            closeButton.Click += CloseButton_Click;
            // 
            // controlPanel
            // 
            controlPanel.BackColor = Color.Transparent;
            controlPanel.Controls.Add(elapsedTimerPauseButton);
            controlPanel.Controls.Add(restartSessionButton);
            controlPanel.Controls.Add(elapsedPageStopButton);
            controlPanel.CustomizableEdges = customizableEdges29;
            controlPanel.FillColor = Color.Transparent;
            controlPanel.Location = new Point(20, 290);
            controlPanel.Name = "controlPanel";
            controlPanel.ShadowDecoration.CustomizableEdges = customizableEdges30;
            controlPanel.Size = new Size(280, 80);
            controlPanel.TabIndex = 4;
            // 
            // elapsedTimerPauseButton
            // 
            elapsedTimerPauseButton.BackColor = Color.Transparent;
            elapsedTimerPauseButton.BorderColor = Color.FromArgb(170, 60, 130);
            elapsedTimerPauseButton.BorderRadius = 24;
            elapsedTimerPauseButton.CustomizableEdges = customizableEdges23;
            elapsedTimerPauseButton.DisabledState.BorderColor = Color.DarkGray;
            elapsedTimerPauseButton.DisabledState.CustomBorderColor = Color.DarkGray;
            elapsedTimerPauseButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            elapsedTimerPauseButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            elapsedTimerPauseButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            elapsedTimerPauseButton.FillColor = Color.FromArgb(170, 60, 130);
            elapsedTimerPauseButton.FillColor2 = Color.FromArgb(100, 170, 200);
            elapsedTimerPauseButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            elapsedTimerPauseButton.ForeColor = Color.White;
            elapsedTimerPauseButton.Location = new Point(40, 16);
            elapsedTimerPauseButton.Name = "elapsedTimerPauseButton";
            elapsedTimerPauseButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            elapsedTimerPauseButton.ShadowDecoration.CustomizableEdges = customizableEdges24;
            elapsedTimerPauseButton.ShadowDecoration.Depth = 5;
            elapsedTimerPauseButton.ShadowDecoration.Enabled = true;
            elapsedTimerPauseButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            elapsedTimerPauseButton.Size = new Size(48, 48);
            elapsedTimerPauseButton.TabIndex = 5;
            elapsedTimerPauseButton.Text = "⏸";
            elapsedTimerPauseButton.TextOffset = new Point(3, 0);
            elapsedTimerPauseButton.Click += PauseButton_Click;
            // 
            // restartSessionButton
            // 
            restartSessionButton.BackColor = Color.Transparent;
            restartSessionButton.BorderRadius = 24;
            restartSessionButton.CustomizableEdges = customizableEdges25;
            restartSessionButton.DisabledState.BorderColor = Color.DarkGray;
            restartSessionButton.DisabledState.CustomBorderColor = Color.DarkGray;
            restartSessionButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            restartSessionButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            restartSessionButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            restartSessionButton.FillColor = Color.FromArgb(170, 60, 130);
            restartSessionButton.FillColor2 = Color.FromArgb(168, 228, 255);
            restartSessionButton.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            restartSessionButton.ForeColor = Color.White;
            restartSessionButton.Location = new Point(192, 16);
            restartSessionButton.Name = "restartSessionButton";
            restartSessionButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            restartSessionButton.ShadowDecoration.CustomizableEdges = customizableEdges26;
            restartSessionButton.ShadowDecoration.Depth = 5;
            restartSessionButton.ShadowDecoration.Enabled = true;
            restartSessionButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            restartSessionButton.Size = new Size(48, 48);
            restartSessionButton.TabIndex = 7;
            restartSessionButton.Text = "⟳";
            restartSessionButton.TextOffset = new Point(3, 0);
            restartSessionButton.Click += RestartSessionButton_Click;
            // 
            // elapsedPageStopButton
            // 
            elapsedPageStopButton.BackColor = Color.Transparent;
            elapsedPageStopButton.BorderColor = Color.FromArgb(170, 60, 130);
            elapsedPageStopButton.BorderRadius = 24;
            elapsedPageStopButton.CustomizableEdges = customizableEdges27;
            elapsedPageStopButton.DisabledState.BorderColor = Color.DarkGray;
            elapsedPageStopButton.DisabledState.CustomBorderColor = Color.DarkGray;
            elapsedPageStopButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            elapsedPageStopButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            elapsedPageStopButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            elapsedPageStopButton.FillColor = Color.FromArgb(170, 60, 130);
            elapsedPageStopButton.FillColor2 = Color.FromArgb(100, 170, 200);
            elapsedPageStopButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            elapsedPageStopButton.ForeColor = Color.White;
            elapsedPageStopButton.Location = new Point(116, 16);
            elapsedPageStopButton.Name = "elapsedPageStopButton";
            elapsedPageStopButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            elapsedPageStopButton.ShadowDecoration.CustomizableEdges = customizableEdges28;
            elapsedPageStopButton.ShadowDecoration.Depth = 5;
            elapsedPageStopButton.ShadowDecoration.Enabled = true;
            elapsedPageStopButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            elapsedPageStopButton.Size = new Size(48, 48);
            elapsedPageStopButton.TabIndex = 8;
            elapsedPageStopButton.Text = "■";
            elapsedPageStopButton.TextOffset = new Point(2, 0);
            elapsedPageStopButton.Click += StopButton_Click;
            // 
            // winFormsTimer
            // 
            winFormsTimer.Interval = 50;
            winFormsTimer.Tick += WinFormsTimer_Tick;
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
            // timeDisplayLabel
            // 
            timeDisplayLabel.BackColor = Color.Transparent;
            timeDisplayLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            timeDisplayLabel.ForeColor = Color.White;
            timeDisplayLabel.Location = new Point(58, 89);
            timeDisplayLabel.Name = "timeDisplayLabel";
            timeDisplayLabel.Size = new Size(141, 39);
            timeDisplayLabel.TabIndex = 2;
            timeDisplayLabel.Text = "00 : 00 : 00";
            // 
            // elapsedTimeLabel
            // 
            elapsedTimeLabel.AutoSize = false;
            elapsedTimeLabel.BackColor = Color.Transparent;
            elapsedTimeLabel.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            elapsedTimeLabel.ForeColor = Color.White;
            elapsedTimeLabel.Location = new Point(70, 148);
            elapsedTimeLabel.Name = "elapsedTimeLabel";
            elapsedTimeLabel.Size = new Size(80, 24);
            elapsedTimeLabel.TabIndex = 3;
            elapsedTimeLabel.Text = "Elapsed";
            elapsedTimeLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // ElapsedTimerPage
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(320, 400);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ElapsedTimerPage";
            StartPosition = FormStartPosition.Manual;
            Load += ElapsedTimerForm_Load;
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            controlPanel.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Guna2ControlBox closeButton;
        private Guna2ControlBox elapsedTimerMinimizeButton;
        private Guna2GradientButton homeButton;
        private Guna2AnimateWindow gunaAnimationWindow;
        private Guna2GradientButton elapsedPageStopButton;
        private Guna2HtmlLabel timeDisplayLabel;
        private Guna2HtmlLabel elapsedTimeLabel;
        private Guna2HtmlLabel durationLabel;
    }
}