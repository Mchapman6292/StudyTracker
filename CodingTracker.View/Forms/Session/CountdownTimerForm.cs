using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormManagement;
using Guna.UI2.WinForms;

namespace CodingTracker.View.TimerDisplayService
{
    public partial class CountdownTimerForm : Form
    {
        #region Properties

        private bool isPaused = false;
        private int progressValue = 0;
        private double? progressTimerGoalSecondsDouble;

        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormNavigator _formNavigator;
        private readonly IApplicationLogger _appLogger;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IExitFlowManager _exitFlowManager;

        #endregion

        #region Constructor

        public CountdownTimerForm(ICodingSessionManager codingSessionManager, IFormNavigator formSwitcher, IApplicationLogger appLogger, IStopWatchTimerService stopWatchTimerService, IExitFlowManager exitFlowManager)
        {
            InitializeComponent();
            _codingSessionManager = codingSessionManager;
            _formNavigator = formSwitcher;
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

            if (percentProgressLabel != null)
            {
                percentProgressLabel.Text = "0%";
            }

            if (sessionGoalSecondsInt != 0)
            {
                progressTimerGoalSecondsDouble = (double)sessionGoalSecondsInt;
            }

            _codingSessionManager.InitializeCodingSessionAndSetGoal(sessionGoalSecondsInt.Value, true);

            progressTimer.Start();
            _stopWatchTimerService.StartTimer();
        }

        private void ApplyColorTransitionToTimeDisplayLabel(Color targetColor)
        {
            if (timeDisplayLabel == null)
                return;

            if (timeDisplayLabel.Tag == null || !((Color)timeDisplayLabel.Tag).ToArgb().Equals(targetColor.ToArgb()))
            {
                Color currentColor = (Color)(timeDisplayLabel.Tag ?? Color.White);
                ApplyColorTransitionToGuna2HtmlLabel(timeDisplayLabel, currentColor, targetColor);
                timeDisplayLabel.Tag = targetColor;
            }
        }

        private void ApplyColorTransitionToPercentProgressLabel(Color targetColor)
        {
            if (percentProgressLabel == null)
                return;

            if (percentProgressLabel.Tag == null || !((Color)percentProgressLabel.Tag).ToArgb().Equals(targetColor.ToArgb()))
            {
                Color currentColor = (Color)(percentProgressLabel.Tag ?? Color.White);
                ApplyColorTransitionToGuna2HtmlLabel(percentProgressLabel, currentColor, targetColor);
                percentProgressLabel.Tag = targetColor;
            }
        }

        private void UpdateTimeDisplayLabelText(string timeText)
        {
            if (timeDisplayLabel == null)
                return;

            if (timeDisplayLabel.Text != timeText)
            {
                timeDisplayLabel.Text = timeText;
            }
        }

        private void UpdatePercentProgressLabelText(string percentText)
        {
            if (percentProgressLabel == null)
                return;

            if (percentProgressLabel.Text != percentText)
            {
                percentProgressLabel.Text = percentText;
            }
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            if (!progressTimerGoalSecondsDouble.HasValue)
            {
                return;
            }

            TimeSpan elapsedTime = _stopWatchTimerService.ReturnElapsedTimeSpan();
            string formattedTimeSpan = elapsedTime.ToString(@"hh\:mm\:ss");

            UpdateTimeDisplayLabelText(formattedTimeSpan);

            double percentage = Math.Min(100, (elapsedTime.TotalSeconds / progressTimerGoalSecondsDouble.Value) * 100);
            progressValue = (int)percentage;
            progressBar.Value = progressValue;

            (Color mainColor, Color secondaryColor) = GetProgressColors(percentage / 100);

            progressBar.ProgressColor = mainColor;
            progressBar.ProgressColor2 = secondaryColor;

            ApplyColorTransitionToTimeDisplayLabel(mainColor);

            string percentText = $"{progressValue}%";
            UpdatePercentProgressLabelText(percentText);
            ApplyColorTransitionToPercentProgressLabel(mainColor);

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
            _formNavigator.SwitchToFormWithoutPreviousFormClosing(FormPageEnum.MainPage);
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
        /// Updates the time display gunaLabel with the current elapsed time and calls ApplyColorTransitionToGuna2HtmlLabel. 
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
                        ApplyColorTransitionToGuna2HtmlLabel(timeDisplayLabel, (Color)(timeDisplayLabel.Tag ?? Color.White), mainColor);
                        timeDisplayLabel.Tag = mainColor;
                    }

                    timeDisplayLabel.Text = formattedTimeSpan;
                }
            }
        }

        /// <summary>
        /// The method creates a timer with a 5ms interval, which will update the color multiple times per second.
        private void ApplyColorTransitionToGuna2HtmlLabel(Guna2HtmlLabel label, Color fromColor, Color toColor)
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
        /// Calculates the gradient colors for the progress bar based on completion percentage
        /// Uses linear interpolation (lerp) to find the colour value between two colors relative to the progress.
        /// 
        private (Color MainColor, Color SecondaryColor) GetProgressColors(double progress)
        {
            //Start with RGB 255,81,195(pink/magenta) transitioning to 64,230,200(teal/cyan)

            int r = (int)(255 * (1 - progress) + 64 * progress);
            int g = (int)(81 * (1 - progress) + 230 * progress);
            int b = (int)(195 * (1 - progress) + 200 * progress);
            Color mainColor = Color.FromArgb(r, g, b);
            Color secondaryColor;

            if (progress < 0.5)
            {
                // First half: Secondary color transitions from darker pink (RGB 200,60,170) 
                // to turquoise (RGB 0,180,190)

                // When calculating the lerp we use (progress * 2) to offset only using half of the progress value. 
                int r2 = (int)(200 * (1 - progress * 2) + 0 * progress * 2);
                int g2 = (int)(60 * (1 - progress * 2) + 180 * progress * 2);
                int b2 = (int)(170 * (1 - progress * 2) + 190 * progress * 2);
                secondaryColor = Color.FromArgb(r2, g2, b2);
            }
            else
            {
                // Second half: Secondary color transitions from turquoise (RGB 0,180,190)
                // to bright green (RGB 72,255,180)

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