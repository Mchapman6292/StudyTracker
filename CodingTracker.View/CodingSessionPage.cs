using CodingTracker.Business.CodingSessionService.UserIdServices;
using CodingTracker.Common.BusinessInterfaces;
using CodingTracker.Common.BusinessInterfaces.IAuthenticationServices;
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IErrorHandlers;
using CodingTracker.Common.IInputValidators;
using CodingTracker.View.FormService;
using System.Diagnostics;


namespace CodingTracker.View
{
    public partial class CodingSessionPage : Form
    {
        private readonly IFormController _formController;
        private readonly IFormSwitcher _formSwitcher;
        private readonly ISessionGoalCountDownTimer _goalCountDownTimer;
        private readonly IInputValidator _inputValidator;
        private readonly IErrorHandler _errorHandler;
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IAuthenticationService _authenticationService;

        private readonly UserIdService _userIdService;


        private int _goalHours;
        private int _goalMinutes;
        public CodingSessionPage(IFormSwitcher formSwitcher, IFormController formController,ISessionGoalCountDownTimer goalCountDownTimer, IInputValidator inputValidator, IApplicationLogger appLogger, ICodingSessionManager codingSessionManager, IAuthenticationService authenticationService, UserIdService idService)
        {
            InitializeComponent();
            _formSwitcher = formSwitcher;
            _formController = formController;
            _goalCountDownTimer = goalCountDownTimer;
            _inputValidator = inputValidator;
            _appLogger = appLogger;
            _codingSessionManager = codingSessionManager;
            _authenticationService = authenticationService;
            _userIdService = idService;
        }

        private async void CodingSessionPageStartSessionButton_Click(object sender, EventArgs e)
        {
            _appLogger.Debug($"Coding session StartSession button clicked for {nameof(CodingSessionPageStartSessionButton)}.");

            _codingSessionManager.StartCodingSessionTimer();

            _formSwitcher.SwitchToCodingSessionTimer();
        }


        private async void CodingSesionPageEndSessionButton_Click(object sender, EventArgs e)
        {
            _appLogger.Debug($"Coding session EndSession button clicked for {nameof(CodingSesionPageEndSessionButton)}.");

            _codingSessionManager.EndCodingSessionTimer();


        }



        private void CodingSessionPageConfirmSessionGoalButton_Click(object sender, EventArgs e)
        {
            var activity = new Activity(nameof(CodingSessionPageConfirmSessionGoalButton_Click)).Start();
            var stopwatch = Stopwatch.StartNew();

            int goalHours = Convert.ToInt32(CodingGoalSetHourToggle.Value);
            int goalMinutes = Convert.ToInt32(CodingGoalSetMinToggle.Value);



            activity.Stop();
        }


        public bool ValidateGoalTime(int hours, int minutes)
        {
            var activity = new Activity(nameof(ValidateGoalTime)).Start();
            var stopwatch = Stopwatch.StartNew();

            _appLogger.Debug($"Validating goal time. Hours: {hours}, Minutes: {minutes}, TraceID: {activity.TraceId}");

            int totalMinutes = hours * 60 + minutes;

            if (totalMinutes < 15 || totalMinutes > 24 * 60)
            {
                _appLogger.Warning($"Invalid goal time. Total time {totalMinutes} minutes is outside the valid range. TraceID: {activity.TraceId}");
                stopwatch.Stop();
                activity.Stop();
                return false;
            }

            _appLogger.Info($"Goal time validated successfully. Total time: {totalMinutes} minutes. Execution Time: {stopwatch.ElapsedMilliseconds}ms, TraceID: {activity.TraceId}");

            stopwatch.Stop();
            activity.Stop();
            return true;
        }

        private void CodingSessionPageCodingGoalToggle_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = CodingSessionPageCodingGoalToggle.Checked;

            CodingGoalSetHourToggle.Enabled = isChecked;
            CodingGoalSetMinToggle.Enabled = isChecked;
        }

        public void MinimizeToTray()
        {
            this.Hide();
            this.CodingSessionPageNotifyIcon.Visible = true;

            this.CodingSessionPageNotifyIcon.MouseDoubleClick += (sender, e) =>
            {
                this.Show();
                this.WindowState = FormWindowState.Normal;
                this.CodingSessionPageNotifyIcon.Visible = false;
            };
        }


        private void CodingSessionPageHomeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            _formSwitcher.SwitchToMainPage();
        }
    }
}
