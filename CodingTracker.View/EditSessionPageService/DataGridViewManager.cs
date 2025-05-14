using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.IUtilityServices;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.EditSessionPageService.DataGridRowManagers;
using CodingTracker.View.EditSessionPageService.DataGridRowStates;
using CodingTracker.View.FormService.ColourServices;
using CodingTracker.View.FormService.LayoutServices;
using Guna.UI2.WinForms;
using Microsoft.Extensions.Configuration;



// **IMPORTANT
// The DataGridView object will always have a row even when row.Clear is called, this is an empty row for new records which typically has null values until edited. 
// This means any attempted to edit/clear the DataGrid must check for this and skip using if(row.IsNewRow).

namespace CodingTracker.View.EditSessionPageService.DataGridViewManagers
{
    public interface IDataGridViewManager
    {
        void ClearRowsMarkedForDeletion(DataGridView dataGrid);

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
        Task CONTROLLERClearAndRefreshDataGridByCriteriaAsync(DataGridView dataGridView, SessionSortCriteria sessionSortCriteria);
        Task CONTROLLERClearAndRefreshDataGridByDateAsync(DataGridView dataGrid, DateOnly date);
        void CONTROLLERClearAndLoadDataGridViewWithSessions(DataGridView dataGrid, List<CodingSessionEntity> codingSessions);
        HashSet<int> GetSessionIdsMarkedForDeletion();
        List<int> GetSessionIdsNotMarkedForDeletion();
        void DeleteRowInfoMarkedForDeletion();

    }


    public class DataGridViewManager : IDataGridViewManager
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ILayoutService _layoutService;
        private readonly IRowStateManager _dataGridRowStateManager;
        private readonly IConfiguration _configuration;
        private readonly IUtilityService _utilityService;




        // Maps the sessionId to the datagridview display, used to tracking session ids for deletion
        private Dictionary<DataGridViewRow, RowState> _rowToInfoMapping { get; set; }
        private List<String> _visibleColumns;
        private int _numberOfSessions = 10;
        private List<int> _currentHighlightedRows = new List<int>();



        public DataGridViewManager(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, ILayoutService layoutService, IRowStateManager dataGridRowStateManager, IConfiguration configuration, IUtilityService utilityService)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
            _layoutService = layoutService;
            _dataGridRowStateManager = dataGridRowStateManager;
            _configuration = configuration;
            _utilityService = utilityService;
            _rowToInfoMapping = new Dictionary<DataGridViewRow, RowState>();
            _visibleColumns = new List<string>
                {
                    "SessionId",
                    "DurationHHMM",
                    "StartDateLocal",
                    "StartTimeLocal",
                    "EndDateLocal",
                    "EndTimeLocal"
                };
        }










        // Methods for altering _rowToInfoMapping


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

        public void ResetDataGridAndRowInfoDict(DataGridView dataGridView)
        {
            ClearALlDataGridRows(dataGridView);
            ClearAllInRowToInfoMapping();
        }

        // Blanket deletion of all rows
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



        public void ClearRowsMarkedForDeletion(DataGridView dataGrid)
        {
            List<int> rowsToDelete = GetIndexesMarkedInRowDeletionColour(dataGrid);

            using (_layoutService.SuspendLayout(dataGrid))
            {


            }
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

        // Takes DataGridViewRow as a parameter, finds the assocaited rowState in the _rowToInfoMapping & then updates using UpdateMarkedForDeletionBool.
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











        /// <summary>
        /// These methods are needed to modify the selection colour behaviour.
        ///  DisableDataGridViewSelectionHighlighting makes selection invisible by matching it to the background.
        ///  UniformDataGridViewRowColors makes all rows uniform by removing the alternating dataGridRow pattern.

        /// </summary>
        private void DisableDataGridViewSelectionHighlighting(Guna2DataGridView dataGridView)
        {
            dataGridView.DefaultCellStyle.SelectionBackColor = dataGridView.DefaultCellStyle.BackColor;
            dataGridView.DefaultCellStyle.SelectionForeColor = dataGridView.DefaultCellStyle.ForeColor;
        }

        private void UniformDataGridViewRowColors(Guna2DataGridView dataGridView)
        {
            dataGridView.AlternatingRowsDefaultCellStyle.BackColor = dataGridView.RowsDefaultCellStyle.BackColor;
            dataGridView.AlternatingRowsDefaultCellStyle.SelectionBackColor = dataGridView.RowsDefaultCellStyle.BackColor;
        }



        private void ResetDataGridRowColoursWhenEditSessionOff(Guna2DataGridView dataGridView)
        {
            foreach (int rowIndex in _currentHighlightedRows)
            {
                var targetRow = dataGridView.Rows[rowIndex];
                targetRow.DefaultCellStyle.BackColor = dataGridView.DefaultCellStyle.BackColor;
            }
            _currentHighlightedRows.Clear();
        }

        // This alters the DisplayIndex and not the RowIndex so the mapping in _rowToInfo remains unaltered. 

        public void HideUnusuedColumns(DataGridView dataGrid)
        {
            foreach (DataGridViewColumn column in dataGrid.Columns)
            {
                if (!_visibleColumns.Contains(column.Name))
                {
                    column.Visible = false;
                }
            }

            if (dataGrid.Columns.Contains("SessionId"))
                dataGrid.Columns["SessionId"].DisplayIndex = 0;

            if (dataGrid.Columns.Contains("DurationHHMM"))
                dataGrid.Columns["DurationHHMM"].DisplayIndex = 1;

            if (dataGrid.Columns.Contains("StartDateLocal"))
                dataGrid.Columns["StartDateLocal"].DisplayIndex = 2;

            if (dataGrid.Columns.Contains("StartTimeLocal"))
                dataGrid.Columns["StartTimeLocal"].DisplayIndex = 3;

            if (dataGrid.Columns.Contains("EndDateLocal"))
                dataGrid.Columns["EndDateLocal"].DisplayIndex = 4;

            if (dataGrid.Columns.Contains("EndTimeLocal"))
                dataGrid.Columns["EndTimeLocal"].DisplayIndex = 5;

        }

        public void RenameDataGridColumns(DataGridView dataGrid)
        {
            if (dataGrid.Columns.Contains("SessionId"))
                dataGrid.Columns["SessionId"].HeaderText = "Session ID";

            if (dataGrid.Columns.Contains("DurationHHMM"))
                dataGrid.Columns["DurationHHMM"].HeaderText = "Duration";

            if (dataGrid.Columns.Contains("StartDateLocal"))
                dataGrid.Columns["StartDateLocal"].HeaderText = "Start Date";

            if (dataGrid.Columns.Contains("StartTimeLocal"))
                dataGrid.Columns["StartTimeLocal"].HeaderText = "Start Time";

            if (dataGrid.Columns.Contains("EndDateLocal"))
                dataGrid.Columns["EndDateLocal"].HeaderText = "End Date";

            if (dataGrid.Columns.Contains("EndTimeLocal"))
                dataGrid.Columns["EndTimeLocal"].HeaderText = "End Time";
        }

        public void ReSizeColumns(DataGridView dataGrid)
        {
            foreach (string columnName in _visibleColumns)
            {
                if (dataGrid.Columns.Contains(columnName))
                {
                    DataGridViewColumn column = dataGrid.Columns[columnName];

                    if (columnName == "SessionId")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else if (columnName == "StartDateLocal" || columnName == "EndDateLocal")
                    {
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    }
                    else if (columnName == "StartTimeLocal" || columnName == "EndTimeLocal")
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
            if (dataGrid.Columns.Contains("SessionId"))
            {
                dataGrid.Columns["SessionId"].HeaderText = "Session Id";
            }

            if (dataGrid.Columns.Contains("DurationHHMM"))
            {
                dataGrid.Columns["DurationHHMM"].HeaderText = "Duration";
            }

            if (dataGrid.Columns.Contains("StartDateLocal"))
            {
                dataGrid.Columns["StartDateLocal"].DefaultCellStyle.Format = "dd MMM yyyy";
                dataGrid.Columns["StartDateLocal"].HeaderText = "Start Date";
            }

            if (dataGrid.Columns.Contains("EndDateLocal"))
            {
                dataGrid.Columns["EndDateLocal"].DefaultCellStyle.Format = "dd MMM yyyy";
                dataGrid.Columns["EndDateLocal"].HeaderText = "End Date";
            }

            if (dataGrid.Columns.Contains("StartTimeLocal"))
            {
                dataGrid.Columns["StartTimeLocal"].DefaultCellStyle.Format = "h:mm tt";
                dataGrid.Columns["StartTimeLocal"].HeaderText = "Start Time";
            }

            if (dataGrid.Columns.Contains("EndTimeLocal"))
            {
                dataGrid.Columns["EndTimeLocal"].DefaultCellStyle.Format = "h:mm tt";
                dataGrid.Columns["EndTimeLocal"].HeaderText = "End Time";
            }
        }








        private bool CheckValidRowSelectedDuringEditSession(int rowIndex)
        {
            return true;
        }

        private void HighlightRow(DataGridViewRow row)
        {
            row.DefaultCellStyle.BackColor = ColourService.RowDeletionCrimsonRed;
        }

        private void AddHighlightedRowToCurrentHighlightedRows(DataGridViewRow row)
        {
            _currentHighlightedRows.Add(row.Index);
        }



        private void ClearDataGridViewCell(DataGridViewRow row, int rowIndex)
        {
            row.Cells[rowIndex].Value = string.Empty;
        }

        // This takes a list of row indexes instead of a RowState object as the DataGridRows need to be sorted by Ascending
        private void SetDataGridViewCellToEmptyByRowIndex(DataGridView dataGrid, List<int> rowIndexes)
        {
            foreach (int index in rowIndexes)
            {
                if (index >= 0 && index < dataGrid.Rows.Count)
                {
                    foreach (DataGridViewCell cell in dataGrid.Rows[index].Cells)
                    {
                        cell.Value = string.Empty;
                    }
                }
            }
        }




        // Old logic remove when working
        private List<int> GetIndexesMarkedInRowDeletionColour(DataGridView dataGrid)
        {
            List<int> indexesForDeletion = new List<int>();

            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                DataGridViewRow row = dataGrid.Rows[i];
                if (row.DefaultCellStyle.BackColor == ColourService.RowDeletionCrimsonRed)
                {
                    indexesForDeletion.Add(i);
                }
            }
            return indexesForDeletion;
        }

        // When deleting from an array using indexes deletion changes the index of all values above, by sorting from highest to lowest this means the indexes of the rows we still need to change are not affected.





        ////////////// NEW METHODS WHICH USE DATASOURCE


        public async Task CONTROLLERClearAndRefreshDataGridByCriteriaAsync(DataGridView dataGrid, SessionSortCriteria sessionSortCriteria)
        {
            ClearDataGridViewDataSource(dataGrid);
            ClearDataGridViewColumns(dataGrid);
            List<CodingSessionEntity> codingSessions = await _codingSessionRepository.GetSessionBySessionSortCriteriaAsync(_numberOfSessions, sessionSortCriteria);

            // Convert dates to local time from utc. 
            _utilityService.ConvertCodingSessionListDatesToLocal(codingSessions);
            LoadDataGridViewWithSessions(dataGrid, codingSessions);
            HideUnusuedColumns(dataGrid);
            RenameDataGridColumns(dataGrid);
            FormatDataGridViewDateData(dataGrid);
            RefreshDataGridView(dataGrid);
            CreateRowStateAndAddToDictWithDataGridRow(dataGrid);
        }

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

        public void CreateRowStateAndAddToDictWithDataGridRow(DataGridView dataGrid)
        {
            _appLogger.Debug($"Count of {nameof(_rowToInfoMapping)} entries before {nameof(CreateRowStateAndAddToDictWithDataGridRow)} called: {_rowToInfoMapping.Count}");
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                if (row.IsNewRow)
                    continue;

                if (row.Cells["SessionId"].Value is int sessionId)
                {
                    RowState rowState = (_dataGridRowStateManager.CreateDataGridRowState(row.Index, sessionId));
                    AddPairToRowToInfoMapping(row, rowState);
                }
            }
            _appLogger.Debug($"Count of {nameof(_rowToInfoMapping)} entries after {nameof(CreateRowStateAndAddToDictWithDataGridRow)} called: {_rowToInfoMapping.Count}");
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



        public void AddPairToRowToInfoMapping(DataGridViewRow dataGridRow, RowState rowState)
        {
            _rowToInfoMapping.Add(dataGridRow, rowState);
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
























    }
}
