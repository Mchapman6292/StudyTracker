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

        // Maps the sessionId to the datagridview display, used to tracking session ids for deletion
        private Dictionary<int, int> _rowIndexToSessionId = new Dictionary<int, int>();
        private bool IsEditSession { get; set; } = false;
        private List<int> _currentHighlightedRows = new List<int>();
        private int _numberOfSessions = 10;
        public string DisplayText { get; set; }
        private string[] SortOptions = new string[]
        {
            "Session Id",
            "Duration",
            "Start Date",
            "Start Time",
            "End Date",
            "End Time"
        };
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
            InitializeComboBoxOptionToSortCriteria();
            UpdateSaveChangesButtonVisibility();
            EditSessionPageComboBox.SelectedIndexChanged += EditSessionPageComboBox_SelectedIndexChanged;
            CustomizeDatePicker();
        }

        private async void EditSessionPage_Load(object sender, EventArgs e)
        {
            await LoadSessionsIntoDataGridView(SessionSortCriteria.None);
            InitializeDataGridViewColumns();
        }

        // Methods to update properties, button behaviour.
        private void UpdateIsEditSession(bool isEditSession)
        {
            IsEditSession = isEditSession;
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

        private void SetSaveChangesButtonImageSize()
        {
            EditSessionPageSaveChangesButton.ImageSize = new Size(33, 33);


        }

        private void UpdateSaveChangesButtonVisibility()
        {
            EditSessionPageSaveChangesButton.Visible = IsEditSession;
        }

        private void SetDeleteButtonImageSize()
        {
            EditSessionPageDeleteButton.ImageSize = new Size(10, 10);
        }







        // Data grid viewing loading methods.


        private void InitializeDataGridViewColumns()
        {
            EditSessionPageDataGridView.Columns.Clear();
            EditSessionPageDataGridView.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "SessionId", HeaderText = "Session ID" },
                new DataGridViewTextBoxColumn { Name = "Duration", HeaderText = "Duration" },
                new DataGridViewTextBoxColumn { Name = "StartDate", HeaderText = "Start Date" },
                new DataGridViewTextBoxColumn { Name = "StartTime", HeaderText = "Start Time" },
                new DataGridViewTextBoxColumn { Name = "EndDate", HeaderText = "End Date" },
                new DataGridViewTextBoxColumn { Name = "EndTime", HeaderText = "End Time" }
            });
        }

        private async Task LoadSessionsIntoDataGridView(SessionSortCriteria sortCriteria)
        {
            ClearGridAndSessionMapping();

            List<CodingSessionEntity> sessions = await _codingSessionRepository.GetRecentSessionsOrderedBySessionSortCriteriaAsync(_numberOfSessions, sortCriteria);
            PopulateGridWithSessions(sessions);
        }


        private void ClearGridAndSessionMapping()
        {
            EditSessionPageDataGridView.Rows.Clear();
            _rowIndexToSessionId.Clear();
        }


        private void SetDataGridViewCell(DataGridViewRow row, int cellIndex, string value)
        {
            row.Cells[cellIndex].Value = value;
        }

        private void PopulateDataGridViewRowCells(int rowIndex, CodingSessionEntity session)
        {
            var row = EditSessionPageDataGridView.Rows[rowIndex];
            SetDataGridViewCell(row, 0, session.SessionId.ToString());
            SetDataGridViewCell(row, 1, session.DurationHHMM);
            SetDataGridViewCell(row, 2, session.StartDate.ToShortDateString());
            SetDataGridViewCell(row, 3, session.StartTime.ToString());
            SetDataGridViewCell(row, 4, session.EndDate.ToShortDateString());
            SetDataGridViewCell(row, 5, session.EndTime.ToString());
        }



        private void AddToRowIndexToSessionId(int rowIndex, int sessionId)
        {
            if (_rowIndexToSessionId.ContainsKey(rowIndex))
            {
                throw new ArgumentException($"RowIndex key: {rowIndex} already exists in {nameof(_rowIndexToSessionId)}");
            }
            _rowIndexToSessionId.Add(rowIndex, sessionId);
        }

        private void PopulateGridWithSessions(List<CodingSessionEntity> sessions)
        {
            foreach (var session in sessions)
            {
                int rowIndex = AddNewRowToGrid(session);
                if (rowIndex < 0) continue;  // Skip if row addition failed

                AddToRowIndexToSessionId(rowIndex, session.SessionId);
                PopulateDataGridViewRowCells(rowIndex, session);
            }
        }



        private int AddNewRowToGrid(CodingSessionEntity session)
        {
            int rowIndex = EditSessionPageDataGridView.Rows.Add();
            if (rowIndex < 0)
            {
                _appLogger.Error($"Failed to add row for SessionID {session.SessionId}. Invalid row index returned.");
            }
            return rowIndex;
        }

















        // Row Selection & Highlighting. 

        private bool CheckValidRowSelectedDuringEditSession(int rowIndex)
        {
            return IsEditSession && rowIndex >= 0;
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
            if (IsEditSession)
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
            if (IsEditSession)
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
            if (!IsEditSession)
            {
                throw new InvalidOperationException($"Error for {nameof(DeleteSessionButton_Click)}. IsEditSession is set to {IsEditSession.ToString()}, session editing must be enabled to delete sessions.");
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
            await RefreshDataGridView(this, EventArgs.Empty);
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
            UpdateSaveChangesButtonVisibility();
        }

        private void SetEditMode()
        {
            SetEditSessionNotificationPaintColours();
            SetEditSessionButtonColours();

            if (IsEditSession)
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
            EditSessionPageComboBox.Items.AddRange(SortOptions);
        }

        private void InitializeComboBoxOptionToSortCriteria()
        {
            Dictionary<string, SessionSortCriteria> comboBoxOptionToSortCriteria = new Dictionary<string, SessionSortCriteria>
            {
                { SortOptions[0], SessionSortCriteria.SessionId },
                { SortOptions[1], SessionSortCriteria.Duration },
                { SortOptions[2], SessionSortCriteria.StartDate },
                { SortOptions[3], SessionSortCriteria.StartTime },
                { SortOptions[4], SessionSortCriteria.EndDate },
                { SortOptions[5], SessionSortCriteria.EndTime }
            };
            ComboBoxOptionToSortCriteria = comboBoxOptionToSortCriteria;
        }


        public SessionSortCriteria GetSortCriteriaFromComboBoxSelection(string selectedOption)
        {
            return ComboBoxOptionToSortCriteria.TryGetValue(selectedOption, out SessionSortCriteria criteria)
                ? criteria
                : throw new ArgumentException($"Unable to parse Criteria: {selectedOption}.");
        }


        private async Task RefreshDataGridView(object sender, EventArgs e)
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
            await RefreshDataGridView(sender, e);
        }









        // Time Picker Management
        private void CustomizeDatePicker()
        {
            EditSessionPageTimePicker.BackColor = ColourService.DarkerGrey;
            EditSessionPageTimePicker.FillColor = ColourService.DarkerGrey;
            EditSessionPageTimePicker.BorderColor = ColourService.MediumGrey;
            EditSessionPageTimePicker.ForeColor = ColourService.White;
            EditSessionPageTimePicker.Font = new Font("Segoe UI", 10F);



            EditSessionPageTimePicker.FocusedColor = ColourService.LightTeal;
            EditSessionPageTimePicker.Size = new Size(200, 36);
        }

        private async void EditSessionPageDeleteButton_Click(object sender, EventArgs e)
        {
            if (!IsEditSession)
            {
                throw new InvalidOperationException($"Error for {nameof(DeleteSessionButton_Click)}. IsEditSession is set to {IsEditSession.ToString()}, session editing must be enabled to delete sessions.");
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
            await RefreshDataGridView(this, EventArgs.Empty);
        }
    }
}
