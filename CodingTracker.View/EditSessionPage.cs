using CodingTracker.Business.CodingSessionService.EditSessionPageContextManagers;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormService;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService.LayoutServices;

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
        private readonly ILayoutService _layoutService;

        private Dictionary<int, int> _rowIndexToSessionId = new Dictionary<int, int>();
        private bool IsEditSession { get; set; } = false;

        private List<int> _currentHighlightedRows = new List<int>();

        private int _numberOfSessions = 10;


        public EditSessionPage(IApplicationControl appControl, IFormSwitcher formSwitcher, IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, EditSessionPageContextManager editContextManager, ILayoutService layoutService)
        {
            _appLogger = appLogger;
            _appControl = appControl;
            _formSwitcher = formSwitcher;
            _codingSessionRepository = codingSessionRepository;
            _editSessionPageContextManager = editContextManager;
            _layoutService = layoutService;
            InitializeComponent();
        }

        private async void EditSessionPage_Load(object sender, EventArgs e)
        {
            await LoadSessionsIntoDataGridView();
        }

        private void AddToRowIndexToSessionId(int rowIndex, int sessionId)
        {
            if(_rowIndexToSessionId.ContainsKey(rowIndex))
            {
                throw new ArgumentException($"RowIndex key: {rowIndex} already exists in {nameof(_rowIndexToSessionId)}");
            }

            _rowIndexToSessionId.Add(rowIndex, sessionId);
        }


        private async Task LoadSessionsIntoDataGridView()
        {
            EditSessionPageDataGridView.Rows.Clear();
            _rowIndexToSessionId.Clear();

            List<CodingSessionEntity> sessions = await _codingSessionRepository.GetRecentSessionsAsync(_numberOfSessions);

            foreach (var session in sessions)
            {

                int rowIndex = EditSessionPageDataGridView.Rows.Add();
                if (rowIndex < 0)
                {
                    _appLogger.Error($"Failed to add row for SessionID {session.SessionId}. Invalid row index returned.");
                    continue;
                }

                AddToRowIndexToSessionId(rowIndex, session.SessionId);

                EditSessionPageDataGridView.Rows[rowIndex].Cells[0].Value = session.SessionId;
                EditSessionPageDataGridView.Rows[rowIndex].Cells[1].Value = session.SessionId;
                EditSessionPageDataGridView.Rows[rowIndex].Cells[2].Value = session.DurationHHMM;
                EditSessionPageDataGridView.Rows[rowIndex].Cells[3].Value = session.StartDate;
                EditSessionPageDataGridView.Rows[rowIndex].Cells[4].Value = session.EndDate;

            }
        }
            
        private void UpdateIsEditSession(bool isEditSession)
        {
            IsEditSession = isEditSession;
        }


        private void EditModeDataGridView_CellClick(object sender, DataGridViewCellEventArgs dataGridViewArgs)
        {
            if(!ChangeRowSelectionColourForEditMode(dataGridViewArgs.RowIndex))
            {
                return;
            }
        
            using (_layoutService.SuspendLayout(EditSessionPageDataGridView))
            {
                if (IsEditSession)
                {
                    HandleEditModeClick(dataGridViewArgs.RowIndex);
                }
                else
                {
                    UpdateDefaultSelectionColors();
                }
            }
        }

        private bool ChangeRowSelectionColourForEditMode(int rowIndex)
        {
            return IsEditSession && rowIndex >= 0;
        }


        private void HandleEditModeClick(int rowIndex)
        {
            int selectedRowSessionId = _rowIndexToSessionId[rowIndex];
            DataGridViewRow selectedRow = EditSessionPageDataGridView.Rows[rowIndex];

            HighlightRow(selectedRow);
            AddHighlightedRowToCurrentHighlightedRows(selectedRow);
            _editSessionPageContextManager.AddSessionIdForDeletion(selectedRowSessionId);
        }


        private void UpdateDefaultSelectionColors()
        {
            EditSessionPageDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 34, 50);
            EditSessionPageDataGridView.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 34, 50);
        }


        private void HighlightRow(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = Color.FromArgb(220, 20, 60);// Crimson red.
            
        }

        private void AddHighlightedRowToCurrentHighlightedRows(DataGridViewRow row)
        {
            _currentHighlightedRows.Add(row.Index);
        }


        private void ResetDataGridRowColoursWhenEditSessionOff()
        {
            foreach (int rowIndex in _currentHighlightedRows)
            {
                var tartgetRow = EditSessionPageDataGridView.Rows[rowIndex];

            }
        }





        private void EditSessionExitControlBox_Click(object sender, EventArgs e)
        {
            _formController.CloseCurrentForm();
        }

  


        private void UpdateDeleteSessionButtonVisibility(bool visible)
        {
            if (visible)
            {
                DeleteSessionButton.Visible = true;
                return;
            }

            DeleteSessionButton.Visible = false;
        }

        private void UpdateDeleteSessionButtonEnabled(bool enabled)
        {
            if(enabled) 
            {
                DeleteSessionButton.Enabled = true;
                return;
            }
            DeleteSessionButton.Enabled = false;
        }






        private async void DeleteSessionButton_Click(object sender, EventArgs e)
        {
            if (!IsEditSession)
            {
                throw new InvalidOperationException($"Error for {nameof(DeleteSessionButton_Click)}. IsEditSession is set to {IsEditSession.ToString()}, session editing must be enabled to delete sessions.");
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


        private void SetEditMode(bool isEnabled)
        {
            IsEditSession = isEnabled;

            // Update UI appearance
            if (isEnabled)
            {
                EditSessionPageNotificationPaint.Text = "On";
                EditSessionPageNotificationPaint.FillColor = Color.FromArgb(0, 128, 128);  // Teal
                EditSessionPageNotificationPaint.BorderColor = Color.FromArgb(0, 180, 180);
                EditSessionPageNotificationPaint.ForeColor = Color.White;

                TestEditSessionButton2.FillColor = Color.FromArgb(0, 128, 128);
                TestEditSessionButton2.BorderColor = Color.FromArgb(0, 180, 180);
                TestEditSessionButton2.ForeColor = Color.White;

                EditSessionPageDataGridView.DefaultCellStyle.SelectionBackColor = EditSessionPageDataGridView.DefaultCellStyle.BackColor;
                EditSessionPageDataGridView.DefaultCellStyle.SelectionForeColor = EditSessionPageDataGridView.DefaultCellStyle.ForeColor;


                EditSessionPageDataGridView.AlternatingRowsDefaultCellStyle.BackColor = EditSessionPageDataGridView.RowsDefaultCellStyle.BackColor;
                EditSessionPageDataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = EditSessionPageDataGridView.RowsDefaultCellStyle.BackColor;

            }
            else
            {
                EditSessionPageNotificationPaint.Text = "Off";
                EditSessionPageNotificationPaint.FillColor = Color.FromArgb(64, 63, 79);
                EditSessionPageNotificationPaint.BorderColor = Color.FromArgb(128, 127, 145);
                EditSessionPageNotificationPaint.ForeColor = Color.FromArgb(200, 200, 220);

                TestEditSessionButton2.FillColor = Color.FromArgb(64, 63, 79);
                TestEditSessionButton2.BorderColor = Color.FromArgb(128, 127, 145);
                TestEditSessionButton2.ForeColor = Color.FromArgb(200, 200, 220);

                EditSessionPageDataGridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(35, 34, 50);
                EditSessionPageDataGridView.DefaultCellStyle.SelectionForeColor = Color.FromArgb(35, 34, 50);

                ClearSessionIdsForDeletion();
            }
            UpdateDeleteSessionButtonEnabled(Enabled);
            UpdateDeleteSessionButtonVisibility(Enabled);
        }


    

        private void ClearSessionIdsForDeletion()
        {
            _editSessionPageContextManager.ClearSessionIdsForDeletion();
            foreach (DataGridViewRow row in EditSessionPageDataGridView.Rows)
            {
                row.DefaultCellStyle.BackColor = EditSessionPageDataGridView.DefaultCellStyle.BackColor;
            }
        }


        private void TestEditSessionButton2_CheckedChanged(object sender, EventArgs e)
        {
            SetEditMode(TestEditSessionButton2.Checked);
        }

    }
}
