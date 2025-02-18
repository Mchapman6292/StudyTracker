using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IErrorHandlers;
using CodingTracker.View.FormService;
using System.Diagnostics;
using CodingTracker.View.FormPageEnums;

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
        private DateTime _lastUpdate = DateTime.MinValue;
        private const int UPDATE_THRESHOLD_MS = 16;




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
        }




        private void UpdateTimeRemainingDisplay(TimeSpan timeRemaining)
        {
            if ((DateTime.Now - _lastUpdate).TotalMilliseconds < UPDATE_THRESHOLD_MS)
                return;

            BeginInvoke((MethodInvoker)(() =>
            {
                CodingSessionTimerPageTimerLabel.Text = timeRemaining.ToString(@"hh\:mm\:ss");
                double percentage = CalculateRemainingPercentage(timeRemaining);
                UpdateProgressBar(percentage);
            }));

            _lastUpdate = DateTime.Now;
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
            _formSwitcher.SwitchToForm(FormPageEnum.MainPage);

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
