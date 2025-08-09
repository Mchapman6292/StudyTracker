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
            CustomizableEdges customizableEdges19 = new CustomizableEdges();
            CustomizableEdges customizableEdges20 = new CustomizableEdges();
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges17 = new CustomizableEdges();
            CustomizableEdges customizableEdges18 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            CustomizableEdges customizableEdges13 = new CustomizableEdges();
            CustomizableEdges customizableEdges14 = new CustomizableEdges();
            CustomizableEdges customizableEdges15 = new CustomizableEdges();
            CustomizableEdges customizableEdges16 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2GradientPanel();
            elapsedTestToggleSwitch = new Guna2ToggleSwitch();
            elapsedTestTextBox = new Guna2TextBox();
            nameDisplayTextBox = new Guna2TextBox();
            codingTrackerSymbol = new FontAwesome.Sharp.IconPictureBox();
            codingTrackerDisplayLabel = new Guna2HtmlLabel();
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
            animatedTimerFormDragControl = new Guna2DragControl(components);
            mainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)codingTrackerSymbol).BeginInit();
            ((System.ComponentModel.ISupportInitialize)homeButton).BeginInit();
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
            mainPanel.Anchor = AnchorStyles.None;
            mainPanel.BackColor = Color.Transparent;
            mainPanel.BorderColor = Color.FromArgb(225, 225, 225);
            mainPanel.BorderRadius = 12;
            mainPanel.Controls.Add(elapsedTestToggleSwitch);
            mainPanel.Controls.Add(elapsedTestTextBox);
            mainPanel.Controls.Add(nameDisplayTextBox);
            mainPanel.Controls.Add(codingTrackerSymbol);
            mainPanel.Controls.Add(codingTrackerDisplayLabel);
            mainPanel.Controls.Add(homeButton);
            mainPanel.Controls.Add(minimizeButton);
            mainPanel.Controls.Add(exitButton);
            mainPanel.Controls.Add(controlPanel);
            mainPanel.Controls.Add(timeDisplayLabel);
            mainPanel.Controls.Add(animatedTimerSKControl);
            mainPanel.CustomizableEdges = customizableEdges19;
            mainPanel.FillColor = Color.FromArgb(26, 26, 46);
            mainPanel.FillColor2 = Color.FromArgb(26, 26, 46);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges20;
            mainPanel.ShadowDecoration.Depth = 20;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(3, 3, 7, 7);
            mainPanel.Size = new Size(511, 749);
            mainPanel.TabIndex = 0;
            // 
            // elapsedTestToggleSwitch
            // 
            elapsedTestToggleSwitch.CheckedState.BorderColor = Color.FromArgb(94, 148, 255);
            elapsedTestToggleSwitch.CheckedState.FillColor = Color.FromArgb(94, 148, 255);
            elapsedTestToggleSwitch.CheckedState.InnerBorderColor = Color.White;
            elapsedTestToggleSwitch.CheckedState.InnerColor = Color.White;
            elapsedTestToggleSwitch.CustomizableEdges = customizableEdges1;
            elapsedTestToggleSwitch.Location = new Point(3, 10);
            elapsedTestToggleSwitch.Name = "elapsedTestToggleSwitch";
            elapsedTestToggleSwitch.ShadowDecoration.CustomizableEdges = customizableEdges2;
            elapsedTestToggleSwitch.Size = new Size(35, 20);
            elapsedTestToggleSwitch.TabIndex = 52;
            elapsedTestToggleSwitch.UncheckedState.BorderColor = Color.FromArgb(125, 137, 149);
            elapsedTestToggleSwitch.UncheckedState.FillColor = Color.FromArgb(125, 137, 149);
            elapsedTestToggleSwitch.UncheckedState.InnerBorderColor = Color.White;
            elapsedTestToggleSwitch.UncheckedState.InnerColor = Color.White;
            // 
            // elapsedTestTextBox
            // 
            elapsedTestTextBox.CustomizableEdges = customizableEdges3;
            elapsedTestTextBox.DefaultText = "";
            elapsedTestTextBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            elapsedTestTextBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            elapsedTestTextBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            elapsedTestTextBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            elapsedTestTextBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            elapsedTestTextBox.Font = new Font("Segoe UI", 9F);
            elapsedTestTextBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            elapsedTestTextBox.Location = new Point(57, 3);
            elapsedTestTextBox.Name = "elapsedTestTextBox";
            elapsedTestTextBox.PlaceholderText = "";
            elapsedTestTextBox.SelectedText = "";
            elapsedTestTextBox.ShadowDecoration.CustomizableEdges = customizableEdges4;
            elapsedTestTextBox.Size = new Size(63, 36);
            elapsedTestTextBox.TabIndex = 51;
            // 
            // nameDisplayTextBox
            // 
            nameDisplayTextBox.AutoSize = true;
            nameDisplayTextBox.BorderColor = Color.FromArgb(26, 26, 46);
            nameDisplayTextBox.CustomizableEdges = customizableEdges5;
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
            nameDisplayTextBox.Location = new Point(199, 36);
            nameDisplayTextBox.Margin = new Padding(4, 3, 4, 3);
            nameDisplayTextBox.Name = "nameDisplayTextBox";
            nameDisplayTextBox.PlaceholderText = "";
            nameDisplayTextBox.SelectedText = "";
            nameDisplayTextBox.ShadowDecoration.CustomizableEdges = customizableEdges6;
            nameDisplayTextBox.Size = new Size(85, 35);
            nameDisplayTextBox.TabIndex = 50;
            // 
            // codingTrackerSymbol
            // 
            codingTrackerSymbol.BackColor = Color.FromArgb(35, 34, 50);
            codingTrackerSymbol.ForeColor = Color.FromArgb(255, 160, 210);
            codingTrackerSymbol.IconChar = FontAwesome.Sharp.IconChar.CodePullRequest;
            codingTrackerSymbol.IconColor = Color.FromArgb(255, 160, 210);
            codingTrackerSymbol.IconFont = FontAwesome.Sharp.IconFont.Auto;
            codingTrackerSymbol.IconSize = 27;
            codingTrackerSymbol.Location = new Point(132, 3);
            codingTrackerSymbol.Margin = new Padding(3, 2, 3, 2);
            codingTrackerSymbol.Name = "codingTrackerSymbol";
            codingTrackerSymbol.Size = new Size(35, 27);
            codingTrackerSymbol.SizeMode = PictureBoxSizeMode.CenterImage;
            codingTrackerSymbol.TabIndex = 49;
            codingTrackerSymbol.TabStop = false;
            // 
            // codingTrackerDisplayLabel
            // 
            codingTrackerDisplayLabel.Anchor = AnchorStyles.None;
            codingTrackerDisplayLabel.BackColor = Color.Transparent;
            codingTrackerDisplayLabel.Font = new Font("Century Gothic", 16F, FontStyle.Bold);
            codingTrackerDisplayLabel.ForeColor = Color.FromArgb(204, 84, 144);
            codingTrackerDisplayLabel.Location = new Point(173, 3);
            codingTrackerDisplayLabel.Name = "codingTrackerDisplayLabel";
            codingTrackerDisplayLabel.Size = new Size(153, 27);
            codingTrackerDisplayLabel.TabIndex = 48;
            codingTrackerDisplayLabel.Text = "CodingTracker";
            codingTrackerDisplayLabel.TextAlignment = ContentAlignment.TopCenter;
            // 
            // homeButton
            // 
            homeButton.BackColor = Color.FromArgb(25, 24, 40);
            homeButton.ForeColor = Color.FromArgb(255, 160, 210);
            homeButton.IconChar = FontAwesome.Sharp.IconChar.HomeLg;
            homeButton.IconColor = Color.FromArgb(255, 160, 210);
            homeButton.IconFont = FontAwesome.Sharp.IconFont.Auto;
            homeButton.IconSize = 30;
            homeButton.Location = new Point(399, 0);
            homeButton.Margin = new Padding(3, 2, 3, 2);
            homeButton.Name = "homeButton";
            homeButton.Size = new Size(40, 30);
            homeButton.SizeMode = PictureBoxSizeMode.CenterImage;
            homeButton.TabIndex = 47;
            homeButton.TabStop = false;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeButton.Cursor = Cursors.Hand;
            minimizeButton.CustomizableEdges = customizableEdges7;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.FromArgb(255, 160, 210);
            minimizeButton.Location = new Point(435, -3);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
            minimizeButton.Size = new Size(40, 30);
            minimizeButton.TabIndex = 28;
            minimizeButton.Click += minimizeButton_Click;
            // 
            // exitButton
            // 
            exitButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            exitButton.Cursor = Cursors.Hand;
            exitButton.CustomClick = true;
            exitButton.CustomizableEdges = customizableEdges9;
            exitButton.FillColor = Color.FromArgb(25, 24, 40);
            exitButton.HoverState.IconColor = Color.White;
            exitButton.IconColor = Color.FromArgb(255, 160, 210);
            exitButton.Location = new Point(471, -3);
            exitButton.Name = "exitButton";
            exitButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
            exitButton.Size = new Size(40, 30);
            exitButton.TabIndex = 27;
            exitButton.Click += ExitButton_Click;
            // 
            // controlPanel
            // 
            controlPanel.BackColor = Color.Transparent;
            controlPanel.Controls.Add(pauseButton);
            controlPanel.Controls.Add(restartButton);
            controlPanel.Controls.Add(stopButton);
            controlPanel.CustomizableEdges = customizableEdges17;
            controlPanel.FillColor = Color.Transparent;
            controlPanel.Location = new Point(41, 687);
            controlPanel.Name = "controlPanel";
            controlPanel.ShadowDecoration.CustomizableEdges = customizableEdges18;
            controlPanel.Size = new Size(444, 50);
            controlPanel.TabIndex = 9;
            // 
            // pauseButton
            // 
            pauseButton.Animated = true;
            pauseButton.BackColor = Color.Transparent;
            pauseButton.BorderColor = Color.FromArgb(170, 60, 130);
            pauseButton.BorderRadius = 24;
            pauseButton.CustomizableEdges = customizableEdges11;
            pauseButton.DisabledState.BorderColor = Color.DarkGray;
            pauseButton.DisabledState.CustomBorderColor = Color.DarkGray;
            pauseButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            pauseButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            pauseButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            pauseButton.EnableHoverRipple = true;
            pauseButton.FillColor = Color.FromArgb(255, 81, 195);
            pauseButton.FillColor2 = Color.FromArgb(168, 228, 255);
            pauseButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            pauseButton.ForeColor = Color.White;
            pauseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.BackwardDiagonal;
            pauseButton.Location = new Point(16, 11);
            pauseButton.Name = "pauseButton";
            pauseButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            pauseButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
            pauseButton.ShadowDecoration.Depth = 5;
            pauseButton.ShadowDecoration.Enabled = true;
            pauseButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            pauseButton.Size = new Size(103, 36);
            pauseButton.TabIndex = 5;
            pauseButton.Text = "⏸";
            pauseButton.TextOffset = new Point(3, 0);
            // 
            // restartButton
            // 
            restartButton.Animated = true;
            restartButton.BackColor = Color.Transparent;
            restartButton.BorderRadius = 24;
            restartButton.CustomizableEdges = customizableEdges13;
            restartButton.DisabledState.BorderColor = Color.DarkGray;
            restartButton.DisabledState.CustomBorderColor = Color.DarkGray;
            restartButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            restartButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            restartButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            restartButton.EnableHoverRipple = true;
            restartButton.FillColor = Color.FromArgb(255, 81, 195);
            restartButton.FillColor2 = Color.FromArgb(168, 228, 255);
            restartButton.Font = new Font("Segoe UI", 22F);
            restartButton.ForeColor = Color.White;
            restartButton.Location = new Point(158, 11);
            restartButton.Name = "restartButton";
            restartButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            restartButton.ShadowDecoration.CustomizableEdges = customizableEdges14;
            restartButton.ShadowDecoration.Depth = 5;
            restartButton.ShadowDecoration.Enabled = true;
            restartButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            restartButton.Size = new Size(103, 36);
            restartButton.TabIndex = 7;
            restartButton.Text = "⟳";
            restartButton.TextOffset = new Point(0, -8);
            restartButton.Click += RestartButton_Click;
            // 
            // stopButton
            // 
            stopButton.Animated = true;
            stopButton.BackColor = Color.Transparent;
            stopButton.BorderColor = Color.FromArgb(170, 60, 130);
            stopButton.BorderRadius = 24;
            stopButton.CustomizableEdges = customizableEdges15;
            stopButton.DisabledState.BorderColor = Color.DarkGray;
            stopButton.DisabledState.CustomBorderColor = Color.DarkGray;
            stopButton.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            stopButton.DisabledState.FillColor2 = Color.FromArgb(169, 169, 169);
            stopButton.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            stopButton.EnableHoverRipple = true;
            stopButton.FillColor = Color.FromArgb(255, 81, 195);
            stopButton.FillColor2 = Color.FromArgb(168, 228, 255);
            stopButton.Font = new Font("Segoe UI", 16F, FontStyle.Regular, GraphicsUnit.Point, 0);
            stopButton.ForeColor = Color.White;
            stopButton.Location = new Point(295, 11);
            stopButton.Name = "stopButton";
            stopButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            stopButton.ShadowDecoration.CustomizableEdges = customizableEdges16;
            stopButton.ShadowDecoration.Depth = 5;
            stopButton.ShadowDecoration.Enabled = true;
            stopButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            stopButton.Size = new Size(103, 36);
            stopButton.TabIndex = 8;
            stopButton.Text = "■";
            stopButton.TextOffset = new Point(2, 0);
            // 
            // timeDisplayLabel
            // 
            timeDisplayLabel.BackColor = Color.Transparent;
            timeDisplayLabel.Font = new Font("Segoe UI", 13F);
            timeDisplayLabel.ForeColor = Color.HotPink;
            timeDisplayLabel.Location = new Point(336, 3);
            timeDisplayLabel.Name = "timeDisplayLabel";
            timeDisplayLabel.Size = new Size(46, 25);
            timeDisplayLabel.TabIndex = 8;
            timeDisplayLabel.Text = "Timer";
            timeDisplayLabel.TextAlignment = ContentAlignment.TopCenter;
            // 
            // animatedTimerSKControl
            // 
            animatedTimerSKControl.BackColor = Color.FromArgb(26, 26, 46);
            animatedTimerSKControl.ForeColor = Color.FromArgb(50, 49, 65);
            animatedTimerSKControl.Location = new Point(27, 56);
            animatedTimerSKControl.Name = "animatedTimerSKControl";
            animatedTimerSKControl.Size = new Size(448, 608);
            animatedTimerSKControl.TabIndex = 0;
            // 
            // animatedTimerFormDragControl
            // 
            animatedTimerFormDragControl.DockIndicatorTransparencyValue = 0.6D;
            animatedTimerFormDragControl.TargetControl = mainPanel;
            animatedTimerFormDragControl.UseTransparentDrag = true;
            // 
            // AnimatedTimerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(35, 34, 50);
            ClientSize = new Size(511, 749);
            Controls.Add(mainPanel);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AnimatedTimerForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Modern Timer Test";
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)codingTrackerSymbol).EndInit();
            ((System.ComponentModel.ISupportInitialize)homeButton).EndInit();
            controlPanel.ResumeLayout(false);
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
        private FontAwesome.Sharp.IconPictureBox codingTrackerSymbol;
        private Guna2HtmlLabel codingTrackerDisplayLabel;
        private Guna2TextBox nameDisplayTextBox;
        private Guna2DragControl animatedTimerFormDragControl;
        private Guna2ToggleSwitch elapsedTestToggleSwitch;
        private Guna2TextBox elapsedTestTextBox;
    }
}