using Guna.UI2.WinForms;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.View.FormService;
using CodingTracker.View.FormPageEnums;
using CodingTracker.Common.IApplicationLoggers;


namespace CodingTracker.View.TimerDisplayService
{
    public partial class CountdownTimerForm : Form
    {
        private Guna2CircleProgressBar progressBar;
        private System.Windows.Forms.Timer progressTimer;
        private Guna2BorderlessForm borderlessForm;
        private Guna2Panel mainPanel;
        private Guna2ControlBox closeControlBox;
        private Guna2ControlBox minimizeControlBox;
        private Guna2Button pauseButton;
        private bool isPaused = false;
        private bool isDragging = false;
        private Point dragStartPoint;

        private int progressValue = 0;
        private int formSessionGoal;
        private DateTime formStartTime;
        private DateTime endTime;


        private readonly IFormStatePropertyManager _formStatePropertyManager;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IApplicationLogger _appLogger;
           

        public CountdownTimerForm(IFormStatePropertyManager formStatePropertyManager, ICodingSessionManager codingSessionManager, IFormSwitcher formSwitcher, IApplicationLogger appLogger)
        {
            InitializeComponent();
            _formStatePropertyManager = formStatePropertyManager;
            _codingSessionManager = codingSessionManager;
            _formSwitcher = formSwitcher;
            _appLogger = appLogger;
            formSessionGoal = _formStatePropertyManager.ReturnFormGoalTimeHHMMAsInt();
            SetupProgressBar();
            _codingSessionManager.StartCodingSession(formStartTime, formSessionGoal, true);
        }

        void SetupProgressBar()
        {
            this.Size = new Size(220, 220);
            this.Text = "";
            this.StartPosition = FormStartPosition.Manual;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(35, 34, 50);
            this.ShowInTaskbar = false;

            borderlessForm = new Guna2BorderlessForm();
            borderlessForm.ContainerControl = this;
            borderlessForm.DragForm = true;
            borderlessForm.BorderRadius = 12;

            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(32, 33, 36);
            mainPanel.BorderRadius = 12;
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderThickness = 1;
            mainPanel.Padding = new Padding(10);

            progressBar = new Guna2CircleProgressBar();
            progressBar.Size = new Size(170, 170);
            progressBar.Location = new Point(15, 15);
            progressBar.Minimum = 0;
            progressBar.Maximum = 100;
            progressBar.Value = 0;
            progressBar.AnimationSpeed = 0.5f;
            progressBar.ProgressThickness = 15;
            progressBar.FillThickness = 15;
            progressBar.FillColor = Color.FromArgb(45, 46, 50);
            progressBar.BackColor = Color.Transparent;
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
            statusLabel.Location = new Point(15, 75);
            statusLabel.Name = "statusLabel";
            statusLabel.BackColor = Color.Transparent;

            SetFormPosition();

            progressBar.Controls.Add(statusLabel);
            mainPanel.Controls.Add(progressBar);
            this.Controls.Add(mainPanel);

            progressTimer = new System.Windows.Forms.Timer();
            progressTimer.Interval = 50;
            progressTimer.Tick += ProgressTimer_Tick;

            closeControlBox = new Guna2ControlBox();
            closeControlBox.Size = new Size(16, 16);
            closeControlBox.Location = new Point(mainPanel.Width - 20, 4);
            closeControlBox.FillColor = Color.FromArgb(40, 40, 60);
            closeControlBox.IconColor = Color.White;
            closeControlBox.HoverState.FillColor = Color.FromArgb(60, 60, 80);
            closeControlBox.BorderRadius = 0;
            closeControlBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.CloseBox;
            closeControlBox.Click += CloseControlBox_Click;

            minimizeControlBox = new Guna2ControlBox();
            minimizeControlBox.Size = new Size(16, 16);
            minimizeControlBox.Location = new Point(mainPanel.Width - 36, 4);
            minimizeControlBox.FillColor = Color.FromArgb(40, 40, 60);
            minimizeControlBox.IconColor = Color.White;
            minimizeControlBox.HoverState.FillColor = Color.FromArgb(60, 60, 80);
            minimizeControlBox.BorderRadius = 0;
            minimizeControlBox.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            minimizeControlBox.Click += (s, e) => { this.WindowState = FormWindowState.Minimized; };


            pauseButton = new Guna2Button();
            pauseButton.Size = new Size(25, 25);
            pauseButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            pauseButton.FillColor = Color.FromArgb(70, 71, 117);
            pauseButton.BorderRadius = 12;
            pauseButton.Text = "";
            pauseButton.Image = Properties.Resources.pause;
            pauseButton.Location = new Point(5, mainPanel.Height - 30);
            pauseButton.Cursor = Cursors.Hand;
            pauseButton.ForeColor = Color.White;
            pauseButton.Click += PauseButton_Click;

            mainPanel.Controls.Add(closeControlBox);
            mainPanel.Controls.Add(minimizeControlBox);
            mainPanel.Controls.Add(pauseButton);

            this.Load += CountdownTimerForm_Load;
        }

        private void SetFormStartTime(DateTime startTime)
        {
            formStartTime = startTime;
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

            _codingSessionManager.SetCodingSessionStartTimeAndDate(DateTime.Now);
            SetFormStartTime(DateTime.Now);
            progressTimer.Start();
        }

        private async void ProgressTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = DateTime.Now - formStartTime;
            double percentage = Math.Min(100, (elapsed.TotalSeconds / formSessionGoal) * 100);
            progressValue = (int)percentage;

            progressBar.Value = progressValue;

            (Color mainColor, Color secondaryColor) = GetProgressColors(percentage / 100);

            progressBar.ProgressColor = mainColor;
            progressBar.ProgressColor2 = secondaryColor;

            var statusLabel = progressBar.Controls["statusLabel"] as Guna2HtmlLabel;

            if (statusLabel != null)
            {
                statusLabel.Text = $"{progressValue}%";
                statusLabel.ForeColor = mainColor;
            }

            if (progressValue >= 100)
            {
                progressTimer.Stop();
                try
                {
                    await _codingSessionManager.EndCodingSessionAsync(true);
                }
                catch (Exception ex)
                {
                    _appLogger.Error($"Error during {nameof(ProgressTimer_Tick)}: {ex}.");
                }
            }
        }

        private void UpdateCodingSessionOnTimerComplete()
        {

        }

        private (Color MainColor, Color SecondaryColor) GetProgressColors(double progress)
        {
            int r = (int)(255 * (1 - progress) + 64 * progress);
            int g = (int)(81 * (1 - progress) + 230 * progress);
            int b = (int)(195 * (1 - progress) + 200 * progress);
            Color mainColor = Color.FromArgb(r, g, b);
            Color secondaryColor;

            if (progress < 0.5)
            {
                int r2 = (int)(200 * (1 - progress * 2) + 0 * progress * 2);
                int g2 = (int)(60 * (1 - progress * 2) + 180 * progress * 2);
                int b2 = (int)(170 * (1 - progress * 2) + 190 * progress * 2);
                secondaryColor = Color.FromArgb(r2, g2, b2);
            }

            else
            {
                int r2 = (int)(0 * (1 - (progress - 0.5) * 2) + 72 * (progress - 0.5) * 2);
                int g2 = (int)(180 * (1 - (progress - 0.5) * 2) + 255 * (progress - 0.5) * 2);
                int b2 = (int)(190 * (1 - (progress - 0.5) * 2) + 180 * (progress - 0.5) * 2);
                secondaryColor = Color.FromArgb(r2, g2, b2);
            }

            return (mainColor, secondaryColor);
        }



        private void SetFormPosition()
        {
            this.StartPosition = FormStartPosition.Manual;

            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;


            this.Location = new Point(screenWidth - formWidth - 20, screenHeight - formHeight - 20);

        }



        private async void CloseControlBox_Click(object sender, EventArgs e)
        {
            progressTimer.Stop();

            TimeSpan elapsed = DateTime.Now - formStartTime;

            DialogResult result = MessageBox.Show(
                $"End session and record time?\nElapsed: {elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}",
                "End Session",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.OK;
                bool? goalReached = _codingSessionManager.ReturnCurrentSessionGoalReached();
                await _codingSessionManager.EndCodingSessionAsync(goalReached);
                _formSwitcher.SwitchToForm(FormPageEnum.MainPage);
            }
            else
            {
                if (!isPaused)
                {
                    progressTimer.Start();
                }
            }
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (isPaused)
            {
                formStartTime = DateTime.Now.Subtract(TimeSpan.FromSeconds(formSessionGoal * progressValue / 100.0));
                progressTimer.Start();
                pauseButton.Image = Properties.Resources.pause;
                isPaused = false;
            }
            else
            {
                progressTimer.Stop();
                pauseButton.Image = Properties.Resources.playButton;
                isPaused = true;

            }
        }
    }
}
