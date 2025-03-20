using CodingTracker.Business.CodingSessionService.EditSessionPageContextManagers;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.IApplicationLoggers;
using CodingTracker.View.FormService.ColourServices;
using CodingTracker.View.FormService.LayoutServices;
using Guna.UI2.WinForms;
using CodingTracker.View.EditSessionPageService.DataGridRowStates;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.View.EditSessionPageService.DataGridRowManagers;
using System.Text;
using CodingTracker.Common.CommonEnums;

namespace CodingTracker.View.EditSessionPageService.DataGridViewManagers
{
    public interface IDataGridViewManager
    {
        void CONTROLLERUpdateDataGridViewWithCodingSessionList(List<CodingSessionEntity> codingSessions, Guna2DataGridView dataGridView);
        void AddSessionsToDataGrid(List<CodingSessionEntity> codingSessions, Guna2DataGridView dataGridView);
        void ClearRowsMarkedForDeletion(DataGridView dataGrid);

        void ClearEntryInRowToInfoMapping(DataGridViewRow row);
        int GetSessionIdForRowIndex(DataGridViewRow selectedRow);
        void UpdateMarkedDeletionByRow(DataGridViewRow row, bool marked);
        void ResetDataGridAndRowInfoDict(DataGridView dataGridView);
        void SetAllMarkedDeletionToFalse();

    }


    public class DataGridViewManager : IDataGridViewManager
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ILayoutService _layoutService;
        private readonly IEditSessionPageContextManager _editSessionPageContextManager;
        private readonly IRowStateManager _dataGridRowStateManager;




        // Maps the sessionId to the datagridview display, used to tracking session ids for deletion
        private Dictionary<DataGridViewRow, RowState> _rowToInfoMapping { get; set; }
        private int _numberOfSessions = 10;
        private List<int> _currentHighlightedRows = new List<int>();



        public DataGridViewManager(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, ILayoutService layoutService, IEditSessionPageContextManager editSessionPageContextManager, IRowStateManager dataGridRowStateManager)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
            _layoutService = layoutService;
            _editSessionPageContextManager = editSessionPageContextManager;
            _dataGridRowStateManager = dataGridRowStateManager;
            _rowToInfoMapping = new Dictionary<DataGridViewRow, RowState>();
        }





        // Data grid viewing loading methods.

        public void CONTROLLERUpdateDataGridViewWithCodingSessionList(List<CodingSessionEntity> codingSessions, Guna2DataGridView dataGridView)
        {
            using (_layoutService.SuspendLayout(dataGridView))
            {
                if (codingSessions == null || codingSessions.Count == 0)
                {
                    throw new ArgumentException($"Invalid list passed to {nameof(CONTROLLERUpdateDataGridViewWithCodingSessionList)}", nameof(codingSessions));
                }

                ResetDataGridAndRowInfoDict(dataGridView);
                AddSessionsToDataGrid(codingSessions, dataGridView);
            }
        }


        public void AddSessionsToDataGrid(List<CodingSessionEntity> codingSessions, Guna2DataGridView dataGridView)
        {
            foreach (CodingSessionEntity codingSession in codingSessions)
            {
                CreateRowInGrid(codingSession, dataGridView);
            }
        }

        // Creates a dataGridRow in the grid and returns both the dataGridRow and rowIndex

        private void CreateRowInGrid(CodingSessionEntity session, Guna2DataGridView dataGridView)
        {
            int rowIndex = dataGridView.Rows.Add();
            if (rowIndex < 0)
            {
                _appLogger.Error($"Failed to add dataGridRow for SessionID {session.SessionId}. Invalid dataGridRow index returned.");
                throw new ArgumentException($"Invalid index for {nameof(CreateRowInGrid)}, index: {rowIndex}.");
 
            }
            // Assign the dataGridRow to a variable so we can pass it to AddPairToRowToInfoMapping
            DataGridViewRow row = dataGridView.Rows[rowIndex];

            // Create the RowState which holds dataGridRow index, session id & marked for deletion.
            RowState rowState = _dataGridRowStateManager.CreateDataGridRowState(rowIndex, session.SessionId);

       
            // This will throw an exception if any of rows do not populate. 
             TryPopulateDataGridRow(row, session, dataGridView);

             AddPairToRowToInfoMapping(row, rowState);


        }



        private bool SetDataGridViewCell(DataGridViewRow row, int cellIndex, string value)
        {
            if (row == null || cellIndex < 0 || cellIndex >= row.Cells.Count)
            {
                return false;
            }
            row.Cells[cellIndex].Value = value;
            return true;
        }



        private bool TryPopulateDataGridRow(DataGridViewRow row, CodingSessionEntity session, DataGridView dataGridView)
        {
            bool success = true;
            List<string> failedFields = new List<string>();

            if (!SetDataGridViewCell(row, 0, session.SessionId.ToString())) { success = false; failedFields.Add("SessionId"); }
            if (!SetDataGridViewCell(row, 1, session.DurationHHMM)) { success = false; failedFields.Add("Duration"); }
            if (!SetDataGridViewCell(row, 2, session.StartDate.ToShortDateString())) { success = false; failedFields.Add("StartDate"); }
            if (!SetDataGridViewCell(row, 3, session.StartTime.ToString())) { success = false; failedFields.Add("StartTime"); }
            if (!SetDataGridViewCell(row, 4, session.EndDate.ToShortDateString())) { success = false; failedFields.Add("EndDate"); }
            if (!SetDataGridViewCell(row, 5, session.EndTime.ToString())) { success = false; failedFields.Add("EndTime"); }

            if (!success)
            {
                _appLogger.Error($"Failed to populate cells for SessionId {session.SessionId}: {string.Join(", ", failedFields)}");
                throw new InvalidOperationException($"Unable to populate DataGridView for {nameof(TryPopulateDataGridRow)}.");
            }

            return success;
        }





        // Methods for altering _rowToInfoMapping


        public void AddPairToRowToInfoMapping(DataGridViewRow dataGridRow, RowState rowState)
        {
            _rowToInfoMapping.Add(dataGridRow, rowState);
        }

        private void ClearAllInRowToInfoMapping()
        {
            _rowToInfoMapping.Clear();
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
            int rowCount = dataGridView.Rows.Count;
            dataGridView.Rows.Clear();
            _appLogger.Info($"{rowCount} rows deleted in DataGridView for {nameof(ClearALlDataGridRows)}.");
        }



        public void ClearRowsMarkedForDeletion(DataGridView dataGrid)
        {
            List<int> rowsToDelete = GetIndexesMarkedInRowDeletionColour(dataGrid);
            List<int> sortedRowsToDelete = SortRowIndexesDescending(rowsToDelete);

            using (_layoutService.SuspendLayout(dataGrid))
            {
                SetDataGridViewCellToEmptyByRowIndex(dataGrid, sortedRowsToDelete);

            }
        }



        public int GetSessionIdForRowIndex(DataGridViewRow selectedRow)
        {
            if(_rowToInfoMapping.Count < 1)
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
            foreach(RowState rowSate in _rowToInfoMapping.Values)
            {
                _dataGridRowStateManager.UpdateMarkedForDeletionBool(rowSate, false);
            }
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
            foreach(int index in rowIndexes)
            {
                if(index >= 0 && index < dataGrid.Rows.Count) 
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

            for(int i = 1; i < dataGrid.Rows.Count; i++) 
            {
                DataGridViewRow row = dataGrid.Rows[i];
                if(row.DefaultCellStyle.BackColor == ColourService.RowDeletionCrimsonRed)
                {
                    indexesForDeletion.Add(i);
                }
            }
            return indexesForDeletion;
        }

        // When deleting from an array using indexes deletion changes the index of all values above, by sorting from highest to lowest this means the indexes of the rows we still need to change are not affected.
        private List<int> SortRowIndexesDescending(List<int> rowIndexes)
        {
            List<int> sortedIndexes = new List<int>(rowIndexes);
            sortedIndexes.Sort((a, b) => b.CompareTo(a)); // Sort descending
            return sortedIndexes;
        }












    }
}
