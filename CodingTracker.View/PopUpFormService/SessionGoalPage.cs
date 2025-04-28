using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.Common.IInputValidators;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.TimerDisplayService.FormStatePropertyManagers;
using Guna.UI2.WinForms;

namespace CodingTracker.View.PopUpFormService
{
    public partial class SessionGoalPage : Form
    {
        private Guna2Panel mainPanel;
        private Guna2HtmlLabel questionLabel;
        private Guna2TextBox timeGoalTextBox;
        private Guna2HtmlLabel formatLabel;
        private Guna2Button SetTimeGoalButton;
        private Guna2Button SkipButton;
        private Guna2BorderlessForm borderlessForm;
        private readonly INotificationManager _notificationManager;
        private readonly IUtilityService _utilityService;

        private readonly ICodingSessionManager _codingSessionManager;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IInputValidator _inputValidator;
        private readonly IFormStatePropertyManager _formStatePropertyManager;
        private readonly IApplicationLogger _appLogger;

        public string TimeGoal { get; private set; }
        public bool GoalSet { get; private set; } = false;
        public SessionGoalPage(ICodingSessionManager codingSessionManager, IFormSwitcher formSwitcher, IInputValidator inputValidator, INotificationManager notificationManager, IFormStatePropertyManager formStatePropertyManager, IUtilityService utilityService, IApplicationLogger appLogger)
        {
            _codingSessionManager = codingSessionManager;
            _formSwitcher = formSwitcher;
            _inputValidator = inputValidator;
            _notificationManager = notificationManager;
            _formStatePropertyManager = formStatePropertyManager;
            _utilityService = utilityService;
            _appLogger = appLogger;

            InitializeComponent();
            InitializeToolsAndComponents();
        }

        private void PopUpForm_Load(object sender, EventArgs e)
        {

        }

        private void InitializeToolsAndComponents()
        {
            this.Size = new Size(400, 250);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(35, 34, 50);
            this.ShowInTaskbar = false;

            borderlessForm = new Guna2BorderlessForm();
            borderlessForm.ContainerControl = this;
            borderlessForm.DragForm = true;
            borderlessForm.BorderRadius = 12;

            mainPanel = new Guna2Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.FillColor = Color.FromArgb(32, 33, 36);
            mainPanel.BorderRadius = 12;
            mainPanel.BorderColor = Color.FromArgb(70, 71, 117);
            mainPanel.BorderThickness = 1;

            questionLabel = new Guna2HtmlLabel();
            questionLabel.Text = "Would you like to set a time goal for this session?";
            questionLabel.ForeColor = Color.White;
            questionLabel.Font = new Font("Segoe UI", 12F, FontStyle.Regular);
            questionLabel.AutoSize = false;
            questionLabel.Size = new Size(350, 40);
            questionLabel.TextAlignment = ContentAlignment.MiddleCenter;
            questionLabel.Location = new Point(25, 30);

            timeGoalTextBox = new Guna2TextBox();
            timeGoalTextBox.PlaceholderText = "0100";
            timeGoalTextBox.BorderRadius = 8;
            timeGoalTextBox.ForeColor = Color.White;
            timeGoalTextBox.FillColor = Color.FromArgb(45, 46, 50);
            timeGoalTextBox.BorderColor = Color.FromArgb(94, 148, 255);
            timeGoalTextBox.Size = new Size(120, 36);
            timeGoalTextBox.Location = new Point(140, 70);
            timeGoalTextBox.MaxLength = 4;
            timeGoalTextBox.TextAlign = HorizontalAlignment.Center;
            timeGoalTextBox.KeyPress += TimeGoalTextBox_KeyPress;

            formatLabel = new Guna2HtmlLabel();
            formatLabel.Text = "Enter time in HHMM format (e.g., 0130 for 1 hour 30 minutes)";
            formatLabel.ForeColor = Color.Silver;
            formatLabel.Font = new Font("Segoe UI", 9F, FontStyle.Regular);
            formatLabel.AutoSize = false;
            formatLabel.Size = new Size(350, 20);
            formatLabel.TextAlignment = ContentAlignment.MiddleCenter;
            formatLabel.Location = new Point(25, 110);

            SetTimeGoalButton = new Guna2Button();
            SetTimeGoalButton.Text = "Set TimeGoal";
            SetTimeGoalButton.FillColor = Color.FromArgb(94, 148, 255);
            SetTimeGoalButton.BorderRadius = 8;
            SetTimeGoalButton.Size = new Size(120, 45);
            SetTimeGoalButton.Location = new Point(70, 150);
            SetTimeGoalButton.Click += SetTimeGoalButton_Click;

            SkipButton = new Guna2Button();
            SkipButton.Text = "Skip";
            SkipButton.FillColor = Color.FromArgb(72, 73, 77);
            SkipButton.BorderRadius = 8;
            SkipButton.Size = new Size(120, 45);
            SkipButton.Location = new Point(210, 150);
            SkipButton.Click += SkipButton_Click;

            mainPanel.Controls.Add(questionLabel);
            mainPanel.Controls.Add(timeGoalTextBox);
            mainPanel.Controls.Add(formatLabel);
            mainPanel.Controls.Add(SetTimeGoalButton);
            mainPanel.Controls.Add(SkipButton);
            this.Controls.Add(mainPanel);
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

        // Change input time values 

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

        public void HandleSkipButton()
        {
            bool goalSet = false;
            DateTime StartTime = DateTime.Now;
            _codingSessionManager.StartCodingSession(StartTime, null, goalSet);

            _formSwitcher.SwitchToForm(FormPageEnum.OrbitalTimerPage);
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}

