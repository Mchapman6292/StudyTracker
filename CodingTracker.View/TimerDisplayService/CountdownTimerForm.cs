using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.TimerDisplayService.StopWatchTimerServices;
using Guna.UI2.WinForms;

namespace CodingTracker.View.TimerDisplayService
{
    public partial class CountdownTimerForm : Form
    {
        #region Properties

        private bool isPaused = false;
        private bool isDragging = false;
        private Point dragStartPoint;
        private int progressValue = 0;
        private int? sessionGoalSecondsInt;
        private double? progressTimerGoalSecondsDouble;

        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IApplicationLogger _appLogger;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IExitFlowManager _exitFlowManager;

        #endregion

        #region Constructor

        public CountdownTimerForm(ICodingSessionManager codingSessionManager, IFormSwitcher formSwitcher, IApplicationLogger appLogger, IStopWatchTimerService stopWatchTimerService, IExitFlowManager exitFlowManager)
        {
            InitializeComponent();
            _codingSessionManager = codingSessionManager;
            _formSwitcher = formSwitcher;
            _appLogger = appLogger;
            _stopWatchTimerService = stopWatchTimerService;
            _exitFlowManager = exitFlowManager;

            closeButton.Click += CloseButton_Click;

            SetFormPosition();
        }

        #endregion

        #region Form Events

        private void CountdownTimerForm_Load(object sender, EventArgs e)
        {
            int? sessionGoalSecondsInt = _codingSessionManager.ReturnGoalSeconds();

            progressValue = 0;
            progressBar.Value = 0;

            if (statusLabel != null)
            {
                statusLabel.Text = "0%";
            }

            if (sessionGoalSecondsInt != 0)
            {
                progressTimerGoalSecondsDouble = (double)sessionGoalSecondsInt;
            }

            _codingSessionManager.InitializeCodingSessionAndSetGoal(sessionGoalSecondsInt.Value, true);

            progressTimer.Start();
            _stopWatchTimerService.StartTimer();
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            if (!progressTimerGoalSecondsDouble.HasValue)
            {
                return;
            }

            TimeSpan elapsedTime = _stopWatchTimerService.ReturnElapsedTimeSpan();

            // Update the timeDisplayLabel
            UpdateTimeDisplayLabel(elapsedTime);

            double percentage = Math.Min(100, (elapsedTime.TotalSeconds / progressTimerGoalSecondsDouble.Value) * 100);
            progressValue = (int)percentage;

            progressBar.Value = progressValue;

            (Color mainColor, Color secondaryColor) = GetProgressColors(percentage / 100);

            progressBar.ProgressColor = mainColor;
            progressBar.ProgressColor2 = secondaryColor;

            if (statusLabel != null)
            {
                statusLabel.Text = $"{progressValue}%";
                statusLabel.ForeColor = mainColor;
            }

            if (progressValue >= 100)
            {
                _codingSessionManager.SetCurrentSessionGoalReached(true);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            _exitFlowManager.HandleExitRequest(sender, e, this);
        }

        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (isPaused && progressTimerGoalSecondsDouble > 0)
            {
                _stopWatchTimerService.StartTimer();
                progressTimer.Start();
                pauseButton.Image = Properties.Resources.pause;
                isPaused = false;
            }
            else
            {
                _stopWatchTimerService.StopTimer();
                progressTimer.Stop();
                pauseButton.Image = Properties.Resources.playButton;
                isPaused = true;
            }
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToFormWithoutPreviousFormClosing(FormPageEnum.MainPage);
            this.WindowState = FormWindowState.Minimized;
        }

        #endregion

        #region UI Helper Methods

        private void SetFormPosition()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;

            this.Location = new Point(screenWidth - formWidth - 20, screenHeight - formHeight - 20);
        }

        /// <summary>
        /// Updates the time display label with the current elapsed time and applies color transitions based on progress
        /// </summary>
        private void UpdateTimeDisplayLabel(TimeSpan elapsedTime)
        {
            string formattedTimeSpan = elapsedTime.ToString(@"hh\:mm\:ss");

            if (timeDisplayLabel.Text != formattedTimeSpan || timeDisplayLabel.Tag == null)
            {
                if (progressTimerGoalSecondsDouble.HasValue)
                {
                    double progress = Math.Min(1.0, elapsedTime.TotalSeconds / progressTimerGoalSecondsDouble.Value);
                    (Color mainColor, Color secondaryColor) = GetProgressColors(progress);

                    if (timeDisplayLabel.Tag == null || !((Color)timeDisplayLabel.Tag).ToArgb().Equals(mainColor.ToArgb()))
                    {
                        ApplyColorTransition(timeDisplayLabel, (Color)(timeDisplayLabel.Tag ?? Color.White), mainColor);
                        timeDisplayLabel.Tag = mainColor;
                    }

                    timeDisplayLabel.Text = formattedTimeSpan;

                    if (elapsedTime.Seconds % 10 == 0)
                    {
                        ApplyPulseEffect(timeDisplayLabel);
                    }
                }
                else
                {
                    timeDisplayLabel.Text = formattedTimeSpan;
                }
            }
        }

        /// <summary>
        /// Applies a smooth color transition effect between two colors on a label
        /// </summary>
        private void ApplyColorTransition(Guna2HtmlLabel label, Color fromColor, Color toColor)
        {
            System.Windows.Forms.Timer transitionTimer = new System.Windows.Forms.Timer();
            transitionTimer.Interval = 5;

            int steps = 10;
            int currentStep = 0;

            transitionTimer.Tick += (s, e) => {
                currentStep++;
                double progress = (double)currentStep / steps;

                int r = (int)(fromColor.R + (toColor.R - fromColor.R) * progress);
                int g = (int)(fromColor.G + (toColor.G - fromColor.G) * progress);
                int b = (int)(fromColor.B + (toColor.B - fromColor.B) * progress);

                label.ForeColor = Color.FromArgb(r, g, b);

                if (currentStep >= steps)
                {
                    transitionTimer.Stop();
                    transitionTimer.Dispose();
                    label.ForeColor = toColor;
                }
            };

            transitionTimer.Start();
        }

        /// <summary>
        /// Creates a brief pulse animation effect by temporarily increasing the font size
        /// </summary>
        private void ApplyPulseEffect(Guna2HtmlLabel label)
        {
            Font originalFont = label.Font;

            Font pulseFont = new Font(originalFont.FontFamily, originalFont.Size * 1.1f, originalFont.Style);

            label.Font = pulseFont;

            System.Windows.Forms.Timer pulseTimer = new System.Windows.Forms.Timer();
            pulseTimer.Interval = 200;
            pulseTimer.Tick += (s, e) => {
                label.Font = originalFont;
                pulseTimer.Stop();
                pulseTimer.Dispose();
            };

            pulseTimer.Start();
        }

        /// <summary>
        /// Calculates the gradient colors for the progress bar based on completion percentage
        /// </summary>
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

        #endregion
    }
}