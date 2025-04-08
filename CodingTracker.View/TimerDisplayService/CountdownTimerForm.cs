using Guna.UI2.WinForms;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;

namespace CodingTracker.View.TimerDisplayService
{
    public partial class CountdownTimerForm : Form
    {
        private Guna2CircleProgressBar progressBar;
        private System.Windows.Forms.Timer progressTimer;
        private int progressValue = 0;
        private int sessionGoal;
        private DateTime startTime;
        private readonly IFormStatePropertyManager _formStatePropertyManager;

        public CountdownTimerForm(IFormStatePropertyManager formStatePropertyManager)
        {
            InitializeComponent();
            _formStatePropertyManager = formStatePropertyManager;
            sessionGoal = _formStatePropertyManager.ReturnFormGoalTimeHHMMAsInt();
            SetupProgressBar();
        }

        private void SetupProgressBar()
        {
            this.Size = new Size(400, 400);
            this.Text = "Progress Bar Test";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(40, 45, 60);

            progressBar = new Guna2CircleProgressBar();
            progressBar.Size = new Size(200, 200);
            progressBar.Location = new Point(100, 80);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            progressBar.AnimationSpeed = 0.5f;
            progressBar.ProgressThickness = 15;
            progressBar.FillThickness = 15;
            progressBar.FillColor = Color.FromArgb(60, 65, 80);
            progressBar.ProgressColor = Color.FromArgb(255, 81, 195);
            progressBar.ProgressColor2 = Color.FromArgb(168, 228, 255);
            progressBar.ProgressBrushMode = Guna.UI2.WinForms.Enums.BrushMode.Gradient;

            Guna2HtmlLabel statusLabel = new Guna2HtmlLabel();
            statusLabel.Text = "0%";
            statusLabel.Font = new Font("Segoe UI", 16, FontStyle.Bold);
            statusLabel.ForeColor = Color.White;
            statusLabel.AutoSize = false;
            statusLabel.Size = new Size(150, 30);
            statusLabel.TextAlignment = ContentAlignment.MiddleCenter;
            statusLabel.Location = new Point(25, 85);
            statusLabel.Name = "statusLabel";

            progressBar.Controls.Add(statusLabel);
            this.Controls.Add(progressBar);

            progressTimer = new System.Windows.Forms.Timer();
            progressTimer.Interval = 50;
            progressTimer.Tick += ProgressTimer_Tick;

            this.Load += CountdownTimerForm_Load;
        }

        private void CountdownTimerForm_Load(object sender, EventArgs e)
        {
            progressValue = 0;
            progressBar.Value = 0;
            var statusLabel = progressBar.Controls["statusLabel"] as Guna2HtmlLabel;
            if (statusLabel != null)
            {
                statusLabel.Text = "0%";
            }
            startTime = DateTime.Now;
            progressTimer.Start();
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - startTime;
            double percentage = Math.Min(100, (elapsed.TotalSeconds / sessionGoal) * 100);
            progressValue = (int)percentage;

            progressBar.Value = progressValue;

            var statusLabel = progressBar.Controls["statusLabel"] as Guna2HtmlLabel;
            if (statusLabel != null)
            {
                statusLabel.Text = $"{progressValue}%";
            }

            UpdateProgressColor(percentage / 100);

            if (progressValue >= 100)
            {
                progressTimer.Stop();
            }
        }

        private void UpdateProgressColor(double progress)
        {
            int r = (int)(255 * (1 - progress) + 168 * progress);
            int g = (int)(81 * (1 - progress) + 228 * progress);
            int b = (int)(195 * (1 - progress) + 255 * progress);

            progressBar.ProgressColor = Color.FromArgb(r, g, b);

            if (progress < 0.5)
            {
                int r2 = (int)(168 * (progress * 2) + 255 * (1 - progress * 2));
                int g2 = (int)(228 * (progress * 2) + 81 * (1 - progress * 2));
                int b2 = (int)(255 * (progress * 2) + 195 * (1 - progress * 2));
                progressBar.ProgressColor2 = Color.FromArgb(r2, g2, b2);
            }
            else
            {
                int r2 = (int)(100 * ((progress - 0.5) * 2) + 168 * (1 - (progress - 0.5) * 2));
                int g2 = (int)(200 * ((progress - 0.5) * 2) + 228 * (1 - (progress - 0.5) * 2));
                int b2 = (int)(255 * ((progress - 0.5) * 2) + 255 * (1 - (progress - 0.5) * 2));
                progressBar.ProgressColor2 = Color.FromArgb(r2, g2, b2);
            }
        }
    }
}