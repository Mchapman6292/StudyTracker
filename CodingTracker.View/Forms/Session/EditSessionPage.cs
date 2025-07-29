using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.View.ApplicationControlService.ButtonNotificationManagers;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.EditSessionPageService;
using CodingTracker.View.Forms.Services.SharedFormServices;
using CodingTracker.View.FormService.ColourServices;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Common.DataInterfaces.Repositories;

namespace CodingTracker.View
{
    /// <summary>
    /// EditSessionForm handles the display and editing of past coding sessions.
    /// Provides controls for sorting, deleting, and toggling edit mode on session data.
    /// </summary>
    public partial class EditSessionPage : Form
    {
        #region Dependencies and State

        private readonly IFormNavigator _formNavigator;
        private readonly IFormManager _formController;
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IDataGridViewManager _dataGridViewManager;
        private readonly INotificationManager _notificationManager;
        private readonly IExitFlowManager _exitFlowManager;
        private readonly IButtonHighlighterService _buttonHighlighterService;

        private bool IsEditSession { get; set; } = false;
        private List<int> _currentHighlightedRows = new();
        private int _numberOfSessions = 10;

        public string DisplayText { get; set; }

        private string[] SortOptions = new[]
        {
            "StudyProject",
            "Duration",
            "Start PanelDateLocal",
            "Start Time",
            "End PanelDateLocal",
            "End Time"
        };

        public Dictionary<string, SessionSortCriteria> ComboBoxOptionToSortCriteria { get; set; }

        #endregion

        #region Constructor

        public EditSessionPage(
            IFormNavigator formSwitcher,
            IApplicationLogger appLogger,
            ICodingSessionRepository codingSessionRepository,
            IDataGridViewManager dataGridViewManager,
            INotificationManager notificationManager,
            IExitFlowManager buttonNotificationManager,
            IButtonHighlighterService buttonHighlighterService)
        {
            _appLogger = appLogger;
            _formNavigator = formSwitcher;
            _codingSessionRepository = codingSessionRepository;
            _dataGridViewManager = dataGridViewManager;
            _notificationManager = notificationManager;
            _exitFlowManager = buttonNotificationManager;
            _buttonHighlighterService = buttonHighlighterService;

            InitializeComponent();
            InitializeComboBoxDropDowns();
            InitializeComboBoxOptionToSortCriteria();
            CustomizeDatePicker();
            UpdateDeleteSessionButtonVisibility();

            EditSessionPageComboBox.SelectedIndexChanged += EditSessionPageComboBox_SelectedIndexChanged;
 
        }

        #endregion

        #region Load Logic

        private async void EditSessionPage_Load(object sender, EventArgs e)
        {
            await _dataGridViewManager.ClearAndRefreshDataGridByCriteriaAsync(EditSessionPageDataGridView, SessionSortCriteria.StartDate);
            _buttonHighlighterService.SetButtonHoverColors(toggleEditSessionsButton);
            _buttonHighlighterService.SetButtonHoverColors(deleteSessionButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(toggleEditSessionsButton);
            _buttonHighlighterService.SetButtonBackColorAndBorderColor(deleteSessionButton);

        }

        #endregion

        #region Session Deletion and Edit Toggle

        private void TestEditSessionButton2_CheckedChanged(object sender, EventArgs e)
        {
            UpdateIsEditSession(toggleEditSessionsButton.Checked);
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
                ResetDataGridRowColoursWhenEditSessionOff();
            }

            UpdateDeleteSessionButtonEnabled();
            UpdateDeleteSessionButtonVisibility();
        }

        private void UpdateIsEditSession(bool isEditSession)
        {
            IsEditSession = isEditSession;
        }

        private void UpdateDeleteSessionButtonEnabled(bool? isEditSession = null)
        {
            deleteSessionButton.Enabled = isEditSession ?? IsEditSession;
        }

        private void UpdateDeleteSessionButtonVisibility()
        {
            deleteSessionButton.Visible = IsEditSession;
        }

        private async void EditSessionPageDeleteButton_Click(object sender, EventArgs e)
        {
            if (!IsEditSession)
            {
                throw new InvalidOperationException($"Error: IsEditSession is {IsEditSession}, must be true to delete.");
            }

            UpdateDeleteSessionButtonEnabled(false);

            try
            {
                HashSet<int> sessionsForDeletion = _dataGridViewManager.GetSessionIdsMarkedForDeletion();
                int deletedSessions = await _codingSessionRepository.DeleteSessionsByIdAsync(sessionsForDeletion);
                _dataGridViewManager.DeleteRowInfoMarkedForDeletion();

                string message = deletedSessions == 0 ? "No sessions deleted" : $"{deletedSessions} sessions deleted.";
                _notificationManager.ShowNotificationDialog(this, message);
                ClearCurrentHighlightedRows();
            }
            finally
            {
                List<int> sessionIdsNotMarkedForDeletion = _dataGridViewManager.GetSessionIdsNotMarkedForDeletion();
                List<CodingSessionEntity> sessionsNotDeleted = await _codingSessionRepository.GetSessionsbyIDAsync(sessionIdsNotMarkedForDeletion);

                _dataGridViewManager.CONTROLLERClearAndLoadDataGridViewWithSessions(EditSessionPageDataGridView, sessionsNotDeleted);
                UpdateDeleteSessionButtonEnabled(IsEditSession);
                this.ActiveControl = EditSessionPageComboBox;
            }
        }

        #endregion

        #region Row Interaction and Highlighting

        private void EditModeDataGridView_CellClick(object sender, DataGridViewCellEventArgs dataGridViewArgs)
        {
            if (!CheckValidRowSelectedDuringEditSession(dataGridViewArgs.RowIndex)) return;

            EditSessionPageDataGridView.SuspendLayout();

            if (IsEditSession)
            {
                HandleEditModeDataGridViewClick(dataGridViewArgs.RowIndex);
            }
            else
            {
                UpdateDefaultSelectionColors();
            }

            EditSessionPageDataGridView.ResumeLayout();
        }

        private void HandleEditModeDataGridViewClick(int rowIndex)
        {
            int selectedRowSessionId = _dataGridViewManager.GetSessionIdForRowIndex(EditSessionPageDataGridView.Rows[rowIndex]);
            DataGridViewRow selectedRow = EditSessionPageDataGridView.Rows[rowIndex];

            HighlightRow(selectedRow);
            AddHighlightedRowToCurrentHighlightedRows(selectedRow);
            _dataGridViewManager.UpdateMarkedDeletionByRow(selectedRow, true);
        }

        private bool CheckValidRowSelectedDuringEditSession(int rowIndex)
        {
            return IsEditSession && rowIndex >= 0;
        }

        private void HighlightRow(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = ColorService.RowDeletionCrimsonRed;
        }

        private void AddHighlightedRowToCurrentHighlightedRows(DataGridViewRow row)
        {
            _currentHighlightedRows.Add(row.Index);
        }

        private void ClearCurrentHighlightedRows()
        {
            _currentHighlightedRows.Clear();
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

        #endregion

        #region Colour Management

        private void UpdateDefaultSelectionColors()
        {
            EditSessionPageDataGridView.DefaultCellStyle.SelectionBackColor = ColorService.DarkerGrey;
            EditSessionPageDataGridView.DefaultCellStyle.SelectionForeColor = ColorService.DarkerGrey;
        }

        private void SetEditSessionNotificationPaintColours()
        {
            EditSessionPageNotificationPaint.Text = IsEditSession ? "On" : "Off";
            EditSessionPageNotificationPaint.FillColor = IsEditSession ? ColorService.Teal : ColorService.DarkGrey;
            EditSessionPageNotificationPaint.BorderColor = IsEditSession ? ColorService.LightTeal : ColorService.MediumGrey;
            EditSessionPageNotificationPaint.ForeColor = IsEditSession ? ColorService.White : ColorService.LightGrey;
        }

        private void SetEditSessionButtonColours()
        {
            toggleEditSessionsButton.FillColor = IsEditSession ? ColorService.Teal : ColorService.DarkGrey;
            toggleEditSessionsButton.BorderColor = IsEditSession ? ColorService.LightTeal : ColorService.MediumGrey;
            toggleEditSessionsButton.ForeColor = IsEditSession ? ColorService.White : ColorService.LightGrey;
        }

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

        #endregion

        #region ComboBox DropDown Sorting

        private void InitializeComboBoxDropDowns()
        {
            EditSessionPageComboBox.Items.AddRange(SortOptions);
        }

        private void InitializeComboBoxOptionToSortCriteria()
        {
            ComboBoxOptionToSortCriteria = new()
            {
                { SortOptions[0], SessionSortCriteria.SessionId },
                { SortOptions[1], SessionSortCriteria.Duration },
                { SortOptions[2], SessionSortCriteria.StartDate },
                { SortOptions[3], SessionSortCriteria.StartTime },
                { SortOptions[4], SessionSortCriteria.EndDate },
                { SortOptions[5], SessionSortCriteria.EndTime }
            };
        }

        public SessionSortCriteria GetSortCriteriaFromComboBoxSelection(string selectedOption)
        {
            return ComboBoxOptionToSortCriteria.TryGetValue(selectedOption, out var criteria)
                ? criteria
                : SessionSortCriteria.StartTime;
        }

        private async void EditSessionPageComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedOption = EditSessionPageComboBox?.SelectedItem?.ToString() ?? SortOptions[2];
            SessionSortCriteria selectedCriteria = GetSortCriteriaFromComboBoxSelection(selectedOption);

            await _dataGridViewManager.ClearAndRefreshDataGridByCriteriaAsync(EditSessionPageDataGridView, selectedCriteria);
        }

        #endregion

        #region Time Picker Controls

        private void CustomizeDatePicker()
        {
            EditSessionPageTimePicker.BackColor = ColorService.DarkerGrey;
            EditSessionPageTimePicker.FillColor = ColorService.DarkerGrey;
            EditSessionPageTimePicker.BorderColor = ColorService.MediumGrey;
            EditSessionPageTimePicker.ForeColor = ColorService.White;
            EditSessionPageTimePicker.Font = new Font("Segoe UI", 10F);
            EditSessionPageTimePicker.FocusedColor = ColorService.LightTeal;
            EditSessionPageTimePicker.Size = new Size(200, 36);
            EditSessionPageTimePicker.ShadowDecoration.Enabled = true;
            EditSessionPageTimePicker.ShadowDecoration.Color = ColorService.MediumGrey;
        }

        private async void EditSessionPageTimePicker_ValueChanged(object sender, EventArgs e)
        {
            var date = DateOnly.FromDateTime(EditSessionPageTimePicker.Value);
            await _dataGridViewManager.CONTROLLERClearAndRefreshDataGridByDateAsync(EditSessionPageDataGridView, date);
        }

        #endregion

        #region Navigation and Exit

        private void EditSessionPageBackButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.MainPage);
        }

        private void CodingSessionPageHomeButton_Click(object sender, EventArgs e)
        {
            _formNavigator.SwitchToForm(FormPageEnum.MainPage);
        }

        private void MainPageExitControlMinimizeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
        }






        #endregion


    }
}
