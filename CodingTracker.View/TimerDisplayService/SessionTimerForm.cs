using Guna.UI2.WinForms;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;


namespace CodingTracker.View.PopUpFormService
{
    public partial class SessionTimerForm : Form
    {
        private readonly IFormStatePropertyManager _formStatePropertyManager;

        private Guna2Panel mainPanel;
        private Guna2HtmlLabel timerLabel;
        private Guna2HtmlLabel goalLabel;
        private Guna2ProgressBar progressBar;
        private Guna2BorderlessForm borderlessForm;
        private Guna2ControlBox minimizeButton;
        private Guna2ControlBox closeButton;
        private Guna2Button pauseResumeButton;

        private System.Windows.Forms.Timer sessionTimer;
        private DateTime startTime;
        private TimeSpan goalTime;
        private TimeSpan elapsedTime;
        private bool isPaused = false;
        private TimeSpan pausedElapsedTime;

        public SessionTimerForm()
        {

            InitializeComponent();
            InitializeComponentsAndTools();
            StartTimer();
        }



        private void ParseGoalTime(string goalTimeHHMM)
        {
            int hours = int.Parse(goalTimeHHMM.Substring(0, 2));
            int minutes = int.Parse(goalTimeHHMM.Substring(2, 2));
            goalTime = new TimeSpan(hours, minutes, 0);
        }



        private void InitializeComponentsAndTools()
        {
            // Configure form settings
            this.Size = new Size(300, 180);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.Manual;
            this.TopMost = true;
            this.ShowInTaskbar = false;
            this.BackColor = Color.FromArgb(35, 34, 50);

            // Position in bottom right corner of screen
            Rectangle workingArea = Screen.GetWorkingArea(this);
            this.Location = new Point(workingArea.Right - this.Width - 20,
                                    workingArea.Bottom - this.Height - 20);

            // Initialize borderless form control with rounded corners
            borderlessForm = new Guna2BorderlessForm();
            borderlessForm.ContainerControl = this;
            borderlessForm.DragForm = true;
            borderlessForm.BorderRadius = 12;

            // Add control buttons
            closeButton = new Guna2ControlBox();
            closeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            closeButton.FillColor = Color.FromArgb(35, 34, 50);
            closeButton.IconColor = Color.White;
            closeButton.Size = new Size(30, 25);
            closeButton.Location = new Point(this.Width - 30, 0);
            closeButton.Click += (s, e) => { this.Hide(); };

            minimizeButton = new Guna2ControlBox();
            minimizeButton.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            minimizeButton.FillColor = Color.FromArgb(35, 34, 50);
            minimizeButton.IconColor = Color.White;
            minimizeButton.Size = new Size(30, 25);
            minimizeButton.Location = new Point(this.Width - 60, 0);

            // Main panel
            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(35, 34, 50);
            mainPanel.BorderRadius = 12;
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderThickness = 1;
            mainPanel.Padding = new Padding(10);

            // Goal label
            goalLabel = new Guna2HtmlLabel();
            goalLabel.Text = $"Goal: {goalTime.Hours:D2}:{goalTime.Minutes:D2}:00";
            goalLabel.ForeColor = Color.White;
            goalLabel.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            goalLabel.AutoSize = false;
            goalLabel.Size = new Size(280, 20);
            goalLabel.TextAlignment = ContentAlignment.MiddleCenter;
            goalLabel.Location = new Point(10, 30);

            // Timer label
            timerLabel = new Guna2HtmlLabel();
            timerLabel.Text = "00:00:00";
            timerLabel.ForeColor = Color.White;
            timerLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            timerLabel.AutoSize = false;
            timerLabel.Size = new Size(280, 40);
            timerLabel.TextAlignment = ContentAlignment.MiddleCenter;
            timerLabel.Location = new Point(10, 55);

            // Progress bar
            progressBar = new Guna2ProgressBar();
            progressBar.BorderRadius = 5;
            progressBar.FillColor = Color.FromArgb(45, 46, 50);
            progressBar.ProgressColor = Color.FromArgb(94, 148, 255);
            progressBar.ProgressColor2 = Color.FromArgb(193, 20, 137);
            progressBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            progressBar.Size = new Size(280, 15);
            progressBar.Location = new Point(10, 100);
            progressBar.Maximum = (int)goalTime.TotalSeconds;
            progressBar.Value = 0;

            // Pause/Resume button
            pauseResumeButton = new Guna2Button();
            pauseResumeButton.Text = "Pause";
            pauseResumeButton.FillColor = Color.FromArgb(193, 20, 137);
            pauseResumeButton.BorderRadius = 15;
            pauseResumeButton.Size = new Size(100, 30);
            pauseResumeButton.Location = new Point(100, 125);
            pauseResumeButton.ForeColor = Color.White;
            pauseResumeButton.Click += PauseResumeButton_Click;

            // Session timer
            sessionTimer = new System.Windows.Forms.Timer();
            sessionTimer.Interval = 1000; // Update every second
            sessionTimer.Tick += SessionTimer_Tick;

            // Add controls to form
            mainPanel.Controls.Add(goalLabel);
            mainPanel.Controls.Add(timerLabel);
            mainPanel.Controls.Add(progressBar);
            mainPanel.Controls.Add(pauseResumeButton);
            this.Controls.Add(mainPanel);
            this.Controls.Add(closeButton);
            this.Controls.Add(minimizeButton);
        }

        private void StartTimer()
        {
            startTime = DateTime.Now;
            sessionTimer.Start();
        }

        private void SessionTimer_Tick(object sender, EventArgs e)
        {
            if (!isPaused)
            {
                elapsedTime = DateTime.Now - startTime;

                timerLabel.Text = $"{elapsedTime.Hours:D2}:{elapsedTime.Minutes:D2}:{elapsedTime.Seconds:D2}";


                int elapsedSeconds = (int)elapsedTime.TotalSeconds;
                progressBar.Value = Math.Min(elapsedSeconds, progressBar.Maximum);

                double progressPercentage = (double)elapsedSeconds / progressBar.Maximum;

                if (progressPercentage >= 1.0)
                {

                    timerLabel.ForeColor = Color.LimeGreen;
                    goalLabel.Text = "Goal Reached! 🎉";
                }
                else if (progressPercentage >= 0.9)
                {
                    timerLabel.ForeColor = Color.Yellow;
                }
            }
        }

        private void PauseResumeButton_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                startTime = DateTime.Now - pausedElapsedTime;
                pauseResumeButton.Text = "Pause";
                isPaused = false;
            }
            else
            {
                pausedElapsedTime = elapsedTime;
                pauseResumeButton.Text = "Resume";
                isPaused = true;
            }
        }

        public void ShowTimer()
        {
            this.Show();
            this.BringToFront();
        }

        public TimeSpan GetElapsedTime()
        {
            return elapsedTime;
        }

        public void StopTimer()
        {
            sessionTimer.Stop();
        }

        private void MainPageExitControlMinimizeButton_Click(object sender, EventArgs e)
        {

        }
    }
}
