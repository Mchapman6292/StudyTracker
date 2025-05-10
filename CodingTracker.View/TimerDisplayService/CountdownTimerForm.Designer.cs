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
        private System.Windows.Forms.Timer progressTimer;
        private Guna2BorderlessForm borderlessForm;
        private Guna2Panel mainPanel;
        private Guna2Button pauseButton;
        private Guna2HtmlLabel statusLabel;
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
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2Panel();
            homeButton = new Guna2GradientButton();
            minimizeButton = new Guna2ControlBox();
            closeButton = new Guna2ControlBox();
            progressBar = new Guna2CircleProgressBar();
            statusLabel = new Guna2HtmlLabel();
            pauseButton = new Guna2Button();
            progressTimer = new System.Windows.Forms.Timer(components);
            DisplayMessageBox = new Guna2MessageDialog();
            mainPanel.SuspendLayout();
            progressBar.SuspendLayout();
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
            mainPanel.Controls.Add(homeButton);
            mainPanel.Controls.Add(minimizeButton);
            mainPanel.Controls.Add(closeButton);
            mainPanel.Controls.Add(progressBar);
            mainPanel.Controls.Add(pauseButton);
            customizableEdges10.BottomRight = false;
            customizableEdges10.TopLeft = false;
            mainPanel.CustomizableEdges = customizableEdges10;
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(10);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges11;
            mainPanel.Size = new Size(220, 220);
            mainPanel.TabIndex = 0;
            // 
            // homeButton
            // 
            customizableEdges1.BottomRight = false;
            customizableEdges1.TopRight = false;
            homeButton.CustomizableEdges = customizableEdges1;
            homeButton.FillColor = Color.FromArgb(25, 24, 40);
            homeButton.FillColor2 = Color.FromArgb(25, 24, 40);
            homeButton.Font = new Font("Segoe UI", 9F);
            homeButton.ForeColor = Color.White;
            homeButton.Image = Properties.Resources.HomeButtonIcon;
            homeButton.Location = new Point(143, 0);
            homeButton.Name = "homeButton";
            customizableEdges2.BottomLeft = false;
            customizableEdges2.TopLeft = false;
            homeButton.ShadowDecoration.CustomizableEdges = customizableEdges2;
            homeButton.Size = new Size(33, 21);
            homeButton.TabIndex = 11;
            homeButton.Click += homeButton_Click;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            customizableEdges3.BottomRight = false;
            customizableEdges3.TopLeft = false;
            customizableEdges3.TopRight = false;
            minimizeButton.CustomizableEdges = customizableEdges3;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.White;
            minimizeButton.Location = new Point(173, 0);
            minimizeButton.Name = "minimizeButton";
            customizableEdges4.BottomLeft = false;
            customizableEdges4.TopLeft = false;
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            minimizeButton.Size = new Size(23, 21);
            minimizeButton.TabIndex = 9;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.BorderRadius = 12;
            closeButton.CustomClick = true;
            customizableEdges5.BottomLeft = false;
            customizableEdges5.TopLeft = false;
            closeButton.CustomizableEdges = customizableEdges5;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.White;
            closeButton.Location = new Point(193, 0);
            closeButton.Name = "closeButton";
            customizableEdges6.BottomRight = false;
            customizableEdges6.TopRight = false;
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            closeButton.Size = new Size(27, 21);
            closeButton.TabIndex = 10;
            closeButton.Click += CloseButton_Click;
            // 
            // progressBar
            // 
            progressBar.AnimationSpeed = 0.5F;
            progressBar.BackColor = Color.Transparent;
            progressBar.Controls.Add(statusLabel);
            progressBar.FillColor = Color.FromArgb(45, 46, 50);
            progressBar.FillThickness = 15;
            progressBar.Font = new Font("Segoe UI", 12F);
            progressBar.ForeColor = Color.White;
            progressBar.Location = new Point(25, 27);
            progressBar.Minimum = 0;
            progressBar.Name = "progressBar";
            progressBar.ProgressColor = Color.FromArgb(255, 81, 195);
            progressBar.ProgressColor2 = Color.FromArgb(168, 228, 255);
            progressBar.ProgressThickness = 15;
            progressBar.ShadowDecoration.CustomizableEdges = customizableEdges7;
            progressBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            progressBar.Size = new Size(170, 170);
            progressBar.TabIndex = 0;
            // 
            // statusLabel
            // 
            statusLabel.AutoSize = false;
            statusLabel.BackColor = Color.Transparent;
            statusLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            statusLabel.ForeColor = Color.White;
            statusLabel.Location = new Point(10, 70);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(150, 30);
            statusLabel.TabIndex = 0;
            statusLabel.Text = "0%";
            statusLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // pauseButton
            // 
            pauseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pauseButton.BorderRadius = 12;
            pauseButton.Cursor = Cursors.Hand;
            customizableEdges8.BottomRight = false;
            customizableEdges8.TopLeft = false;
            pauseButton.CustomizableEdges = customizableEdges8;
            pauseButton.FillColor = Color.FromArgb(70, 71, 117);
            pauseButton.Font = new Font("Segoe UI", 9F);
            pauseButton.ForeColor = Color.White;
            pauseButton.Image = Properties.Resources.pause;
            pauseButton.Location = new Point(0, 195);
            pauseButton.Name = "pauseButton";
            pauseButton.ShadowDecoration.CustomizableEdges = customizableEdges9;
            pauseButton.Size = new Size(25, 25);
            pauseButton.TabIndex = 3;
            pauseButton.Click += PauseButton_Click;
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
            ClientSize = new Size(220, 220);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "CountdownTimerForm";
            StartPosition = FormStartPosition.Manual;
            Load += CountdownTimerForm_Load;
            mainPanel.ResumeLayout(false);
            progressBar.ResumeLayout(false);
            ResumeLayout(false);
        }
        #endregion

        private Guna2ControlBox closeButton;
        private Guna2ControlBox minimizeButton;
        private Guna2GradientButton homeButton;
    }
}