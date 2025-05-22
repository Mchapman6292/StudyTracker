using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using Guna.UI2.WinForms;
using CodingTracker.View.Forms.Services.CountdownTimerService.CountdownTimerColorManagers;
using CodingTracker.View.Forms.Services.SharedFormServices;


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
        private readonly IButtonNotificationManager _buttonNotificationManager;
        private readonly ICountdownTimerColorManager _countdownTimerColorManager;
        private readonly INotificationManager _notificationManager;


        #endregion

        #region Constructor

        public CountdownTimerForm(ICodingSessionManager codingSessionManager, IFormNavigator formSwitcher, IApplicationLogger appLogger, IStopWatchTimerService stopWatchTimerService, IButtonNotificationManager buttonNotificationManager, ICountdownTimerColorManager countdownTimerColorManager, INotificationManager notificationManager)
        {
            InitializeComponent();
            _codingSessionManager = codingSessionManager;
            _formNavigator = formSwitcher;
            _appLogger = appLogger;
            _stopWatchTimerService = stopWatchTimerService;
            _buttonNotificationManager = buttonNotificationManager;
            _notificationManager = notificationManager;

            closeButton.Click += CloseButton_Click;

            SetFormPosition();
            _countdownTimerColorManager = countdownTimerColorManager;
        }

        #endregion

        #region Form Events

        private void CountdownTimerForm_Load(object sender, EventArgs e)
        {
            // Change after testing          int? sessionGoalSecondsInt = _codingSessionManager.ReturnGoalSeconds(); 
            int? sessionGoalSecondsInt = 20;


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
            timeDisplayLabel.BringToFront();
        }

        private void ApplyColorTransitionToTimeDisplayLabel(Color targetColor)
        {
            if (timeDisplayLabel == null)
                return;

            if (timeDisplayLabel.Tag == null || !((Color)timeDisplayLabel.Tag).ToArgb().Equals(targetColor.ToArgb()))
            {
                Color currentColor = (Color)(timeDisplayLabel.Tag ?? Color.White);
                _countdownTimerColorManager.ApplyColorTransitionToGuna2HtmlLabel(timeDisplayLabel, currentColor, targetColor);
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
                _countdownTimerColorManager.ApplyColorTransitionToGuna2HtmlLabel(percentProgressLabel, currentColor, targetColor);
                percentProgressLabel.Tag = targetColor;
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

        private void UpdateProgressBarValue(int percentage)
        {
            progressBar.Value = percentage;
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            if (!progressTimerGoalSecondsDouble.HasValue)
            {
                return;
            }

            TimeSpan elapsedTime = _stopWatchTimerService.ReturnElapsedTimeSpan();

            UpdateTimeDisplayLabel(elapsedTime);

            double percentage = Math.Min(100, (elapsedTime.TotalSeconds / progressTimerGoalSecondsDouble.Value) * 100);
            progressValue = (int)percentage;
            UpdateProgressBarValue(progressValue);

            (Color mainColor, Color secondaryColor) = _countdownTimerColorManager.GetProgressColors(percentage / 100);

            progressBar.ProgressColor = mainColor;
            progressBar.ProgressColor2 = secondaryColor;



            string percentText = $"{progressValue}%";
            UpdatePercentProgressLabelText(percentText);


            if (progressValue >= 100)
            {
                _codingSessionManager.SetCurrentSessionGoalReached(true);
            }
        }


        private void CloseButton_Click(object sender, EventArgs e)
        {
            _buttonNotificationManager.HandleExitRequestAndStopSession(sender, e, this);
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

            timeDisplayLabel.Text = formattedTimeSpan;
        }



        #endregion

        private void StopButton_Click(object sender, EventArgs e)
        {
            _buttonNotificationManager.HandleStopButtonRequest(this);
        }

        /// <summary>
        /// The logic for starting a coding session is in the forms load method, to restart just reload the form.

        private void RestartSessionButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.CountdownTimerForm);
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}