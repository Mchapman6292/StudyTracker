// SessionGoalForm.cs
using CodingTracker.Common.BusinessInterfaces.Authentication;
using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Common.Utilities;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;

namespace CodingTracker.View.PopUpFormService
{
    public partial class SessionGoalPage : Form
    {
        private readonly INotificationManager _notificationManager;
        private readonly IUtilityService _utilityService;
        private readonly IFormStateManagement _formStateManagement;
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormNavigator _formNavigator;
        private readonly IInputValidator _inputValidator;
        private readonly IApplicationLogger _appLogger;
        private readonly IButtonHighlighterService _buttonHighlighterService;
        private readonly IButtonNotificationManager _buttonNotificationManager;

        public string TimeGoal { get; private set; }
        public bool GoalSet { get; private set; } = false;

        public SessionGoalPage(
            ICodingSessionManager codingSessionManager,
            IFormNavigator formSwitcher,
            IInputValidator inputValidator,
            INotificationManager notificationManager,
            IUtilityService utilityService,
            IApplicationLogger appLogger,
            IFormStateManagement formStateManagement,
            IButtonHighlighterService buttonHighlighterService,
            IButtonNotificationManager buttonNotificationManager)
        {
            _codingSessionManager = codingSessionManager;
            _formNavigator = formSwitcher;
            _inputValidator = inputValidator;
            _notificationManager = notificationManager;
            _utilityService = utilityService;
            _appLogger = appLogger;
            _formStateManagement = formStateManagement;
            _buttonHighlighterService = buttonHighlighterService;
            _buttonNotificationManager = buttonNotificationManager;

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
        /// <returns> String to be used in SessionGoalForm top label.  </returns>

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




        private bool ValidateTimeFormat(out bool valid, out List<string> errorMessages, string time)
        {
            errorMessages = new List<string>();
            valid = true;

            if (time.Length != 4 || !int.TryParse(time, out int _))
            {
                string invalidFormatError = "Please enter valid time format HH:MM.";
                errorMessages.Add(invalidFormatError);
                valid = false;
                return false;
            }

            int hours = int.Parse(time.Substring(0, 2));
            int minutes = int.Parse(time.Substring(2, 2));

            if (hours > 23 || minutes > 59)
            {
                valid = false;
                errorMessages.Add("Please enter valid goal, hours < 23 & minutes < 59");
                return false;
            }

            return true;
        }

        private void SetTimeGoalButton_Click(object sender, EventArgs e)
        {
            string timeInputString = timeGoalTextBox.Text;
            int sessionGoalSeconds = ConvertHHMMStringToDurationSeconds(timeInputString);

            bool valid = false;
            List<string> errorMessages = new List<string>();

            _appLogger.Debug($"SetTimeGoalButton pressed:TimeInputString: {timeInputString}, sessionGoalSecondsInt: {sessionGoalSeconds}");
            DateTime startTime = DateTime.Now;


            _appLogger.Debug($"Time string extraced from text box: {timeInputString}");

            if (!ValidateTimeFormat(out valid, out errorMessages, timeInputString))
            {
                _notificationManager.ShowDialogWithMultipleMessages(this, errorMessages);
                timeGoalTextBox.Text = string.Empty;
            }


            _formNavigator.SwitchToTimerAndWaveForm();
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
            _formNavigator.SwitchToForm(FormPageEnum.MainPage);
            this.Close();
        }

        /// <summary>
        /// When user clicks close: Shows "Are you sure you want to exit?" dialog.
        /// If user clicks Yes: Exits app, automatically saving any active coding session through ExitCodingTrackerAsync.
        /// If user clicks No: Dialog closes and user remains in the application.
        /// </summary>
        private void CloseButton_Click(object sender, EventArgs e)
        {
            _buttonNotificationManager.HandleExitRequestAndStopSession(sender, e, this);
        }

        public void HandleSkipButton()
        {

            _formNavigator.SwitchToForm(FormPageEnum.ElapsedTimerForm);
 
        }
    }
}