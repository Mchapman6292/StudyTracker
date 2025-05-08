// SessionGoalPage.cs
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.ButtonHighlighterServices;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;

namespace CodingTracker.View.PopUpFormService
{
    public partial class SessionGoalPage : Form
    {
        private readonly INotificationManager _notificationManager;
        private readonly IUtilityService _utilityService;
        private readonly IFormStateManagement _formStateManagement;
        private readonly IApplicationControl _applicationControl;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IInputValidator _inputValidator;
        private readonly IFormStatePropertyManager _formStatePropertyManager;
        private readonly IApplicationLogger _appLogger;
        private readonly IButtonHighlighterService _buttonHighlighterService;

        public string TimeGoal { get; private set; }
        public bool GoalSet { get; private set; } = false;

        public SessionGoalPage(
            ICodingSessionManager codingSessionManager,
            IFormSwitcher formSwitcher,
            IInputValidator inputValidator,
            INotificationManager notificationManager,
            IFormStatePropertyManager formStatePropertyManager,
            IUtilityService utilityService,
            IApplicationLogger appLogger,
            IFormStateManagement formStateManagement,
            IApplicationControl applicationControl,
            IButtonHighlighterService buttonHighlighterService)
        {
            _codingSessionManager = codingSessionManager;
            _formSwitcher = formSwitcher;
            _inputValidator = inputValidator;
            _notificationManager = notificationManager;
            _formStatePropertyManager = formStatePropertyManager;
            _utilityService = utilityService;
            _appLogger = appLogger;
            _formStateManagement = formStateManagement;
            _applicationControl = applicationControl;
            _buttonHighlighterService = buttonHighlighterService;

            InitializeComponent();
        }

        private void PopUpForm_Load(object sender, EventArgs e)
        {
            _buttonHighlighterService.SetButtonHoverColors(setTimeGoalButton);
            _buttonHighlighterService.SetButtonHoverColors(skipButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(setTimeGoalButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(skipButton);
        }

        private void UpdatetimeDisplayLabel(int hours, int minutes)
        {
            string displayMessage = $"{hours} hours, {minutes} minutes";
        }




        private void TimeGoalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digits and control characters (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private bool ValidateTimeFormat(string time)
        {
            if (time.Length != 4 || !int.TryParse(time, out int _))
                return false;

            int hours = int.Parse(time.Substring(0, 2));
            int minutes = int.Parse(time.Substring(2, 2));

            if (hours > 23 || minutes > 59)
                return false;

            return true;
        }

        private void SetTimeGoalButton_Click(object sender, EventArgs e)
        {
            string timeInputString = timeGoalTextBox.Text;
            int sessionGoalSeconds = _utilityService.ConvertHHMMStringToSeconds(timeInputString);
            DateTime startTime = DateTime.Now;
            bool goalSet = true;


            _appLogger.Debug($"Time string extraced from text box: {timeInputString}");

            if (!ValidateTimeFormat(timeInputString))
            {
                string message = "Please enter a valid time in HHMM format";
                return;
            }

            // Goal is taken as HHMM format and convert to seconds.



            _formStatePropertyManager.SetFormGoalTimeHHMM(sessionGoalSeconds);
            _codingSessionManager.StartCodingSession(startTime, sessionGoalSeconds, goalSet);
            /*
            _formStatePropertyManager.SetIsFormGoalSet(true);
            _formStatePropertyManager.SetFormGoalSeconds(sessionGoalSeconds);
            */

            _formSwitcher.SwitchToForm(FormPageEnum.WORKINGSessionTimerPage);
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            HandleSkipButton();
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToForm(FormPageEnum.MainPage);
            this.Close();
        }

        /// <summary>
        /// When user clicks close: Shows "Are you sure you want to exit?" dialog.
        /// If user clicks Yes: Exits app, automatically saving any active coding session through ExitCodingTrackerAsync.
        /// If user clicks No: Dialog closes and user remains in the application.
        /// </summary>
        private async void CloseButton_Click(object sender, EventArgs args)
        {
            DialogResult exitResult = _notificationManager.ReturnExitMessageDialog(this);

            if (exitResult == DialogResult.Yes)
            {
                await _applicationControl.ExitCodingTrackerAsync();
            }
        }

        public void HandleSkipButton()
        {
            bool goalSet = false;
            DateTime startTime = DateTime.Now;
            _codingSessionManager.StartCodingSession(startTime, null, goalSet);

            _formSwitcher.SwitchToForm(FormPageEnum.OrbitalTimerPage);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}