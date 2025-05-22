using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormService.ColourServices;
using Guna.UI2.WinForms;
using Microsoft.Extensions.Configuration;

// **IMPORTANT
// The DataGridView object will always have a row even when row.Clear is called, this is an empty row for new records which typically has null values until edited. 
// This means any attempted to edit/clear the DataGrid must check for this and skip using if(row.IsNewRow).

namespace CodingTracker.View.Forms.Services.EditSessionPageService
{
    public enum DataGridViewColumns
    {
        StudyProject,
        Duration,
        StartDate,
        StartTime,
        EndDate,
        EndTime
    }

    public interface IDataGridViewManager
    {
        void AddPairToRowToInfoMapping(DataGridViewRow dataGridRow, RowState rowState);
        void ClearEntryInRowToInfoMapping(DataGridViewRow row);
        void ClearAllInRowToInfoMapping();
        void HideUnusuedColumns(DataGridView dataGrid);
        void ReSizeColumns(DataGridView dataGrid);
        int GetSessionIdForRowIndex(DataGridViewRow selectedRow);
        void UpdateMarkedDeletionByRow(DataGridViewRow row, bool marked);
        void ResetDataGridAndRowInfoDict(DataGridView dataGridView);
        void SetAllMarkedDeletionToFalse();
        void LoadDataGridViewWithSessions(DataGridView dataGrid, List<CodingSessionEntity> codingSessions);
        void ClearDataGridViewDataSource(DataGridView dataGrid);
        void ClearDataGridViewColumns(DataGridView dataGrid);
        void CreateRowStateAndAddToDictWithDataGridRow(DataGridView dataGrid);
        void RefreshDataGridView(DataGridView dataGrid);
        void RenameDataGridColumns(DataGridView dataGrid);
        void FormatDataGridViewDateData(DataGridView dataGrid);
        Task ClearAndRefreshDataGridByCriteriaAsync(DataGridView dataGridView, SessionSortCriteria sessionSortCriteria);
        Task CONTROLLERClearAndRefreshDataGridByDateAsync(DataGridView dataGrid, DateOnly date);
        void CONTROLLERClearAndLoadDataGridViewWithSessions(DataGridView dataGrid, List<CodingSessionEntity> codingSessions);
        HashSet<int> GetSessionIdsMarkedForDeletion();
        List<int> GetSessionIdsNotMarkedForDeletion();
        void DeleteRowInfoMarkedForDeletion();
    }

    public class DataGridViewManager : IDataGridViewManager
    {
        #region Properties and Fields

        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly IRowStateManager _dataGridRowStateManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilityService _utilityService;

        // Maps the sessionId to the datagridview display, used to tracking session ids for deletion
        private Dictionary<DataGridViewRow, RowState> _rowToInfoMapping { get; set; }
        private List<string> _visibleColumns;
        private int _numberOfSessions = 10;
        private List<int> _currentHighlightedRows = new List<int>();

        #endregion

        #region Constructor



        public DataGridViewManager(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, IRowStateManager dataGridRowStateManager, IConfiguration configuration, IUtilityService utilityService)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
            _dataGridRowStateManager = dataGridRowStateManager;
            _configuration = configuration;
            _utilityService = utilityService;
            _rowToInfoMapping = new Dictionary<DataGridViewRow, RowState>();
            _visibleColumns = new List<string>
            {
                "StudyProject",
                "DurationHHMM",
                "StartDateUTC",   
                "StartTimeUTC",   
                "EndDateUTC",     
                "EndTimeUTC"
            };
        }

        #endregion

        #region Controller Methods

        /// <summary>
        /// Main controller method to clear and reload the DataGridView based on session criteria.
        /// Handles the full sequence of operations from clearing to formatting.
        /// </summary>
        /// <param name="dataGrid">The DataGridView to update</param>
        /// <param name="sessionSortCriteria">The criteria to sort and filter sessions</param>
        public async Task ClearAndRefreshDataGridByCriteriaAsync(DataGridView dataGrid, SessionSortCriteria sessionSortCriteria)
        {
            ClearDataGridViewDataSource(dataGrid);
            ClearDataGridViewColumns(dataGrid);
            List<CodingSessionEntity> codingSessions = await _codingSessionRepository.GetSessionBySessionSortCriteriaAsync(_numberOfSessions, sessionSortCriteria);

            CodingSessionEntity testEntity = codingSessions.FirstOrDefault();

            _appLogger.LogCodingSessionEntity(testEntity);
            

            // Convert dates to local time from utc. 
            ConvertCodingSessionListDatesToLocal(codingSessions);
            LoadDataGridViewWithSessions(dataGrid, codingSessions);
            HideUnusuedColumns(dataGrid);
            RenameDataGridColumns(dataGrid);
            FormatDataGridViewDateData(dataGrid);
              RefreshDataGridView(dataGrid);
            CreateRowStateAndAddToDictWithDataGridRow(dataGrid);
        }

        /// <summary>
        /// Controller method to clear and reload the DataGridView with sessions from a specific date.
        /// </summary>
        /// <param name="dataGrid">The DataGridView to update</param>
        /// <param name="date">The date to filter sessions by</param>
        public async Task CONTROLLERClearAndRefreshDataGridByDateAsync(DataGridView dataGrid, DateOnly date)
        {
            ClearDataGridViewDataSource(dataGrid);
            ClearDataGridViewColumns(dataGrid);
            List<CodingSessionEntity> codingSessions = await _codingSessionRepository.GetAllCodingSessionsByDateOnlyForStartDateAsync(date);
            _appLogger.Debug($"Number of sessions retrieved: {codingSessions.Count}.");
            LoadDataGridViewWithSessions(dataGrid, codingSessions);
            HideUnusuedColumns(dataGrid);
            RenameDataGridColumns(dataGrid);
            FormatDataGridViewDateData(dataGrid);
            RefreshDataGridView(dataGrid);
            CreateRowStateAndAddToDictWithDataGridRow(dataGrid);
        }

        /// <summary>
        /// Controller method to clear and load a DataGridView with a provided list of coding sessions.
        /// </summary>
        /// <param name="dataGrid">The DataGridView to update</param>
        /// <param name="codingSessions">The list of sessions to display</param>
        public void CONTROLLERClearAndLoadDataGridViewWithSessions(DataGridView dataGrid, List<CodingSessionEntity> codingSessions)
        {
            ClearDataGridViewDataSource(dataGrid);
            ClearDataGridViewColumns(dataGrid);
            LoadDataGridViewWithSessions(dataGrid, codingSessions);
            HideUnusuedColumns(dataGrid);
            RenameDataGridColumns(dataGrid);
            FormatDataGridViewDateData(dataGrid);
            RefreshDataGridView(dataGrid);
            CreateRowStateAndAddToDictWithDataGridRow(dataGrid);
        }

        #endregion

        #region DataGrid Management

        /// <summary>
        /// Loads the DataGridView with the provided list of coding sessions
        /// </summary>
        public void LoadDataGridViewWithSessions(DataGridView dataGrid, List<CodingSessionEntity> codingSessions)
        {
            if (!codingSessions.Any())
            {
                _appLogger.Debug($"No coding sessions passed to {nameof(LoadDataGridViewWithSessions)}.");
            }
            dataGrid.DataSource = codingSessions;
            RefreshDataGridView(dataGrid);
        }

        public void RefreshDataGridView(DataGridView dataGrid)
        {
            dataGrid.Refresh();
        }

 
        public void ClearDataGridViewDataSource(DataGridView dataGrid)
        {
            dataGrid.DataSource = null;
            dataGrid.Rows.Clear();
        }


        public void ClearDataGridViewColumns(DataGridView dataGrid)
        {
            dataGrid.Columns.Clear();
        }

        /// <summary>
        ///
        /// </summary>
        public void CreateRowStateAndAddToDictWithDataGridRow(DataGridView dataGrid)
        {
            _appLogger.Debug($"Count of {nameof(_rowToInfoMapping)} entries before {nameof(CreateRowStateAndAddToDictWithDataGridRow)} called: {_rowToInfoMapping.Count}");
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (row.Cells["SessionId"].Value is int sessionId)
                {
                    RowState rowState = _dataGridRowStateManager.CreateDataGridRowState(row.Index, sessionId);
                    AddPairToRowToInfoMapping(row, rowState);
                }
            }
            _appLogger.Debug($"Count of {nameof(_rowToInfoMapping)} entries after {nameof(CreateRowStateAndAddToDictWithDataGridRow)} called: {_rowToInfoMapping.Count}");
        }


        private void ClearALlDataGridRows(DataGridView dataGridView)
        {
            _appLogger.Info($"Predeletion row count: {dataGridView.RowCount}.");
            bool originalAllowAddRows = dataGridView.AllowUserToAddRows;

            // Temporarily disable the "new row" feature
            dataGridView.AllowUserToAddRows = false;

            int rowCount = dataGridView.Rows.Count;
            dataGridView.Rows.Clear();

            // Restore the original setting
            dataGridView.AllowUserToAddRows = originalAllowAddRows;

            _appLogger.Info($"{rowCount} rows deleted in DataGridView for {nameof(ClearALlDataGridRows)}.");
            _appLogger.Info($"Remaining rows: {dataGridView.Rows.Count}.");
        }


        public void ResetDataGridAndRowInfoDict(DataGridView dataGridView)
        {
            ClearALlDataGridRows(dataGridView);
            ClearAllInRowToInfoMapping();
        }

        #endregion

        #region RowInfo Mapping Management

        public void AddPairToRowToInfoMapping(DataGridViewRow dataGridRow, RowState rowState)
        {
            _rowToInfoMapping.Add(dataGridRow, rowState);
        }


        public void ClearAllInRowToInfoMapping()
        {
            _appLogger.Debug($"Count of {nameof(_rowToInfoMapping)} entries before {nameof(ClearAllInRowToInfoMapping)} called: {_rowToInfoMapping.Count}");
            _rowToInfoMapping.Clear();
            _appLogger.Debug($"Count of {nameof(_rowToInfoMapping)} entries after {nameof(ClearAllInRowToInfoMapping)} called: {_rowToInfoMapping.Count}");
        }


        public void ClearEntryInRowToInfoMapping(DataGridViewRow row)
        {
            _rowToInfoMapping.Remove(row);
        }


        public int GetSessionIdForRowIndex(DataGridViewRow selectedRow)
        {
            if (_rowToInfoMapping.Count < 1)
            {
                throw new KeyNotFoundException($"Count of entries in {nameof(_rowToInfoMapping)} is < 1, Count: {_rowToInfoMapping.Count}.");
            }

            if (!_rowToInfoMapping.TryGetValue(selectedRow, out var rowState))
            {
                throw new KeyNotFoundException($"Unable to find {nameof(RowState)} object in {nameof(_rowToInfoMapping)} for selectedRow.");
            }

            return _rowToInfoMapping[selectedRow].SessionId;
        }

        public void UpdateMarkedDeletionByRow(DataGridViewRow row, bool marked)
        {
            RowState rowState = _rowToInfoMapping[row];
            _dataGridRowStateManager.UpdateMarkedForDeletionBool(rowState, marked);
        }


        public void SetAllMarkedDeletionToFalse()
        {
            foreach (RowState rowSate in _rowToInfoMapping.Values)
            {
                _dataGridRowStateManager.UpdateMarkedForDeletionBool(rowSate, false);
            }
        }

        public HashSet<int> GetSessionIdsMarkedForDeletion()
        {
            HashSet<int> sessionIdsToDelete = new HashSet<int>();
            foreach (var entry in _rowToInfoMapping)
            {
                if (entry.Value.MarkedForDeletion == true)
                {
                    sessionIdsToDelete.Add(entry.Value.SessionId);
                }
            }
            return sessionIdsToDelete;
        }

        public List<int> GetSessionIdsNotMarkedForDeletion()
        {
            List<int> sessionIdsNotMarked = new List<int>();

            foreach (KeyValuePair<DataGridViewRow, RowState> pair in _rowToInfoMapping)
            {
                var rowState = pair.Value;

                if (rowState.MarkedForDeletion == false)
                {
                    sessionIdsNotMarked.Add(rowState.SessionId);
                }
            }
            return sessionIdsNotMarked;
        }

        public void DeleteRowInfoMarkedForDeletion()
        {
            // Cannot modify a collection while iterating through it, first find all rows then delete.
            List<DataGridViewRow> keysToRemove = new List<DataGridViewRow>();

            foreach (var entry in _rowToInfoMapping)
            {
                if (entry.Value.MarkedForDeletion == true)
                {
                    keysToRemove.Add(entry.Key);
                }
            }
            foreach (var key in keysToRemove)
            {
                _rowToInfoMapping.Remove(key);
            }
        }

        #endregion

        #region DataGrid Formatting and Styling

        public void HideUnusuedColumns(DataGridView dataGrid)
        {
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                if (!_visibleColumns.Contains(column.Name))
                {
                    column.Visible = false;
                }
            }

            if (dataGrid.Columns.Contains("StudyProject"))
                dataGrid.Columns["StudyProject"].DisplayIndex = 0;

            if (dataGrid.Columns.Contains("DurationHHMM"))
                dataGrid.Columns["DurationHHMM"].DisplayIndex = 1;

            if (dataGrid.Columns.Contains("StartDateUTC"))
                dataGrid.Columns["StartDateUTC"].DisplayIndex = 2;

            if (dataGrid.Columns.Contains("StartTimeUTC"))
                dataGrid.Columns["StartTimeUTC"].DisplayIndex = 3;

            if (dataGrid.Columns.Contains("EndDateUTC"))
                dataGrid.Columns["EndDateUTC"].DisplayIndex = 4;

            if (dataGrid.Columns.Contains("EndTimeUTC"))
                dataGrid.Columns["EndTimeUTC"].DisplayIndex = 5;

            if (dataGrid.Columns.Contains("SessionId"))
                dataGrid.Columns["SessionId"].Visible = false;
        }

        public void RenameDataGridColumns(DataGridView dataGrid)
        {
            if (dataGrid.Columns.Contains("StudyProject"))
                dataGrid.Columns["StudyProject"].HeaderText = "Study Project";

            if (dataGrid.Columns.Contains("DurationHHMM"))
                dataGrid.Columns["DurationHHMM"].HeaderText = "Duration";

            if (dataGrid.Columns.Contains("StartDateUTC"))
                dataGrid.Columns["StartDateUTC"].HeaderText = "Start Date";

            if (dataGrid.Columns.Contains("StartTimeUTC"))
                dataGrid.Columns["StartTimeUTC"].HeaderText = "Start Time";

            if (dataGrid.Columns.Contains("EndDateUTC"))
                dataGrid.Columns["EndDateUTC"].HeaderText = "End Date";

            if (dataGrid.Columns.Contains("EndTimeUTC"))
                dataGrid.Columns["EndTimeUTC"].HeaderText = "End Time";

            if (dataGrid.Columns.Contains("SessionId"))
                dataGrid.Columns["SessionId"].HeaderText = "Session ID";
        }

        public void ReSizeColumns(DataGridView dataGrid)
        {
            foreach (string columnName in _visibleColumns)
            {
                if (dataGrid.Columns.Contains(columnName))
                {
                    DataGridViewColumn column = dataGrid.Columns[columnName];

                    if (columnName == "StudyProject")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    else if (columnName == "SessionId")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else if (columnName == "StartDateUTC" || columnName == "EndDateUTC")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else if (columnName == "StartTimeUTC" || columnName == "EndTimeUTC")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else if (columnName == "DurationHHMM")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
            dataGrid.AutoResizeColumns();
        }

        public void FormatDataGridViewDateData(DataGridView dataGrid)
        {
            if (dataGrid.Columns.Contains("StudyProject"))
            {
                dataGrid.Columns["StudyProject"].HeaderText = "Study Project";
            }

            if (dataGrid.Columns.Contains("SessionId"))
            {
                dataGrid.Columns["SessionId"].HeaderText = "Session ID";
            }

            if (dataGrid.Columns.Contains("DurationHHMM"))
            {
                dataGrid.Columns["DurationHHMM"].HeaderText = "Duration";
            }

            if (dataGrid.Columns.Contains("StartDateUTC"))
            {
                dataGrid.Columns["StartDateUTC"].DefaultCellStyle.Format = "dd MMM yyyy";
                dataGrid.Columns["StartDateUTC"].HeaderText = "Start Date";
            }

            if (dataGrid.Columns.Contains("EndDateUTC"))
            {
                dataGrid.Columns["EndDateUTC"].DefaultCellStyle.Format = "dd MMM yyyy";
                dataGrid.Columns["EndDateUTC"].HeaderText = "End Date";
            }

            if (dataGrid.Columns.Contains("StartTimeUTC"))
            {
                dataGrid.Columns["StartTimeUTC"].DefaultCellStyle.Format = "h:mm tt";
                dataGrid.Columns["StartTimeUTC"].HeaderText = "Start Time";
            }

            if (dataGrid.Columns.Contains("EndTimeUTC"))
            {
                dataGrid.Columns["EndTimeUTC"].DefaultCellStyle.Format = "h:mm tt";
                dataGrid.Columns["EndTimeUTC"].HeaderText = "End Time";
            }
        }

        /// <summary>
        /// Disables selection highlighting in a Guna2DataGridView
        /// </summary>
        private void DisableDataGridViewSelectionHighlighting(Guna2DataGridView dataGridView)
        {
            dataGridView.DefaultCellStyle.SelectionBackColor = dataGridView.DefaultCellStyle.BackColor;
            dataGridView.DefaultCellStyle.SelectionForeColor = dataGridView.DefaultCellStyle.ForeColor;
        }

        /// <summary>
        /// Makes all rows in a Guna2DataGridView have uniform colors
        /// </summary>
        private void UniformDataGridViewRowColors(Guna2DataGridView dataGridView)
        {
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = dataGridView.RowsDefaultCellStyle.BackColor;
            dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = dataGridView.RowsDefaultCellStyle.BackColor;
        }

        /// <summary>
        /// Resets row colors in a Guna2DataGridView when edit session is off
        /// </summary>
        private void ResetDataGridRowColoursWhenEditSessionOff(Guna2DataGridView dataGridView)
        {
            foreach (int rowIndex in _currentHighlightedRows)
            {
                var targetRow = dataGridView.Rows[rowIndex];
                targetRow.DefaultCellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor;
            }
            _currentHighlightedRows.Clear();
        }

        #endregion

        #region Data Conversion

        /// <summary>
        /// Converts UTC dates in coding sessions to local time
        /// </summary>
        public void ConvertCodingSessionListDatesToLocal(List<CodingSessionEntity> codingSessions)
        {
            if (!codingSessions.Any())
            {
                _appLogger.Info($"CodingSession list empty for {nameof(ConvertCodingSessionListDatesToLocal)}");
                return;
            }
            foreach (var codingSession in codingSessions)
            {
                codingSession.StartDateUTC = DateOnly.FromDateTime(DateTime.SpecifyKind(codingSession.StartDateUTC.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc).ToLocalTime());
                codingSession.StartTimeUTC = codingSession.StartTimeUTC.ToLocalTime();
                codingSession.EndDateUTC = DateOnly.FromDateTime(DateTime.SpecifyKind(codingSession.EndDateUTC.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc).ToLocalTime());
                codingSession.EndTimeUTC = codingSession.EndTimeUTC.ToLocalTime();
            }
        }

        #endregion
    }
}