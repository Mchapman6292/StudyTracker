using CodingTracker.Business.CodingSessionService.EditSessionPageContextManagers;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormService;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService.LayoutServices;
using CodingTracker.View.FormService.ColourServices;
using CodingTracker.Common.CommonEnums;
using System.DirectoryServices;

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
        private bool _isEditSession { get; set; } = false;
        private List<int> _currentHighlightedRows = new List<int>();
        private int _numberOfSessions = 10;
        public string DisplayText { get; set; }
        public SessionSortCriteria SortCriteria { get; set; }

        public Dictionary<string, SessionSortCriteria> ComboBoxOptionToSortCriteria { get; set; }


        public EditSessionPage(IApplicationControl appControl, IFormSwitcher formSwitcher, IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, EditSessionPageContextManager editContextManager, ILayoutService layoutService)
        {
            _appLogger = appLogger;
            _appControl = appControl;
            _formSwitcher = formSwitcher;
            _codingSessionRepository = codingSessionRepository;
            _editSessionPageContextManager = editContextManager;
            _layoutService = layoutService;
            InitializeComponent();
            InitializeComboBoxDropDowns();
        }

        private async void EditSessionPage_Load(object sender, EventArgs e)
        {
            await LoadSessionsIntoDataGridView(SessionSortCriteria.None);
        }

        // Methods to update properties, button behaviour.
        private void UpdateIsEditSession(bool isEditSession)
        {
            _isEditSession = isEditSession;
        }

        private void UpdateDeleteSessionButtonEnabled(bool enabled)
        {
            if (enabled)
            {
                DeleteSessionButton.Enabled = true;
                return;
            }
            DeleteSessionButton.Enabled = false;
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






        // Data grid viewing loading methods.
        private async Task LoadSessionsIntoDataGridView(SessionSortCriteria sortCriteria)
        {
            EditSessionPageDataGridView.Rows.Clear();
            _rowIndexToSessionId.Clear();

            List<CodingSessionEntity> sessions = await _codingSessionRepository.GetRecentSessionsOrderedBySessionSortCriteriaAsync(_numberOfSessions, sortCriteria);

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


        private void AddToRowIndexToSessionId(int rowIndex, int sessionId)
        {
            if (_rowIndexToSessionId.ContainsKey(rowIndex))
            {
                throw new ArgumentException($"RowIndex key: {rowIndex} already exists in {nameof(_rowIndexToSessionId)}");
            }
            _rowIndexToSessionId.Add(rowIndex, sessionId);
        }


        // Row Selection & Highlighting. 

        private bool CheckValidRowSelectedDuringEditSession(int rowIndex)
        {
            return _isEditSession && rowIndex >= 0;
        }

        private void HighlightRow(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = ColourService.CrimsonRed;
        }

        private void AddHighlightedRowToCurrentHighlightedRows(DataGridViewRow row)
        {
            _currentHighlightedRows.Add(row.Index);
        }











        // DataGridView Event Handlers.

        private void EditModeDataGridView_CellClick(object sender, DataGridViewCellEventArgs dataGridViewArgs)
        {
            if (!CheckValidRowSelectedDuringEditSession(dataGridViewArgs.RowIndex))
            {
                return;
            }

            using (_layoutService.SuspendLayout(EditSessionPageDataGridView))
            {
                if (_isEditSession)
                {
                    HandleEditModeClick(dataGridViewArgs.RowIndex);
                }
                else
                {
                    UpdateDefaultSelectionColors();
                }
            }
        }

        private void HandleEditModeClick(int rowIndex)
        {
            int selectedRowSessionId = _rowIndexToSessionId[rowIndex];
            DataGridViewRow selectedRow = EditSessionPageDataGridView.Rows[rowIndex];

            HighlightRow(selectedRow);
            AddHighlightedRowToCurrentHighlightedRows(selectedRow);
            _editSessionPageContextManager.AddSessionIdForDeletion(selectedRowSessionId);
        }



 

 


        // Colour Management


        private void UpdateDefaultSelectionColors()
        {
            EditSessionPageDataGridView.DefaultCellStyle.SelectionBackColor = ColourService.DarkerGrey;
            EditSessionPageDataGridView.DefaultCellStyle.SelectionForeColor = ColourService.DarkerGrey;
        }

        private void SetEditSessionNotificationPaintColours()
        {
            if (_isEditSession)
            {
                EditSessionPageNotificationPaint.Text = "On";
                EditSessionPageNotificationPaint.FillColor = ColourService.Teal;
                EditSessionPageNotificationPaint.BorderColor = ColourService.LightTeal;
                EditSessionPageNotificationPaint.ForeColor = ColourService.White;
            }
            else
            {
                EditSessionPageNotificationPaint.Text = "Off";
                EditSessionPageNotificationPaint.FillColor = ColourService.DarkGrey;
                EditSessionPageNotificationPaint.BorderColor = ColourService.MediumGrey;
                EditSessionPageNotificationPaint.ForeColor = ColourService.LightGrey;
            }
        }

        private void SetEditSessionButtonColours()
        {
            if (_isEditSession)
            {
                TestEditSessionButton2.FillColor = ColourService.Teal;
                TestEditSessionButton2.BorderColor = ColourService.LightTeal;
                TestEditSessionButton2.ForeColor = ColourService.White;
            }
            else
            {
                TestEditSessionButton2.FillColor = ColourService.DarkGrey;
                TestEditSessionButton2.BorderColor = ColourService.MediumGrey;
                TestEditSessionButton2.ForeColor = ColourService.LightGrey;
            }
        }



 

        /// <summary>
        /// These methods are needed to modify the selection colour behaviour.
        ///  DisableDataGridViewSelectionHighlighting makes selection invisible by matching it to the background.
        ///  UniformDataGridViewRowColors makes all rows uniform by removing the alternating row pattern.

        /// </summary>
        private void DisableDataGridViewSelectionHighlighting()
        {
            EditSessionPageDataGridView.DefaultCellStyle.SelectionBackColor = EditSessionPageDataGridView.DefaultCellStyle.BackColor;
            EditSessionPageDataGridView.DefaultCellStyle.SelectionForeColor = EditSessionPageDataGridView.DefaultCellStyle.ForeColor;
        }

        private void UniformDataGridViewRowColors()
        {
            EditSessionPageDataGridView.AlternatingRowsDefaultCellStyle.BackColor = EditSessionPageDataGridView.RowsDefaultCellStyle.BackColor;
            EditSessionPageDataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = EditSessionPageDataGridView.RowsDefaultCellStyle.BackColor;
        }






        private void ResetDataGridRowColoursWhenEditSessionOff()
        {
            foreach (int rowIndex in _currentHighlightedRows)
            {
                var targetRow = EditSessionPageDataGridView.Rows[rowIndex];
                targetRow.DefaultCellStyle.BackColor = EditSessionPageDataGridView.DefaultCellStyle.BackColor;
            }
            _currentHighlightedRows.Clear();
        }

   





  






        // Session Deletion.

        private async void DeleteSessionButton_Click(object sender, EventArgs e)
        {
            if (!_isEditSession)
            {
                throw new InvalidOperationException($"Error for {nameof(DeleteSessionButton_Click)}. _isEditSession is set to {_isEditSession.ToString()}, session editing must be enabled to delete sessions.");
            }

            UpdateDeleteSessionButtonEnabled(false); // Disabled during deletion to prevent multiple clicks etc.
            int deletedSessions = await _editSessionPageContextManager.DeleteSessionsInSessionIdsForDeletion();

            string message = string.Empty;

            if (deletedSessions == 0)
            {
                message = $"No sessions deleted";
            }
            else
            {
                message = $"{deletedSessions} sessions deleted.";
            }

            ShowNotificationDialog(this, EventArgs.Empty, message);
        }






        private void ShowNotificationDialog(object sender, EventArgs e, string message)
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








        // Navigation & Form Controlls

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

        private void EditSessionExitControlBox_Click(object sender, EventArgs e)
        {
            _formController.CloseCurrentForm();
        }










        // Edit Mode Management
        private void TestEditSessionButton2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateIsEditSession(TestEditSessionButton2.Checked);
            SetEditMode();
        }

        private void SetEditMode()
        {
            SetEditSessionNotificationPaintColours();
            SetEditSessionButtonColours();

            if (_isEditSession) 
            {
                DisableDataGridViewSelectionHighlighting();
                UniformDataGridViewRowColors();
            }
            else
            {
                _editSessionPageContextManager.ClearSessionIdsForDeletion();
                ResetDataGridRowColoursWhenEditSessionOff();

            }

            UpdateDeleteSessionButtonEnabled(Enabled);
            UpdateDeleteSessionButtonVisibility(Enabled);
        }








        // ComboBox DropDown logic

        private void InitializeComboBoxDropDowns()
        {
           string[] sortOptions = new string[]
            {
                    "Session Id",
                    "Duration",
                    "Start Date",
                    "Start Time",
                    "End Date",
                    "End Time"
                };

            EditSessionPageComboBox.Items.AddRange(sortOptions); 


        }

        private void InitializeComboBoxOptionToSortCriteria()
        {
            Dictionary<string, SessionSortCriteria> comboBoxOptionToSortCriteria = new Dictionary<string, SessionSortCriteria>
            {
                {"Session Id", SessionSortCriteria.SessionId},
                { "Duration", SessionSortCriteria.Duration },
                { "Start Date", SessionSortCriteria.StartDate },
                { "Start Time", SessionSortCriteria.StartTime },
                { "End Date", SessionSortCriteria.EndDate },
                { "End Time", SessionSortCriteria.EndTime }
            };
        }


        public SessionSortCriteria GetSortCriteriaFromComboBoxSelection(string selectedOption)
        {
            return ComboBoxOptionToSortCriteria.TryGetValue(selectedOption, out SessionSortCriteria criteria)
                ? criteria
                : SessionSortCriteria.None;
        }


        private async Task HandleEditSessionPageComboBoxSelection(object sender, EventArgs e)
        {
            string selectedOption = (string)EditSessionPageComboBox.SelectedItem;
            SessionSortCriteria selectedCriteria = GetSortCriteriaFromComboBoxSelection(selectedOption);

            using (_layoutService.SuspendLayout(EditSessionPageDataGridView))
            {
                await LoadSessionsIntoDataGridView(selectedCriteria);
            }
        }

        private async void EditSessionPageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            await HandleEditSessionPageComboBoxSelection(sender, e);
        }



    }
}
