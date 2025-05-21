using Guna.UI2.WinForms;

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
        private Guna.UI2.WinForms.Guna2CircleProgressBar circleProgressBar;
        private Guna.UI2.WinForms.Guna2HtmlLabel timeLabel;
        private Guna.UI2.WinForms.Guna2HtmlLabel sessionNameLabel;
        private Guna.UI2.WinForms.Guna2Panel controlPanel;
        private Guna.UI2.WinForms.Guna2GradientButton pauseButton;
        private Guna.UI2.WinForms.Guna2GradientButton stopButton;
        private Guna.UI2.WinForms.Guna2GradientButton restartButton;

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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            borderlessForm = new Guna.UI2.WinForms.Guna2BorderlessForm(components);
            mainPanel = new Guna.UI2.WinForms.Guna2Panel();
            circleProgressBar = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            trackOutline = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            timeLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            sessionNameLabel = new Guna.UI2.WinForms.Guna2HtmlLabel();
            controlPanel = new Guna.UI2.WinForms.Guna2Panel();
            pauseButton = new Guna.UI2.WinForms.Guna2GradientButton();
            stopButton = new Guna.UI2.WinForms.Guna2GradientButton();
            restartButton = new Guna.UI2.WinForms.Guna2GradientButton();
            mainPanel.SuspendLayout();
            circleProgressBar.SuspendLayout();
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
            mainPanel.Controls.Add(circleProgressBar);
            mainPanel.Controls.Add(controlPanel);
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
            // circleProgressBar
            circleProgressBar.AnimationSpeed = 0.6F; // ENHANCEMENT 1: Smoother animation
            circleProgressBar.BackColor = Color.Transparent;
            circleProgressBar.Controls.Add(trackOutline);
            circleProgressBar.Controls.Add(timeLabel);
            circleProgressBar.Controls.Add(sessionNameLabel);
            circleProgressBar.FillColor = Color.FromArgb(48, 49, 54);
            circleProgressBar.FillThickness = 10; // ENHANCEMENT 2: Thicker background
            circleProgressBar.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            circleProgressBar.ForeColor = Color.White;
            circleProgressBar.Location = new Point(50, 40);
            circleProgressBar.Minimum = 0;
            circleProgressBar.Name = "circleProgressBar";
            circleProgressBar.ProgressColor = Color.FromArgb(255, 81, 195);
            circleProgressBar.ProgressColor2 = Color.FromArgb(168, 228, 255);
            circleProgressBar.ProgressEndCap = System.Drawing.Drawing2D.LineCap.Round; // ENHANCEMENT 2: Rounded ends
            circleProgressBar.ProgressStartCap = System.Drawing.Drawing2D.LineCap.Round; // ENHANCEMENT 2: Rounded starts
            circleProgressBar.ProgressThickness = 10; // ENHANCEMENT 2: Matching thickness
            circleProgressBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal; // ENHANCEMENT 3: Diagonal gradient
            circleProgressBar.ShadowDecoration.Enabled = true; // ENHANCEMENT 3: Enable shadow
            circleProgressBar.ShadowDecoration.Color = Color.FromArgb(60, 0, 0, 0); // ENHANCEMENT 3: Shadow color
            circleProgressBar.ShadowDecoration.Depth = 3; // ENHANCEMENT 3: Shadow depth
            circleProgressBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle; // ENHANCEMENT 3: Shadow mode
            circleProgressBar.ShadowDecoration.CustomizableEdges = customizableEdges2;
            circleProgressBar.Size = new Size(220, 220);
            circleProgressBar.TabIndex = 1;
            circleProgressBar.UseTransparentBackground = true; // ENHANCEMENT 1: Enable transparency
            circleProgressBar.Value = 25;

            // TRACK OUTLINE SETUP
            // ------------------
            trackOutline.BackColor = Color.Transparent;
            trackOutline.Dock = DockStyle.Fill;
            trackOutline.FillColor = Color.FromArgb(10, 255, 255, 255); // ENHANCEMENT 4: Subtle inner glow
            trackOutline.FillThickness = 1;
            trackOutline.Font = new Font("Segoe UI", 12F);
            trackOutline.ForeColor = Color.White;
            trackOutline.Location = new Point(0, 0);
            trackOutline.Minimum = 0;
            trackOutline.Name = "trackOutline";
            trackOutline.ProgressColor = Color.FromArgb(30, 255, 255, 255); // ENHANCEMENT 4: Subtle track indication
            trackOutline.ProgressThickness = 2; // ENHANCEMENT 4: Thinner outline track
            trackOutline.ShadowDecoration.CustomizableEdges = customizableEdges1;
            trackOutline.Size = new Size(220, 220);
            trackOutline.TabIndex = 0;
            trackOutline.Value = 100;

            // ENHANCEMENT 5: Add Glow Effect
            // -----------------------------
            Guna.UI2.WinForms.Guna2CircleProgressBar glowEffect = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            glowEffect.FillColor = Color.Transparent;
            glowEffect.ProgressColor = Color.FromArgb(40, 255, 255, 255);
            glowEffect.ProgressThickness = 15;
            glowEffect.ProgressBrushMode = Guna.UI2.WinForms.Enums.BrushMode.SolidTransition;
            glowEffect.Value = 100;
            glowEffect.Dock = DockStyle.Fill;
            glowEffect.Name = "glowEffect";
            trackOutline.Controls.Add(glowEffect);

            // ENHANCEMENT 6: Create Outer Ring
            // ------------------------------
            Guna.UI2.WinForms.Guna2CircleProgressBar outerRing = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            outerRing.FillColor = Color.FromArgb(25, 24, 40);
            outerRing.ProgressColor = Color.FromArgb(20, 255, 255, 255);
            outerRing.ProgressThickness = 1;
            outerRing.Value = 100;
            outerRing.Name = "outerRing";
            outerRing.Size = new Size(240, 240);
            outerRing.Location = new Point(40, 30);
            mainPanel.Controls.Add(outerRing);
            mainPanel.Controls.SetChildIndex(outerRing, mainPanel.Controls.GetChildIndex(circleProgressBar) + 1);

            // ENHANCEMENT 7: Text Display Enhancements
            timeLabel.Font = new Font("Segoe UI", 28F, FontStyle.Bold);
            timeLabel.AutoSize = false;
            timeLabel.Size = new Size(150, 55);
            timeLabel.Location = new Point(35, 80);
            timeLabel.TextAlignment = ContentAlignment.MiddleCenter;

            // Create a slight shadow effect by layering (if you really want a shadow effect)
            Guna2HtmlLabel shadowLabel = new Guna2HtmlLabel();
            shadowLabel.Text = timeLabel.Text;
            shadowLabel.Font = timeLabel.Font;
            shadowLabel.Size = timeLabel.Size;
            shadowLabel.Location = new Point(timeLabel.Location.X + 1, timeLabel.Location.Y + 1);
            shadowLabel.ForeColor = Color.FromArgb(80, 0, 0, 0);
            shadowLabel.TextAlignment = ContentAlignment.MiddleCenter;
            shadowLabel.AutoSize = false;
            shadowLabel.Name = "shadowLabel";
            circleProgressBar.Controls.Add(shadowLabel);
            circleProgressBar.Controls.SetChildIndex(shadowLabel, circleProgressBar.Controls.GetChildIndex(timeLabel) + 1);

            // ENHANCEMENT 10: Consistent Color Scheme
            // -------------------------------------
            Color primaryAccent = Color.FromArgb(255, 81, 195);
            Color secondaryAccent = Color.FromArgb(168, 228, 255);

            pauseButton.FillColor = primaryAccent;
            pauseButton.FillColor2 = secondaryAccent;
            stopButton.FillColor = primaryAccent;
            stopButton.FillColor2 = secondaryAccent;
            restartButton.FillColor = primaryAccent;
            restartButton.FillColor2 = secondaryAccent;

            // 
            // timeLabel
            // 
            timeLabel.BackColor = Color.Transparent;
            timeLabel.Font = new Font("Segoe UI", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            timeLabel.ForeColor = Color.FromArgb(40, 40, 40);
            timeLabel.Location = new Point(30, 85);
            timeLabel.Name = "timeLabel";
            timeLabel.Size = new Size(84, 47);
            timeLabel.TabIndex = 2;
            timeLabel.Text = "00:15";
            timeLabel.TextAlignment = ContentAlignment.MiddleCenter;
            // 
            // sessionNameLabel
            // 
            sessionNameLabel.BackColor = Color.Transparent;
            sessionNameLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular, GraphicsUnit.Point, 0);
            sessionNameLabel.ForeColor = Color.FromArgb(120, 120, 120);
            sessionNameLabel.Location = new Point(10, 160);
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
            controlPanel.CustomizableEdges = customizableEdges9;
            controlPanel.FillColor = Color.Transparent;
            controlPanel.Location = new Point(20, 290);
            controlPanel.Name = "controlPanel";
            controlPanel.ShadowDecoration.CustomizableEdges = customizableEdges10;
            controlPanel.Size = new Size(280, 80);
            controlPanel.TabIndex = 4;
            // 
            // pauseButton
            // 
            pauseButton.BackColor = Color.Transparent;
            pauseButton.BorderColor = Color.FromArgb(210, 210, 210);
            pauseButton.BorderRadius = 24;
            pauseButton.BorderThickness = 1;
            pauseButton.CustomizableEdges = customizableEdges3;
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
            pauseButton.ShadowDecoration.CustomizableEdges = customizableEdges4;
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
            stopButton.CustomizableEdges = customizableEdges5;
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
            stopButton.ShadowDecoration.CustomizableEdges = customizableEdges6;
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
            restartButton.CustomizableEdges = customizableEdges7;
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
            restartButton.ShadowDecoration.CustomizableEdges = customizableEdges8;
            restartButton.ShadowDecoration.Depth = 5;
            restartButton.ShadowDecoration.Enabled = true;
            restartButton.ShadowDecoration.Shadow = new Padding(0, 0, 3, 3);
            restartButton.Size = new Size(48, 48);
            restartButton.TabIndex = 7;
            restartButton.Text = "⟳";
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
            circleProgressBar.ResumeLayout(false);
            circleProgressBar.PerformLayout();
            controlPanel.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2CircleProgressBar trackOutline;
    }
}