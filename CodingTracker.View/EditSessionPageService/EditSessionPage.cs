using CodingTracker.Business.CodingSessionService.EditSessionPageContextManagers;
using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.IApplicationControls;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.EditSessionPageService.DataGridViewManagers;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService;
using CodingTracker.View.FormService.ColourServices;
using CodingTracker.View.FormService.LayoutServices;
using CodingTracker.View.EditSessionPageService.DataGridRowStates;
using CodingTracker.View.EditSessionPageService.DataGridRowManagers;
using System.Windows.Forms;


// Calling SetDataGridViewCellToEmptyByRowIndex instead of ClearALlDataGridRows works to empty the rows and refresh the grid but not remove the rows.
// Maybe try refreshing entire page instead of datagrid once combo box changed?


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
        private readonly IEditSessionPageContextManager _editSessionPageContextManager;
        private readonly ILayoutService _layoutService;
        private readonly IDataGridViewManager _dataGridViewManager;
        private readonly IRowStateManager _dataGridRowStateManager;

        // Maps the sessionId to the datagridview display, used to tracking session ids which are added to EditSessionPageContextManager SessionIdsForDeletion. 
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


        public EditSessionPage(IApplicationControl appControl, IFormSwitcher formSwitcher, IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, IEditSessionPageContextManager editContextManager, ILayoutService layoutService, IDataGridViewManager dataGridViewManager, IRowStateManager dataGridRowStateManager)
        {
            _appLogger = appLogger;
            _appControl = appControl;
            _formSwitcher = formSwitcher;
            _codingSessionRepository = codingSessionRepository;
            _editSessionPageContextManager = editContextManager;
            _layoutService = layoutService;
            _dataGridViewManager = dataGridViewManager;
            _dataGridRowStateManager = dataGridRowStateManager;
            InitializeComponent();
            InitializeComboBoxDropDowns();
            InitializeComboBoxOptionToSortCriteria();
            CustomizeDatePicker();

            // InitializeDataGridViewColumns needs to be called before LoadSessionsIntoDataGridViewAsync
            //InitializeDataGridViewColumns();//
            UpdateDeleteSessionButtonVisibility();
            EditSessionPageComboBox.SelectedIndexChanged += EditSessionPageComboBox_SelectedIndexChanged;
        }

        private async void EditSessionPage_Load(object sender, EventArgs e)
        {
            await LoadSessionsIntoDataGridViewAsync(SessionSortCriteria.StartDate);

        }

        // Methods to update properties, button behaviour.
        private void UpdateIsEditSession(bool isEditSession)
        {
            IsEditSession = isEditSession;
        }

        private void UpdateDeleteSessionButtonEnabled(bool? isEditSession = null)
        {
            EditSessionPageDeleteButton.Enabled = isEditSession ?? IsEditSession;
        }

        private void UpdateDeleteSessionButtonVisibility()
        {
            EditSessionPageDeleteButton.Visible = IsEditSession;
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



        // Calls either GetSessionBySessionSortCriteria or takes a list of coding sessions to load the DatagridView with. This is called by RefreshDataGridViewWithDropBoxFilter which suspends the layout and refreshes the page. 
        private async Task LoadSessionsIntoDataGridViewAsync(SessionSortCriteria? sortCriteria = null, List<CodingSessionEntity>? sessionsForOneDay = null)
        {

            if (sessionsForOneDay == null && sortCriteria != null)
            {
                List<CodingSessionEntity> sessions = await _codingSessionRepository.GetSessionBySessionSortCriteria(_numberOfSessions, sortCriteria);
                PopulateGridWithSessions(sessions);
                return;
            }
            if (sessionsForOneDay != null && sortCriteria == null)
            {
                PopulateGridWithSessions(sessionsForOneDay);
            }
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




        private void PopulateGridWithSessions(List<CodingSessionEntity> sessions)
        {
            foreach (var session in sessions)
            {
                int rowIndex = AddNewRowToGrid(session);
                RowState rowState = _dataGridRowStateManager.CreateDataGridRowState(rowIndex, session.SessionId);
                DataGridViewRow dataGridRow = EditSessionPageDataGridView.Rows[rowIndex];

                _dataGridViewManager.AddPairToRowToInfoMapping(dataGridRow, rowState);

                PopulateDataGridViewRowCells(rowIndex, session);
            }
        }


        // Duplicate with DataGridViewManager?
        private int AddNewRowToGrid(CodingSessionEntity session)
        {
            int rowIndex = EditSessionPageDataGridView.Rows.Add();
            if (rowIndex < 0)
            {
                _appLogger.Error($"Failed to add row for SessionID {session.SessionId}. Invalid row index returned.");
            }
            return rowIndex;
        }





        private async Task UpdateDataViewGrid(SessionSortCriteria? sortCriteria = null, List<CodingSessionEntity>? sessionsForOneDay = null)
        {
            using (_layoutService.SuspendLayout(EditSessionPageDataGridView))
            {
                if (sessionsForOneDay == null && sortCriteria != null)
                {
                    List<CodingSessionEntity> sessions = await _codingSessionRepository.GetSessionBySessionSortCriteria(_numberOfSessions, sortCriteria);
                    PopulateGridWithSessions(sessions);
                    return;
                }
                if (sessionsForOneDay != null && sortCriteria == null)
                {
                    PopulateGridWithSessions(sessionsForOneDay);
                }
            }
        }






















        // Row Selection & Highlighting. 

        private bool CheckValidRowSelectedDuringEditSession(int rowIndex)
        {
            return IsEditSession && rowIndex >= 0;
        }

        private void HighlightRow(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = ColourService.RowDeletionCrimsonRed;
        }

        private void AddHighlightedRowToCurrentHighlightedRows(DataGridViewRow row)
        {
            _currentHighlightedRows.Add(row.Index);
        }

        private void ClearCurrentHighlightedRows()
        {
            _currentHighlightedRows.Clear();
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
                    HandleEditModeDataGridViewClick(dataGridViewArgs.RowIndex);
                }
                else
                {
                    UpdateDefaultSelectionColors();
                }
            }
        }

        private void HandleEditModeDataGridViewClick(int rowIndex)
        {
            int selectedRowSessionId = _dataGridViewManager.GetSessionIdForRowIndex(EditSessionPageDataGridView.Rows[rowIndex]);

            DataGridViewRow selectedRow = EditSessionPageDataGridView.Rows[rowIndex];


            HighlightRow(selectedRow);
            AddHighlightedRowToCurrentHighlightedRows(selectedRow);

            _dataGridViewManager.UpdateMarkedDeletionByRow(selectedRow, true);

            // Remove once bool logic fixed. 
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

            UpdateDeleteSessionButtonEnabled();
            UpdateDeleteSessionButtonVisibility();
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
                : SessionSortCriteria.StartTime;
        }


        private async Task RefreshDataGridViewWithDropBoxFilter(object sender, EventArgs e)
        {
            string selectedOption = EditSessionPageComboBox?.SelectedItem?.ToString() ?? SortOptions[2];
            SessionSortCriteria selectedCriteria = GetSortCriteriaFromComboBoxSelection(selectedOption);


        }


        private async Task CONTROLLERClearAndRefreshDataGridFromComboBoxAsync(object sender, EventArgs e)
        {
            using (_layoutService.SuspendLayout(EditSessionPageDataGridView))
            {
                _dataGridViewManager.ResetDataGridAndRowInfoDict(EditSessionPageDataGridView);
                string selectedOption = EditSessionPageComboBox?.SelectedItem?.ToString() ?? SortOptions[2];
                SessionSortCriteria selectedCriteria = GetSortCriteriaFromComboBoxSelection(selectedOption);
                _appLogger.Debug($"SelectionCriteria: {selectedCriteria.ToString()}");

                List<CodingSessionEntity> codingSessions = await _codingSessionRepository.GetSessionBySessionSortCriteria(_numberOfSessions, selectedCriteria);

                _dataGridViewManager.CONTROLLERUpdateDataGridViewWithCodingSessionList(codingSessions, EditSessionPageDataGridView);

                EditSessionPageDataGridView.Refresh();
            }
        }






        private async void EditSessionPageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = EditSessionPageComboBox?.SelectedItem?.ToString() ?? SortOptions[2];
            SessionSortCriteria selectedCriteria = GetSortCriteriaFromComboBoxSelection(selectedOption);

            await _dataGridViewManager.CONTROLLERClearAndRefreshDataGridByCriteria(EditSessionPageDataGridView, selectedCriteria);
        }









        // Time Picker Management


        // Change to standard DateTimePicker, more customisation options. 
        private void CustomizeDatePicker()
        {
            EditSessionPageTimePicker.BackColor = ColourService.DarkerGrey;
            EditSessionPageTimePicker.FillColor = ColourService.DarkerGrey; // Background color
            EditSessionPageTimePicker.BorderColor = ColourService.MediumGrey; // Border color
            EditSessionPageTimePicker.ForeColor = ColourService.White; // Text color
            EditSessionPageTimePicker.Font = new Font("Segoe UI", 10F);
            EditSessionPageTimePicker.FocusedColor = ColourService.LightTeal; // Border color when focused
            EditSessionPageTimePicker.Size = new Size(200, 36);

            // Simulating Calendar Styling (Only affects text and outline)
            EditSessionPageTimePicker.ShadowDecoration.Enabled = true; // Add shadow for depth
            EditSessionPageTimePicker.ShadowDecoration.Color = ColourService.MediumGrey; // Subtle shadow

        }

        private DateOnly GetDateTimeFromDateTimePicker()
        {
            var date = DateOnly.FromDateTime(EditSessionPageTimePicker.Value);
            _appLogger.Debug($"Date retrieved from DateTimePicker: {date}.");
            return date;

        }

        private async void EditSessionPageTimePicker_ValueChanged(object sender, EventArgs e)
        {
            DateOnly targetDate = GetDateTimeFromDateTimePicker();
            List<CodingSessionEntity>? targetSessions = await _codingSessionRepository.GetAllCodingSessionsByDateTimeForStartDate(targetDate);
            _appLogger.Debug($"{targetSessions.Count} coding sessions retrieved for {nameof(EditSessionPageTimePicker)}.");
            await LoadSessionsIntoDataGridViewAsync(null, targetSessions);
        }







        private async void EditSessionPageDeleteButton_Click(object sender, EventArgs e)
        {
            if (!IsEditSession)
            {
                throw new InvalidOperationException($"Error for {nameof(EditSessionPageDeleteButton)}. IsEditSession is set to {IsEditSession.ToString()}, session editing must be enabled to delete sessions.");
            }

            // If not disabled a race condition occurs and multiple clicks of EditSessionPageDeleteButton_Click are triggered. 
            UpdateDeleteSessionButtonEnabled(false);

            try
            {
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
                _dataGridViewManager.ClearRowsMarkedForDeletion(EditSessionPageDataGridView);
                ClearCurrentHighlightedRows();
            }

            finally
            {
                UpdateDeleteSessionButtonEnabled(IsEditSession);
            }



        }

        private async void TestButton_Click(object sender, EventArgs e)
        {
            _dataGridViewManager.ClearDataGridViewDataSource(EditSessionPageDataGridView);
            _dataGridViewManager.ClearDataGridViewColumns(EditSessionPageDataGridView);
            _dataGridViewManager.ClearAllInRowToInfoMapping();

            List<CodingSessionEntity> codingSessions = await _codingSessionRepository.GetSessionBySessionSortCriteria(_numberOfSessions, SessionSortCriteria.Duration);

            _dataGridViewManager.LoadDataGridViewWithSessions(EditSessionPageDataGridView, codingSessions);
            _dataGridViewManager.HideUnusuedColumns(EditSessionPageDataGridView);
            _dataGridViewManager.RenameDataGridColumns(EditSessionPageDataGridView);
            _dataGridViewManager.FormatDataGridViewDateData(EditSessionPageDataGridView);
            _dataGridViewManager.RefreshDataGridView(EditSessionPageDataGridView);
            _dataGridViewManager.CreateRowStateAndAddToDictWithDataGridRow(EditSessionPageDataGridView);
        }
    }
}
