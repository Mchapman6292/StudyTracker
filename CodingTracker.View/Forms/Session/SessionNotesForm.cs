using Guna.UI2.WinForms;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.SharedFormServices;
using CodingTracker.Common.BusinessInterfaces.CodingSessionService.ICodingSessionManagers;

namespace CodingTracker.View
{
    public partial class SessionNotesForm : Form
    {
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly INotificationManager _notificationManager;
        private readonly IFormNavigator _formNavigator;
        private readonly IExitFlowManager _exitFlowManager;
        private TimeSpan _sessionDuration;

        public SessionNotesForm(ICodingSessionManager codingSessionManager, INotificationManager notificationManager, IFormNavigator formSwitcher, IExitFlowManager buttonNotificationManager)
        {
            InitializeComponent();
            _codingSessionManager = codingSessionManager;
            _notificationManager = notificationManager;
            _exitFlowManager =buttonNotificationManager;
            _formNavigator = formSwitcher;
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



            // Change after testing
            _codingSessionManager.SetSessionStarRating(4);





            bool sessionAdded = await _codingSessionManager.NEWUpdateCodingSessionStudyNotesAndSaveCodingSession(studyProject, studyNotes);

            string successMessage = sessionAdded ? "Coding session saved successfully." : "Error saving coding session.";

            _notificationManager.ShowNotificationDialog(this, successMessage);

            _formNavigator.SwitchToForm(FormPageEnum.SessionRatingForm);
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
            _formNavigator.SwitchToForm(FormPageEnum.MainPage);
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            _exitFlowManager.HandleExitRequestAndStopSession(sender, e, this);
        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {

        }
    }
}