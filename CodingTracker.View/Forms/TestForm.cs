using Guna.UI2.WinForms;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CodingTracker.View.Forms
{
    public partial class TestForm : Form
    {
        private System.Windows.Forms.Timer progressTimer;
        private DateTime startTime;
        private bool isPaused = false;
        private TimeSpan pausedElapsedTime;
        private double timerDurationSeconds = 60; // 1 minute for demo

        public TestForm()
        {
            InitializeComponent();

            // Wire up events
            this.Load += TestForm_Load;
            pauseButton.Click += PauseButton_Click;
            stopButton.Click += StopButton_Click;
            restartButton.Click += RestartButton_Click;

            // Set up timer
            SetupTimer();
        }

        private void SetupTimer()
        {
            progressTimer = new System.Windows.Forms.Timer();
            progressTimer.Interval = 50; // Update 20 times per second for smooth updates
            progressTimer.Tick += ProgressTimer_Tick;
        }

        private void TestForm_Load(object sender, EventArgs e)
        {
            // Initialize and start timer when form loads
            startTime = DateTime.Now;
            progressTimer.Start();
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            // Calculate elapsed time
            TimeSpan elapsedTime = GetElapsedTime();

            // Format time display
            string formattedTime = elapsedTime.ToString(@"mm\:ss");
            timeLabel.Text = formattedTime;

            // Calculate progress percentage (0-100)
            double percentage = Math.Min(100, (elapsedTime.TotalSeconds / timerDurationSeconds) * 100);
            int progressValue = (int)percentage;

            // Update progress bar
            circleProgressBar.Value = progressValue;

            // Add color changes based on progress
            UpdateProgressColors(percentage / 100);

            // Check if timer has completed
            if (progressValue >= 100)
            {
                progressTimer.Stop();
                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        private TimeSpan GetElapsedTime()
        {
            if (isPaused)
            {
                return pausedElapsedTime;
            }
            return DateTime.Now - startTime;
        }

        private void UpdateProgressColors(double normalizedProgress)
        {
            // Simple color transition from pink to blue
            Color startColor = Color.FromArgb(255, 81, 195); // Pink
            Color endColor = Color.FromArgb(168, 228, 255);  // Light blue

            int r = (int)(startColor.R + (endColor.R - startColor.R) * normalizedProgress);
            int g = (int)(startColor.G + (endColor.G - startColor.G) * normalizedProgress);
            int b = (int)(startColor.B + (endColor.B - startColor.B) * normalizedProgress);

            Color blendedColor = Color.FromArgb(r, g, b);

            // Apply colors to progress bar
            circleProgressBar.ProgressColor = blendedColor;
            circleProgressBar.ProgressColor2 = Color.FromArgb(
                Math.Min(255, (int)(r * 1.1)),
                Math.Min(255, (int)(g * 1.1)),
                Math.Min(255, (int)(b * 1.1))
            );

            // Match time label color to progress color
            timeLabel.ForeColor = blendedColor;
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                // Pause the timer
                pausedElapsedTime = DateTime.Now - startTime;
                progressTimer.Stop();
                isPaused = true;
                pauseButton.Text = "▶";
            }
            else
            {
                // Resume the timer
                startTime = DateTime.Now - pausedElapsedTime;
                progressTimer.Start();
                isPaused = false;
                pauseButton.Text = "⏸";
            }
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            // Stop the timer and reset
            progressTimer.Stop();
            circleProgressBar.Value = 0;
            timeLabel.Text = "00:00";
            isPaused = false;
        }

        private void RestartButton_Click(object sender, EventArgs e)
        {
            // Reset and start
            progressTimer.Stop();
            startTime = DateTime.Now;
            circleProgressBar.Value = 0;
            timeLabel.Text = "00:00";
            isPaused = false;
            progressTimer.Start();
            pauseButton.Text = "⏸";
        }
    }
}