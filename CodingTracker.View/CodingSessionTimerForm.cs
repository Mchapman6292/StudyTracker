﻿using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IErrorHandlers;
using CodingTracker.View.FormService;
using System.Diagnostics;
namespace CodingTracker.View
{
    public partial class CodingSessionTimerForm : Form
    {
        private readonly IApplicationLogger _appLogger;
        private readonly IErrorHandler _errorHandler;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IFormController _formController;
        private readonly ISessionGoalCountDownTimer _sessionCountDownTimer;
        private readonly IFormFactory _formFactory;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IUserIdService _userIdService;
        public event Action<TimeSpan> TimeChanged;
        private TimeSpan totalTime;
        public event Action CountDownFinished;



        public CodingSessionTimerForm(IApplicationLogger appLogger, ISessionGoalCountDownTimer countdownTimer, IFormSwitcher formSwitcher, IFormController formController, IFormFactory formFactory, ICodingSessionManager codingSessionManager, IUserIdService userIdService)
        {
            _appLogger = appLogger;
            _sessionCountDownTimer = countdownTimer;
            _formSwitcher = formSwitcher;
            _formController = formController;
            InitializeComponent();
            this.Load += CodingSessionTimerForm_Load;
            _sessionCountDownTimer = countdownTimer;
            _sessionCountDownTimer.TimeChanged += UpdateTimeRemainingDisplay;
            _sessionCountDownTimer.CountDownFinished += HandleCountDownFinished;
            _formFactory = formFactory;
            _codingSessionManager = codingSessionManager;
            _userIdService = userIdService;
        }

        private void CodingSessionTimerForm_Load(object sender, EventArgs e)
        {
            using (var activity = new Activity(nameof(CodingSessionTimerForm_Load)).Start())
            {
                var stopwatch = Stopwatch.StartNew();
                _appLogger.Debug($"Loading Coding Session Timer Form. TraceID: {activity.TraceId}");

                try
                {

                    stopwatch.Stop();
                    _appLogger.Info($"Coding Session Timer Form loaded successfully. Execution Time: {stopwatch.ElapsedMilliseconds}ms, TraceID: {activity.TraceId}");
                }
                catch (Exception ex)
                {
                    stopwatch.Stop();
                    _appLogger.Error($"Error loading Coding Session Timer Form. Execution Time: {stopwatch.ElapsedMilliseconds}ms, Error: {ex.Message}, TraceID: {activity.TraceId}", ex);
                }
            }
        }




        private void UpdateTimeRemainingDisplay(TimeSpan timeRemaining)
        {
            using (var activity = new Activity(nameof(UpdateTimeRemainingDisplay)).Start())
            {
                try
                {
                    Invoke((MethodInvoker)(() =>
                    {
                        Stopwatch stopwatch = Stopwatch.StartNew();
                        CodingSessionTimerPageTimerLabel.Text = timeRemaining.ToString(@"hh\:mm\:ss");
                        double percentage = CalculateRemainingPercentage(timeRemaining);
                        UpdateProgressBar(percentage);
                        stopwatch.Stop();

                    }));
                }
                catch (Exception ex)
                {
                    _appLogger.Error($"An error occurred while updating time remaining display. RemainingTime: {timeRemaining}. Error: {ex.Message}. TraceID: {activity.TraceId}", ex);
                }
            }
        }

        private void HandleCountDownFinished()
        {
            Invoke((MethodInvoker)(() =>
            {
                CodingSessionTimerPageTimerLabel.Text = "00:00:00";
                MessageBox.Show("Countdown complete!");
            }));
        }

        private async void CodingTimerPageEndSessionButton_Click(object sender, EventArgs e)
        {
            int userId = _userIdService.GetCurrentUserId();
            _appLogger.Info($"UserId for {nameof(CodingTimerPageEndSessionButton)} ({userId})");    
            _sessionCountDownTimer.StopCountDownTimer();
            _formFactory.CreateMainPage();
            _formSwitcher.SwitchToMainPage();

            await _codingSessionManager.EndCodingSessionAsync();


            _codingSessionManager.Initialize_CurrentCodingSession(userId);
  
        }

        private void CodingSesisonTimerPageNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void CodingSessionTimerForm_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                CodingSesisonTimerPageNotifyIcon.Visible = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();

                CodingSesisonTimerPageNotifyIcon.Visible = true;
                CodingSesisonTimerPageNotifyIcon.ShowBalloonTip(3000, "Application Minimized", "The application is still running in the background.", ToolTipIcon.Info);
            }
            else
            {
                CodingSesisonTimerPageNotifyIcon.Dispose();
            }
        }


        private double CalculateRemainingPercentage(TimeSpan timeRemaining)
        {
            double totalSeconds = totalTime.TotalSeconds;
            double remainingSeconds = timeRemaining.TotalSeconds;
            return (remainingSeconds / totalSeconds) * 100;
        }

        private void UpdateProgressBar(double percentage)
        {
            CodingSessionTimerPageProgressBar.Value = (int)percentage;
        }

        private void MainPageExitControlMinimizeButton_Click(object sender, EventArgs e)
        {

        }
    }
}
