using CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers;
using CodingTracker.View.FormService.NotificationManagers;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CodingTracker.View
{
    public partial class SessionNotesForm : Form
    {
        private readonly ICodingSessionManager _codingSessionManager;
        private readonly INotificationManager _notificationManager;
        private TimeSpan _sessionDuration;

        public SessionNotesForm(ICodingSessionManager codingSessionManager, INotificationManager notificationManager)
        {
            InitializeComponent();
            _codingSessionManager = codingSessionManager;
            _notificationManager = notificationManager;
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

        }

        private void CloseButton_Click(object sender, EventArgs e)
        {

        }

        private void MinimizeButton_Click(object sender, EventArgs e)
        {

        }
    }
}