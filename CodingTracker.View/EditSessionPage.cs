using CodingTracker.Business.CodingSessionService.EditSessionPageContextManagers;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormService;
using CodingTracker.View.FormPageEnums;

namespace CodingTracker.View
{
    public partial class EditSessionPage : Form
    {
        private readonly EditSessionPageContextManager EditPageContextManager;


        private readonly IApplicationControl _appControl;
        private readonly IFormSwitcher _formSwitcher;
        private readonly IFormController _formController;
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly EditSessionPageContextManager _editSessionPageContextManager;

        public EditSessionPage(IApplicationControl appControl, IFormSwitcher formSwitcher, IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, EditSessionPageContextManager editContextManager)
        {
            EditPageContextManager = editContextManager;
            _appLogger = appLogger;
            _appControl = appControl;
            _formSwitcher = formSwitcher;
            _codingSessionRepository = codingSessionRepository;
            _editSessionPageContextManager = editContextManager;
            InitializeComponent();
            LoadSessionsIntoDataGridView();
        }

        private void EditSessionPage_Load(object sender, EventArgs e)
        {
            LoadSessionsIntoDataGridView();
        }


        private async Task LoadSessionsIntoDataGridView()
        {
            int numberOfSessions = 20;

            List<CodingSessionEntity> sessions = await _codingSessionRepository.GetRecentSessionsAsync(numberOfSessions);

            EditSessionPageDataGridView.Rows.Clear();

            foreach (var session in sessions)
            {
                int rowIndex = EditSessionPageDataGridView.Rows.Add();
                if (rowIndex < 0)
                {
                    _appLogger.Error($"Failed to add row for SessionID {session.SessionId}. Invalid row index returned.");
                    continue;
                }

                EditSessionPageDataGridView.Rows[rowIndex].Cells[0].Value = session.SessionId;
                EditSessionPageDataGridView.Rows[rowIndex].Cells[1].Value = session.SessionId;
                EditSessionPageDataGridView.Rows[rowIndex].Cells[2].Value = session.DurationHHMM;
                EditSessionPageDataGridView.Rows[rowIndex].Cells[3].Value = session.StartDate;
                EditSessionPageDataGridView.Rows[rowIndex].Cells[4].Value = session.EndDate;

            }
        }



        private void EditModeDataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            _editSessionPageContextManager.UpdateIsEditSessionBool(true);
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = EditSessionPageDataGridView.Rows[e.RowIndex];
                int ConvertedsessionId = Convert.ToInt32(row.Cells["SessionId"].Value);
                _appLogger.Debug($"Converted Session id from datagridview: {ConvertedsessionId}");

                bool IdAlreadyIn_sessionIdsForDeletion = EditPageContextManager.CheckForSessionId(ConvertedsessionId);
                _appLogger.Debug($"IdAlreadyIn_sessionIdsForDeletion: {IdAlreadyIn_sessionIdsForDeletion.ToString()} for {EditModeDataGridView_CellClick}");

                if (!IdAlreadyIn_sessionIdsForDeletion)
                {
                    EditPageContextManager.AddSessionIdForDeletion(ConvertedsessionId);
                    _appLogger.Debug($"Session Id: {SessionId} added to AddSessionIdForDeletion.");
                }
                else
                {
                    EditPageContextManager.RemoveSessionIdForDeletion(ConvertedsessionId);
                }
                HighlightRow(row, IdAlreadyIn_sessionIdsForDeletion);
            }
        }




        private void HighlightRow(DataGridViewRow row, bool IdAlreadyIn_sessionIdsForDeletion)
        {
            if (!IdAlreadyIn_sessionIdsForDeletion)
            {
                row.DefaultCellStyle.BackColor = Color.DarkOrange;
                row.DefaultCellStyle.ForeColor = Color.White;
            }
            else
            {
                row.DefaultCellStyle.BackColor = EditSessionPageDataGridView.DefaultCellStyle.BackColor;
                row.DefaultCellStyle.ForeColor = EditSessionPageDataGridView.DefaultCellStyle.ForeColor;
            }
            _appLogger.Debug($"{nameof(HighlightRow)} executed: RowIndex={row.Index}, Highlighted={IdAlreadyIn_sessionIdsForDeletion}.");
        }

        private void EditSessionExitControlBox_Click(object sender, EventArgs e)
        {
            _formController.CloseCurrentForm();
        }

        private void EditSessionButton_Click(object sender, EventArgs e)
        {
            ToggleEditMode();
            SetDataGridViewEditMode();
            ChangeButtonColorIfEditSession();
            DeleteSessionButton.Visible = true;
        }



        private void ToggleEditMode()
        {
            bool isEditSessionOn = _editSessionPageContextManager.ReturnIsEditSessionBool();
            if (isEditSessionOn)
            {
                _editSessionPageContextManager.UpdateIsEditSessionBool(true);
                EditSessionPageDataGridView.BackgroundColor = Color.Yellow;
            }
            else
            {
                EditSessionPageDataGridView.BackgroundColor = Color.FromArgb(35, 34, 50);
            }
        }

        private void ChangeButtonColorIfEditSession()
        {
            bool isEditSessionOn = _editSessionPageContextManager.ReturnIsEditSessionBool();
            if (isEditSessionOn)
            {
                EditSessionButton.ForeColor = Color.White;
            }
            else
            {
                EditSessionButton.ForeColor = Color.FromArgb(193, 20, 137); // Default dark pink
            }
        }

        private void SetDataGridViewEditMode()
        {
            bool isEditSessionOn = _editSessionPageContextManager.ReturnIsEditSessionBool();
            if (!isEditSessionOn)
            {
                EditSessionButton.ForeColor = Color.White;
            }
            else
            {
                EditSessionPageDataGridView.DefaultCellStyle.SelectionForeColor = Color.FromArgb(255, 140, 0);
            }
        }


        private void UpdateColorsForSelectedSessionsInEditMode()
        {
            throw new NotImplementedException();
        }



        private async void DeleteSessionButton_Click(object sender, EventArgs e)
        {
            bool isEditSessionOn = _editSessionPageContextManager.ReturnIsEditSessionBool();
            if (!isEditSessionOn)
            {
                _appLogger.Error($"Error for {nameof(DeleteSessionButton_Click)}. isEditSessionOn is set to false, session editing must be enabled to delete sessions.");
            }

            DeleteSessionButton.Enabled = false; // Disabled during deletion to prevent multiple clicks etc.

            HashSet<int> deletedSessionIds = _editSessionPageContextManager.GetSessionIdsForDeletion();

            _appLogger.Debug($"Sessionids for deletion: {string.Join(", ", deletedSessionIds)}");

            int deletedSessions = await _codingSessionRepository.DeleteSessionsByIdAsync(deletedSessionIds);

            if (deletedSessions > 0)
            {
                string message = $"Deleted sessions for session ids: {string.Join(", ", deletedSessionIds)}";
                ShowMessageInEditSessionDialogBox(this, EventArgs.Empty, message);
            }
            else
            {
                ShowMessageInEditSessionDialogBox(this, EventArgs.Empty, "No sessions were deleted.");
            }
        }


        private void UpdateDeleteSessionButtonEnabled(bool enabled)
        {
            DeleteSessionButton.Enabled = enabled;

        }


        private void ShowMessageInEditSessionDialogBox(object sender, EventArgs e, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                DisplayMessageBox.Text = "No message provided.";
            }
            else
            {
                DisplayMessageBox.Text = message;
            }

            // Set button text to make it obvious
            DisplayMessageBox.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
            DisplayMessageBox.Caption = "Notification";
            DisplayMessageBox.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;


            DisplayMessageBox.Show();

            SendKeys.Send("{ENTER}");
        }




        private void EditSessionPageBackButton_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToForm(FormPageEnum.MainPage);
        }

        private void CodingSessionPageHomeButton_Click(object sender, EventArgs e)
        {
            _formSwitcher.SwitchToForm(FormPageEnum.MainPage);
        }

        private void MainPageExitControlMinimizeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
