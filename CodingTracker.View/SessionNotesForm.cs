using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.NotificationManagers;
using CodingTracker.View.FormPageEnums;
using Guna.UI2.WinForms;
using CodingTracker.View.ApplicationControlService.ExitFlowManagers;

namespace CodingTracker.View
{
    public partial class SessionNotesForm : Form
    {
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly INotificationManager _notificationManager;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IExitFlowManager _exitFlowManager;
        private TimeSpan _sessionDuration;

        public SessionNotesForm(ICodingSessionManager codingSessionManager, INotificationManager notificationManager, IFormSwitcher formSwitcher, IExitFlowManager exitFlowManager)
        {
            InitializeComponent();
            _codingSessionManager = codingSessionManager;
            _notificationManager = notificationManager;
            _exitFlowManager =exitFlowManager;
            _formSwitcher = formSwitcher;
            closeButton.Click += CloseButton_Click;
        }

        public void SetSessionDuration(TimeSpan duration)
        {
            _sessionDuration = duration;
            titleLabel.Text = $"Session Complete - {_sessionDuration.Hours:D2}:{_sessionDuration.Minutes:D2}:{_sessionDuration.Seconds:D2}";
        }

        private async void SaveButton_Click(object sender, EventArgs e)
        {
            string studyProject = ProjectNameTextBox.Text;
            string studyNotes = SessionNotesTextBox.Text;
            bool sessionAdded = await _codingSessionManager.NEWUpdateCodingSessionStudyNotesAndSaveCodingSession(studyProject, studyNotes);

            string successMessage = sessionAdded ? "Coding session saved successfully." : "Error saving coding session.";

            _notificationManager.ShowNotificationDialog(this, successMessage);

            _formSwitcher.SwitchToForm(FormPageEnum.MainPage);
        }

        private void SkipButton_Click(object sender, EventArgs e)
        {
            // Close without saving
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void SessionNotesForm_Load(object sender, EventArgs e)
        {
            ProjectNameTextBox.Focus();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToForm(FormPageEnum.MainPage);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            _exitFlowManager.HandleExitRequest(sender, e, this);
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {

        }
    }
}