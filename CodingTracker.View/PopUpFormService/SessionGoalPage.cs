// SessionGoalPage.cs
using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.ButtonHighlighterServices;
using CodingTracker.View.FormService.NotificationManagers;

namespace CodingTracker.View.PopUpFormService
{
    public partial class SessionGoalPage : Form
    {
        private readonly INotificationManager _notificationManager;
        private readonly IUtilityService _utilityService;
        private readonly IFormStateManagement _formStateManagement;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IInputValidator _inputValidator;
        private readonly IApplicationLogger _appLogger;
        private readonly IButtonHighlighterService _buttonHighlighterService;
        private readonly IExitFlowManager _exitFlowManager;

        public string TimeGoal { get; private set; }
        public bool GoalSet { get; private set; } = false;

        public SessionGoalPage(
            ICodingSessionManager codingSessionManager,
            IFormSwitcher formSwitcher,
            IInputValidator inputValidator,
            INotificationManager notificationManager,
            IUtilityService utilityService,
            IApplicationLogger appLogger,
            IFormStateManagement formStateManagement,
            IButtonHighlighterService buttonHighlighterService,
            IExitFlowManager exitFlowManager)
        {
            _codingSessionManager = codingSessionManager;
            _formSwitcher = formSwitcher;
            _inputValidator = inputValidator;
            _notificationManager = notificationManager;
            _utilityService = utilityService;
            _appLogger = appLogger;
            _formStateManagement = formStateManagement;
            _buttonHighlighterService = buttonHighlighterService;
            _exitFlowManager = exitFlowManager;

            InitializeComponent();
        }

        private void SessionGoalPage_Load(object sender, EventArgs e)
        {
            _buttonHighlighterService.SetButtonHoverColors(setTimeGoalButton);
            _buttonHighlighterService.SetButtonHoverColors(skipButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(setTimeGoalButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(skipButton);
        }


        /// <summary>
        /// Updates real-time input to update a display label as the user types, e.g 130 = 1 hours, 30 minutes.
        /// <param name="timeGoalTextBoxText">The text from timeDisplayLabel.</param>
        /// <returns> String to be used in SessionGoalPage top label.  </returns>

        public string ParseTimeGoalTextBoxInput(string timeGoalTextBoxText)
        {
            if (string.IsNullOrEmpty(timeGoalTextBoxText))
            {
                return string.Empty;
            }
            int inputLength = timeGoalTextBoxText.Length;
            switch (inputLength)
            {
                case 1:
                case 2:
                    {
                        string hoursSubString = timeGoalTextBoxText.Substring(0, inputLength);
                        if (int.TryParse(hoursSubString, out int hoursInt))
                        {
                            return $"{hoursInt} hours";
                        }
                        break;
                    }
                case 3:
                    {
                        string hoursSubString = timeGoalTextBoxText.Substring(0, 1);
                        string minsSubString = timeGoalTextBoxText.Substring(1, 2);
                        if (int.TryParse(hoursSubString, out int hoursInt) && int.TryParse(minsSubString, out int minsInt))
                        {
                            return $"{hoursInt} hours, {minsInt} minutes.";
                        }
                        break;
                    }
                case 4:
                    {
                        string hoursSubString = timeGoalTextBoxText.Substring(0, 2);
                        string minsSubString = timeGoalTextBoxText.Substring(inputLength - 2);
                        if (int.TryParse(hoursSubString, out int hoursInt) && int.TryParse(minsSubString, out int minsInt))
                        {
                            return $"{hoursInt} hours, {minsInt} minutes.";
                        }
                        break;
                    }
            }
            return string.Empty;
        }






        private void UpdateTimeDisplayLabel(string displayText)
        {
            timeDisplayLabel.Text = displayText;
        }




        private void TimeGoalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Only allow digits and control characters (like backspace)
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TimeGoalTextBox_TextChanged(object sender, EventArgs e)
        {
            string goalInputText = timeGoalTextBox.Text;
            string displayText = ParseTimeGoalTextBoxInput(goalInputText);

            UpdateTimeDisplayLabel(displayText);

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
            int sessionGoalSeconds = ConvertHHMMStringToDurationSeconds(timeInputString);

            _appLogger.Debug($"SetTimeGoalButton pressed:TimeInputString: {timeInputString}, sessionGoalSecondsInt: {sessionGoalSeconds}");
            DateTime startTime = DateTime.Now;
            bool goalSet = true;


            _appLogger.Debug($"Time string extraced from text box: {timeInputString}");

            if (!ValidateTimeFormat(timeInputString))
            {
                string message = "Please enter a valid time in HHMM format";
                return;
            }



            _codingSessionManager.InitializeCodingSessionAndSetGoal(sessionGoalSeconds, goalSet);
            /*
            _formStatePropertyManager.SetIsFormGoalSet(true);
            _formStatePropertyManager.SetFormGoalSeconds(sessionGoalSecondsInt);
            */

            _formSwitcher.SwitchToForm(FormPageEnum.WORKINGSessionTimerPage);
        }


        public int ConvertHHMMStringToDurationSeconds(string timeInputString)
        {
            int timeInputLength = timeInputString.Length;
            int result = 0;

            switch (timeInputLength)
            {
                case 0:
                    {
                        return result;
                    }

                case 1:
                case 2:
                    {
                        if (int.TryParse(timeInputString, out int minsInt))
                        {
                            result = minsInt * 60;
                        }
                        break;
                    }

                case 3:
                    {
                        string hoursSubString = timeInputString.Substring(0, 1);
                        string minsSubString = timeInputString.Substring(1, 2);
                        if (int.TryParse(hoursSubString, out int hoursInt) && int.TryParse(minsSubString, out int minsInt))
                        {
                            result = (hoursInt * 3600) + (minsInt * 60);
                        }
                        break;
                    }

                case 4:
                    {
                        string hoursSubString = timeInputString.Substring(0, 2);
                        string minsSubString = timeInputString.Substring(timeInputString.Length - 2);
                        if (int.TryParse(hoursSubString, out int hoursInt) && int.TryParse(minsSubString, out int minsInt))
                        {
                            result = (hoursInt * 3600) + (minsInt * 60);
                        }
                        break;
                    }
            }

            _appLogger.Debug($"Result from {nameof(ConvertHHMMStringToDurationSeconds)}: {result}.");
            return result;
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
        private void CloseButton_Click(object sender, EventArgs e)
        {
            _exitFlowManager.HandleExitRequest(sender, e, this);
        }

        public void HandleSkipButton()
        {
            bool goalSet = false;
            DateTime startTime = DateTime.Now;
            _codingSessionManager.InitializeCodingSessionAndSetGoal(0, goalSet);

            _formSwitcher.SwitchToForm(FormPageEnum.OrbitalTimerPage);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}