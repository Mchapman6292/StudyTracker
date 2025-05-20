using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormManagement;
using Guna.UI2.WinForms;
using CodingTracker.View.Forms.Services.SharedFormServices.IconDrawingManager;

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
        private readonly IIconDrawingManager _iconDrawingManager;

        #endregion

        #region Constructor

        public CountdownTimerForm(ICodingSessionManager codingSessionManager, IFormNavigator formSwitcher, IApplicationLogger appLogger, IStopWatchTimerService stopWatchTimerService, IExitFlowManager exitFlowManager, IIconDrawingManager iconDrawingManager)
        {
            InitializeComponent();
            _codingSessionManager = codingSessionManager;
            _formNavigator = formSwitcher;
            _appLogger = appLogger;
            _stopWatchTimerService = stopWatchTimerService;
            _exitFlowManager = exitFlowManager;

            closeButton.Click += CloseButton_Click;
            _iconDrawingManager = iconDrawingManager;

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
                pauseButton.Text = "⏸";
                pauseButton.TextOffset = new Point(2, 0);
                isPaused = false;
            }
            else
            {
                _stopWatchTimerService.StopTimer();
                progressTimer.Stop();
                pauseButton.Text = "▶";
                pauseButton.TextOffset = new Point(3, 0);
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
        /// Uses a smooth gradient transition throughout the entire progress range
        /// </summary>
        private (Color MainColor, Color SecondaryColor) GetProgressColors(double progress)
        {
            // Define start, middle, and end colors for a smooth transition
            Color startColor = Color.FromArgb(255, 81, 195);  // Pink/magenta
            Color middleColor = Color.FromArgb(180, 120, 210); // Purple transition
            Color endColor = Color.FromArgb(64, 230, 200);    // Teal/cyan

            Color mainColor, secondaryColor;

            if (progress < 0.5)
            {
                // First half: Transition from start to middle
                double adjustedProgress = progress * 2; // Scale to 0-1 range for first half

                int r = (int)(startColor.R + (middleColor.R - startColor.R) * adjustedProgress);
                int g = (int)(startColor.G + (middleColor.G - startColor.G) * adjustedProgress);
                int b = (int)(startColor.B + (middleColor.B - startColor.B) * adjustedProgress);

                mainColor = Color.FromArgb(r, g, b);

                // Secondary color is slightly darker/richer version of main color
                secondaryColor = Color.FromArgb(
                    (int)(r * 0.8),
                    (int)(g * 0.85),
                    (int)(b * 0.9)
                );
            }
            else
            {
                // Second half: Transition from middle to end
                double adjustedProgress = (progress - 0.5) * 2; // Scale to 0-1 range for second half

                int r = (int)(middleColor.R + (endColor.R - middleColor.R) * adjustedProgress);
                int g = (int)(middleColor.G + (endColor.G - middleColor.G) * adjustedProgress);
                int b = (int)(middleColor.B + (endColor.B - middleColor.B) * adjustedProgress);

                mainColor = Color.FromArgb(r, g, b);

                // Secondary color is slightly brighter version of main color
                // This creates the shimmering effect seen in many modern UI timers
                secondaryColor = Color.FromArgb(
                    Math.Min(255, (int)(r * 1.2)),
                    Math.Min(255, (int)(g * 1.1)),
                    Math.Min(255, (int)(b * 1.05))
                );
            }

            return (mainColor, secondaryColor);
        }

        #endregion
    }
}