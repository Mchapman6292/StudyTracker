using CodingTracker.View.Forms.Services.SharedFormServices.CustomGradientButtons;
using Guna.UI2.WinForms;
using Guna.UI2.WinForms.Suite;
using LiveCharts;
using LiveCharts.Defaults;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp.Views.Desktop;

namespace CodingTracker.View.Forms
{
    partial class AnimatedTimerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        // Control declarations
        private Guna.UI2.WinForms.Guna2BorderlessForm borderlessForm;
        private Guna.UI2.WinForms.Guna2GradientPanel mainPanel;
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
            CustomizableEdges customizableEdges31 = new CustomizableEdges();
            CustomizableEdges customizableEdges32 = new CustomizableEdges();
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
            CustomizableEdges customizableEdges17 = new CustomizableEdges();
            CustomizableEdges customizableEdges18 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2GradientPanel();
            homeButton = new FontAwesome.Sharp.IconPictureBox();
            minimizeButton = new Guna2ControlBox();
            exitButton = new Guna2ControlBox();
            controlPanel = new Guna2Panel();
            pauseButton = new CustomGradientButton();
            restartButton = new CustomGradientButton();
            stopButton = new CustomGradientButton();
            timeDisplayLabel = new Guna2HtmlLabel();
            animatedTimerSKControl = new SKControl();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            iconPictureBox1 = new FontAwesome.Sharp.IconPictureBox();
            guna2HtmlLabel6 = new Guna2HtmlLabel();
            nameDisplayTextBox = new Guna2TextBox();
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)homeButton).BeginInit();
            controlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).BeginInit();
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
            mainPanel.Controls.Add(nameDisplayTextBox);
            mainPanel.Controls.Add(iconPictureBox1);
            mainPanel.Controls.Add(guna2HtmlLabel6);
            mainPanel.Controls.Add(homeButton);
            mainPanel.Controls.Add(minimizeButton);
            mainPanel.Controls.Add(exitButton);
            mainPanel.Controls.Add(controlPanel);
            mainPanel.Controls.Add(timeDisplayLabel);
            mainPanel.Controls.Add(animatedTimerSKControl);
            mainPanel.CustomizableEdges = customizableEdges31;
            mainPanel.FillColor = Color.FromArgb(26, 26, 46);
            mainPanel.FillColor2 = Color.FromArgb(26, 26, 46);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges32;
            mainPanel.ShadowDecoration.Depth = 20;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(3, 3, 7, 7);
            mainPanel.Size = new Size(699, 749);
            mainPanel.TabIndex = 0;
            // 
            // homeButton
            // 
            homeButton.BackColor = Color.FromArgb(25, 24, 40);
            homeButton.ForeColor = Color.FromArgb(255, 160, 210);
            homeButton.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            homeButton.IconColor = Color.FromArgb(255, 160, 210);
            homeButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            homeButton.IconSize = 34;
            homeButton.Location = new Point(567, 0);
            homeButton.Margin = new Padding(3, 2, 3, 2);
            homeButton.Name = "homeButton";
            homeButton.Size = new Size(45, 34);
            homeButton.SizeMode = PictureBoxSizeMode.CenterImage;
            homeButton.TabIndex = 47;
            homeButton.TabStop = false;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeButton.Cursor = Cursors.Hand;
            minimizeButton.CustomizableEdges = customizableEdges19;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.FromArgb(255, 160, 210);
            minimizeButton.Location = new Point(610, 0);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges20;
            minimizeButton.Size = new Size(45, 34);
            minimizeButton.TabIndex = 28;
            minimizeButton.Click += minimizeButton_Click;
            // 
            // exitButton
            // 
            exitButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            exitButton.Cursor = Cursors.Hand;
            exitButton.CustomClick = true;
            exitButton.CustomizableEdges = customizableEdges21;
            exitButton.FillColor = Color.FromArgb(25, 24, 40);
            exitButton.HoverState.IconColor = Color.White;
            exitButton.IconColor = Color.FromArgb(255, 160, 210);
            exitButton.Location = new Point(652, 0);
            exitButton.Name = "exitButton";
            exitButton.ShadowDecoration.CustomizableEdges = customizableEdges22;
            exitButton.Size = new Size(45, 34);
            exitButton.TabIndex = 27;
            exitButton.Click += ExitButton_Click;
            // 
            // controlPanel
            // 
            controlPanel.BackColor = Color.Transparent;
            controlPanel.Controls.Add(pauseButton);
            controlPanel.Controls.Add(restartButton);
            controlPanel.Controls.Add(stopButton);
            controlPanel.CustomizableEdges = customizableEdges29;
            controlPanel.FillColor = Color.Transparent;
            controlPanel.Location = new Point(158, 678);
            controlPanel.Name = "controlPanel";
            controlPanel.ShadowDecoration.CustomizableEdges = customizableEdges30;
            controlPanel.Size = new Size(280, 59);
            controlPanel.TabIndex = 9;
            // 
            // pauseButton
            // 
            pauseButton.Animated = true;
            pauseButton.BackColor = Color.Transparent;
            pauseButton.BorderColor = Color.FromArgb(170, 60, 130);
            pauseButton.BorderRadius = 24;
            pauseButton.CustomizableEdges = customizableEdges23;
            pauseButton.DisabledState.BorderColor = Color.DarkGray;
            pauseButton.DisabledState.CustomBorderColor = Color.DarkGray;
            pauseButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            pauseButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            pauseButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            pauseButton.EnableHoverRipple = true;
            pauseButton.FillColor = Color.FromArgb(170, 60, 130);
            pauseButton.FillColor2 = Color.FromArgb(100, 170, 200);
            pauseButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pauseButton.ForeColor = Color.White;
            pauseButton.Location = new Point(32, 8);
            pauseButton.Name = "pauseButton";
            pauseButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            pauseButton.ShadowDecoration.CustomizableEdges = customizableEdges24;
            pauseButton.ShadowDecoration.Depth = 5;
            pauseButton.ShadowDecoration.Enabled = true;
            pauseButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            pauseButton.Size = new Size(48, 48);
            pauseButton.TabIndex = 5;
            pauseButton.Text = "⏸";
            pauseButton.TextOffset = new Point(3, 0);
            // 
            // restartButton
            // 
            restartButton.Animated = true;
            restartButton.BackColor = Color.Transparent;
            restartButton.BorderRadius = 24;
            restartButton.CustomizableEdges = customizableEdges25;
            restartButton.DisabledState.BorderColor = Color.DarkGray;
            restartButton.DisabledState.CustomBorderColor = Color.DarkGray;
            restartButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            restartButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            restartButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            restartButton.EnableHoverRipple = true;
            restartButton.FillColor = Color.FromArgb(170, 60, 130);
            restartButton.FillColor2 = Color.FromArgb(168, 228, 255);
            restartButton.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point, 0);
            restartButton.ForeColor = Color.White;
            restartButton.Location = new Point(184, 8);
            restartButton.Name = "restartButton";
            restartButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            restartButton.ShadowDecoration.CustomizableEdges = customizableEdges26;
            restartButton.ShadowDecoration.Depth = 5;
            restartButton.ShadowDecoration.Enabled = true;
            restartButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            restartButton.Size = new Size(48, 48);
            restartButton.TabIndex = 7;
            restartButton.Text = "⟳";
            restartButton.TextOffset = new Point(3, 0);
            restartButton.Click += RestartButton_Click;
            // 
            // stopButton
            // 
            stopButton.Animated = true;
            stopButton.BackColor = Color.Transparent;
            stopButton.BorderColor = Color.FromArgb(170, 60, 130);
            stopButton.BorderRadius = 24;
            stopButton.CustomizableEdges = customizableEdges27;
            stopButton.DisabledState.BorderColor = Color.DarkGray;
            stopButton.DisabledState.CustomBorderColor = Color.DarkGray;
            stopButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            stopButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            stopButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            stopButton.EnableHoverRipple = true;
            stopButton.FillColor = Color.FromArgb(170, 60, 130);
            stopButton.FillColor2 = Color.FromArgb(100, 170, 200);
            stopButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stopButton.ForeColor = Color.White;
            stopButton.Location = new Point(108, 8);
            stopButton.Name = "stopButton";
            stopButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            stopButton.ShadowDecoration.CustomizableEdges = customizableEdges28;
            stopButton.ShadowDecoration.Depth = 5;
            stopButton.ShadowDecoration.Enabled = true;
            stopButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            stopButton.Size = new Size(48, 48);
            stopButton.TabIndex = 8;
            stopButton.Text = "■";
            stopButton.TextOffset = new Point(2, 0);
            // 
            // timeDisplayLabel
            // 
            timeDisplayLabel.BackColor = Color.Transparent;
            timeDisplayLabel.Font = new Font("Segoe UI", 13F);
            timeDisplayLabel.ForeColor = Color.HotPink;
            timeDisplayLabel.Location = new Point(499, 686);
            timeDisplayLabel.Name = "timeDisplayLabel";
            timeDisplayLabel.Size = new Size(137, 25);
            timeDisplayLabel.TabIndex = 8;
            timeDisplayLabel.Text = "guna2HtmlLabel1";
            // 
            // animatedTimerSKControl
            // 
            animatedTimerSKControl.BackColor = Color.FromArgb(26, 26, 46);
            animatedTimerSKControl.ForeColor = Color.FromArgb(50, 49, 65);
            animatedTimerSKControl.Location = new Point(65, 81);
            animatedTimerSKControl.Name = "animatedTimerSKControl";
            animatedTimerSKControl.Size = new Size(448, 590);
            animatedTimerSKControl.TabIndex = 0;
            // 
            // iconPictureBox1
            // 
            iconPictureBox1.BackColor = Color.FromArgb(35, 34, 50);
            iconPictureBox1.ForeColor = Color.FromArgb(255, 160, 210);
            iconPictureBox1.IconChar = FontAwesome.Sharp.IconChar.CodePullRequest;
            iconPictureBox1.IconColor = Color.FromArgb(255, 160, 210);
            iconPictureBox1.IconFont = FontAwesome.Sharp.IconFont.Auto;
            iconPictureBox1.IconSize = 34;
            iconPictureBox1.Location = new Point(212, 0);
            iconPictureBox1.Margin = new Padding(3, 2, 3, 2);
            iconPictureBox1.Name = "iconPictureBox1";
            iconPictureBox1.Size = new Size(35, 34);
            iconPictureBox1.TabIndex = 49;
            iconPictureBox1.TabStop = false;
            // 
            // guna2HtmlLabel6
            // 
            guna2HtmlLabel6.Anchor = AnchorStyles.None;
            guna2HtmlLabel6.BackColor = Color.Transparent;
            guna2HtmlLabel6.Font = new Font("Century Gothic", 20F, FontStyle.Bold);
            guna2HtmlLabel6.ForeColor = Color.FromArgb(204, 84, 144);
            guna2HtmlLabel6.Location = new Point(252, 0);
            guna2HtmlLabel6.Name = "guna2HtmlLabel6";
            guna2HtmlLabel6.Size = new Size(196, 34);
            guna2HtmlLabel6.TabIndex = 48;
            guna2HtmlLabel6.Text = "CodingTracker";
            guna2HtmlLabel6.TextAlignment = ContentAlignment.TopCenter;
            // 
            // nameDisplayTextBox
            // 
            nameDisplayTextBox.BorderColor = Color.FromArgb(26, 26, 46);
            nameDisplayTextBox.CustomizableEdges = customizableEdges17;
            nameDisplayTextBox.DefaultText = "MChapman";
            nameDisplayTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            nameDisplayTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            nameDisplayTextBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            nameDisplayTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            nameDisplayTextBox.FillColor = Color.FromArgb(26, 26, 46);
            nameDisplayTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            nameDisplayTextBox.Font = new Font("Segoe UI", 10F);
            nameDisplayTextBox.ForeColor = Color.FromArgb(255, 200, 230);
            nameDisplayTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            nameDisplayTextBox.Location = new Point(238, 44);
            nameDisplayTextBox.Name = "nameDisplayTextBox";
            nameDisplayTextBox.PlaceholderText = "";
            nameDisplayTextBox.SelectedText = "";
            nameDisplayTextBox.ShadowDecoration.CustomizableEdges = customizableEdges18;
            nameDisplayTextBox.Size = new Size(200, 41);
            nameDisplayTextBox.TabIndex = 50;
            // 
            // AnimatedTimerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(699, 749);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AnimatedTimerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Modern Timer Test";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)homeButton).EndInit();
            controlPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)iconPictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private CustomGradientButton testButton;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private Services.MainPageService.RecentActivityService.Panels.DurationPanel durationPanel1;


        private SKControl animatedTimerSKControl;
        private Guna2HtmlLabel timeDisplayLabel;
        private Guna2Panel controlPanel;
        private CustomGradientButton pauseButton;
        private CustomGradientButton restartButton;
        private CustomGradientButton stopButton;
        private Guna2ControlBox minimizeButton;
        private Guna2ControlBox exitButton;
        private FontAwesome.Sharp.IconPictureBox homeButton;
        private FontAwesome.Sharp.IconPictureBox iconPictureBox1;
        private Guna2HtmlLabel guna2HtmlLabel6;
        private Guna2TextBox nameDisplayTextBox;
    }
}