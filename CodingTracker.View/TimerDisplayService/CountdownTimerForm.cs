using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;
using CodingTracker.View.TimerDisplayService.StopWatchTimerServices;
using Guna.UI2.WinForms;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace CodingTracker.View.TimerDisplayService
{
    public partial class CountdownTimerForm : Form
    {
        private bool isPaused = false;
        private bool isDragging = false;
        private Point dragStartPoint;
        private int progressValue = 0;
        private int? sessionGoalSecondsInt;
        private double? progressTimerGoalSecondsDouble;

        private readonly IFormStatePropertyManager _formStatePropertyManager;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IApplicationLogger _appLogger;
        private readonly IUtilityService _utitlityService;
        private readonly INotificationManager _notificationManager;
        private readonly IStopWatchTimerService _stopWatchTimerService;
        private readonly IExitFlowManager _exitFlowManager;

        public CountdownTimerForm(IFormStatePropertyManager formStatePropertyManager, ICodingSessionManager codingSessionManager, IFormSwitcher formSwitcher, IApplicationLogger appLogger, IUtilityService utilityService, INotificationManager notificationManager, IStopWatchTimerService stopWatchTimerService, IExitFlowManager exitFlowManager)
        {
            InitializeComponent();
            _formStatePropertyManager = formStatePropertyManager;
            _codingSessionManager = codingSessionManager;
            _formSwitcher = formSwitcher;
            _utitlityService = utilityService;
            _notificationManager = notificationManager;
            _appLogger = appLogger;
            _stopWatchTimerService = stopWatchTimerService;
            _exitFlowManager = exitFlowManager;

            closeButton.Click += CloseButton_Click;

            sessionGoalSecondsInt = _codingSessionManager.ReturnGoalSeconds();

            SetFormPosition();
            _codingSessionManager.StartCodingSession(DateTime.Now, sessionGoalSecondsInt, true);
        }

        private void SetFormPosition()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;
            int formWidth = this.Width;
            int formHeight = this.Height;

            this.Location = new Point(screenWidth - formWidth - 20, screenHeight - formHeight - 20);
        }

        private void CountdownTimerForm_Load(object sender, EventArgs e)
        {
            progressValue = 0;
            progressBar.Value = 0;

            if (statusLabel != null)
            {
                statusLabel.Text = "0%";
            }

            if(sessionGoalSecondsInt  != 0) 
            {
                progressTimerGoalSecondsDouble = (double)sessionGoalSecondsInt;
            }

            _codingSessionManager.SetCodingSessionStartTimeAndDate(DateTime.Now);
            sessionGoalSecondsInt = _codingSessionManager.ReturnGoalSeconds();
            progressTimer.Start();
            _stopWatchTimerService.StartTimer();
        }

        private void ProgressTimer_Tick(object sender, EventArgs e)
        {
            if(!progressTimerGoalSecondsDouble.HasValue)
            {
                return;
            }

            TimeSpan elapsed = _stopWatchTimerService.ReturnElapsedTimeSpan();
            double percentage = Math.Min(100, (elapsed.TotalSeconds / progressTimerGoalSecondsDouble.Value) * 100);
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
    }
}