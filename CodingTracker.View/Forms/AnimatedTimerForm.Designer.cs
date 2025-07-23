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
            CustomizableEdges customizableEdges15 = new CustomizableEdges();
            CustomizableEdges customizableEdges16 = new CustomizableEdges();
            CustomizableEdges customizableEdges1 = new CustomizableEdges();
            CustomizableEdges customizableEdges2 = new CustomizableEdges();
            CustomizableEdges customizableEdges3 = new CustomizableEdges();
            CustomizableEdges customizableEdges4 = new CustomizableEdges();
            CustomizableEdges customizableEdges5 = new CustomizableEdges();
            CustomizableEdges customizableEdges6 = new CustomizableEdges();
            CustomizableEdges customizableEdges13 = new CustomizableEdges();
            CustomizableEdges customizableEdges14 = new CustomizableEdges();
            CustomizableEdges customizableEdges7 = new CustomizableEdges();
            CustomizableEdges customizableEdges8 = new CustomizableEdges();
            CustomizableEdges customizableEdges9 = new CustomizableEdges();
            CustomizableEdges customizableEdges10 = new CustomizableEdges();
            CustomizableEdges customizableEdges11 = new CustomizableEdges();
            CustomizableEdges customizableEdges12 = new CustomizableEdges();
            borderlessForm = new Guna2BorderlessForm(components);
            mainPanel = new Guna2GradientPanel();
            testElapsedBox = new Guna2TextBox();
            minimizeButton = new Guna2ControlBox();
            closeButton = new Guna2ControlBox();
            controlPanel = new Guna2Panel();
            pauseButton = new CustomGradientButton();
            restartButton = new CustomGradientButton();
            stopButton = new CustomGradientButton();
            timeDisplayLabel = new Guna2HtmlLabel();
            animatedTimerSKControl = new SKControl();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
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
            mainPanel.Anchor = AnchorStyles.None;
            mainPanel.BackColor = Color.Transparent;
            mainPanel.BorderColor = Color.FromArgb(225, 225, 225);
            mainPanel.BorderRadius = 12;
            mainPanel.Controls.Add(testElapsedBox);
            mainPanel.Controls.Add(minimizeButton);
            mainPanel.Controls.Add(closeButton);
            mainPanel.Controls.Add(controlPanel);
            mainPanel.Controls.Add(timeDisplayLabel);
            mainPanel.Controls.Add(animatedTimerSKControl);
            mainPanel.CustomizableEdges = customizableEdges15;
            mainPanel.FillColor = Color.FromArgb(26, 26, 46);
            mainPanel.FillColor2 = Color.FromArgb(26, 26, 46);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.ShadowDecoration.Color = Color.FromArgb(80, 0, 0, 0);
            mainPanel.ShadowDecoration.CustomizableEdges = customizableEdges16;
            mainPanel.ShadowDecoration.Depth = 20;
            mainPanel.ShadowDecoration.Enabled = true;
            mainPanel.ShadowDecoration.Shadow = new Padding(3, 3, 7, 7);
            mainPanel.Size = new Size(699, 749);
            mainPanel.TabIndex = 0;
            // 
            // testElapsedBox
            // 
            testElapsedBox.BorderColor = Color.Red;
            testElapsedBox.CustomizableEdges = customizableEdges1;
            testElapsedBox.DefaultText = "";
            testElapsedBox.DisabledState.BorderColor = Color.FromArgb(208, 208, 208);
            testElapsedBox.DisabledState.FillColor = Color.FromArgb(226, 226, 226);
            testElapsedBox.DisabledState.ForeColor = Color.FromArgb(138, 138, 138);
            testElapsedBox.DisabledState.PlaceholderForeColor = Color.FromArgb(138, 138, 138);
            testElapsedBox.FillColor = Color.FromArgb(26, 26, 46);
            testElapsedBox.FocusedState.BorderColor = Color.FromArgb(94, 148, 255);
            testElapsedBox.Font = new Font("Segoe UI", 9F);
            testElapsedBox.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            testElapsedBox.Location = new Point(506, 673);
            testElapsedBox.Name = "testElapsedBox";
            testElapsedBox.PlaceholderText = "";
            testElapsedBox.SelectedText = "";
            testElapsedBox.ShadowDecoration.CustomizableEdges = customizableEdges2;
            testElapsedBox.Size = new Size(149, 36);
            testElapsedBox.TabIndex = 29;
            testElapsedBox.Visible = false;
            // 
            // minimizeButton
            // 
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeButton.Cursor = Cursors.Hand;
            minimizeButton.CustomizableEdges = customizableEdges3;
            minimizeButton.FillColor = Color.FromArgb(25, 24, 40);
            minimizeButton.HoverState.FillColor = Color.FromArgb(0, 9, 43);
            minimizeButton.HoverState.IconColor = Color.White;
            minimizeButton.IconColor = Color.HotPink;
            minimizeButton.Location = new Point(610, 0);
            minimizeButton.Name = "minimizeButton";
            minimizeButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
            minimizeButton.Size = new Size(45, 34);
            minimizeButton.TabIndex = 28;
            // 
            // closeButton
            // 
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.Cursor = Cursors.Hand;
            closeButton.CustomClick = true;
            closeButton.CustomizableEdges = customizableEdges5;
            closeButton.FillColor = Color.FromArgb(25, 24, 40);
            closeButton.HoverState.IconColor = Color.White;
            closeButton.IconColor = Color.HotPink;
            closeButton.Location = new Point(652, 0);
            closeButton.Name = "closeButton";
            closeButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
            closeButton.Size = new Size(45, 34);
            closeButton.TabIndex = 27;
            // 
            // controlPanel
            // 
            controlPanel.BackColor = Color.Transparent;
            controlPanel.Controls.Add(pauseButton);
            controlPanel.Controls.Add(restartButton);
            controlPanel.Controls.Add(stopButton);
            controlPanel.CustomizableEdges = customizableEdges13;
            controlPanel.FillColor = Color.Transparent;
            controlPanel.Location = new Point(186, 657);
            controlPanel.Name = "controlPanel";
            controlPanel.ShadowDecoration.CustomizableEdges = customizableEdges14;
            controlPanel.Size = new Size(280, 80);
            controlPanel.TabIndex = 9;
            // 
            // pauseButton
            // 
            pauseButton.Animated = true;
            pauseButton.BackColor = Color.Transparent;
            pauseButton.BorderColor = Color.FromArgb(170, 60, 130);
            pauseButton.BorderRadius = 24;
            pauseButton.CustomizableEdges = customizableEdges7;
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
            pauseButton.Location = new Point(40, 16);
            pauseButton.Name = "pauseButton";
            pauseButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            pauseButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
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
            restartButton.CustomizableEdges = customizableEdges9;
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
            restartButton.Location = new Point(192, 16);
            restartButton.Name = "restartButton";
            restartButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            restartButton.ShadowDecoration.CustomizableEdges = customizableEdges10;
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
            stopButton.CustomizableEdges = customizableEdges11;
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
            stopButton.Location = new Point(116, 16);
            stopButton.Name = "stopButton";
            stopButton.ShadowDecoration.Color = Color.FromArgb(0, 0, 0, 40);
            stopButton.ShadowDecoration.CustomizableEdges = customizableEdges12;
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
            timeDisplayLabel.Location = new Point(238, 9);
            timeDisplayLabel.Name = "timeDisplayLabel";
            timeDisplayLabel.Size = new Size(137, 25);
            timeDisplayLabel.TabIndex = 8;
            timeDisplayLabel.Text = "guna2HtmlLabel1";
            // 
            // animatedTimerSKControl
            // 
            animatedTimerSKControl.BackColor = Color.FromArgb(26, 26, 46);
            animatedTimerSKControl.ForeColor = Color.FromArgb(50, 49, 65);
            animatedTimerSKControl.Location = new Point(107, 40);
            animatedTimerSKControl.Name = "animatedTimerSKControl";
            animatedTimerSKControl.Size = new Size(448, 590);
            animatedTimerSKControl.TabIndex = 0;
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
        private Guna2ControlBox closeButton;
        private Guna2TextBox testElapsedBox;
    }
}