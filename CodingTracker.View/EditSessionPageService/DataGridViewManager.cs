using CodingTracker.View.FormService;
using CodingTracker.View.FormPageEnums;
using CodingTracker.View.FormService.ColourServices;
using CodingTracker.View.FormService.LayoutServices;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.IApplicationLoggers;
using Guna.UI2.WinForms;
using CodingTracker.Business.CodingSessionService.EditSessionPageContextManagers;

namespace CodingTracker.View.EditSessionPageService.DataGridViewManagers
{
    public interface IDataGridViewManager
    {

        void ClearRowsMarkedForDeletion(DataGridView dataGrid);
    }


    public class DataGridViewManager : IDataGridViewManager
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;
        private readonly ILayoutService _layoutService;
        private readonly IEditSessionPageContextManager _editSessionPageContextManager;





        // Maps the sessionId to the datagridview display, used to tracking session ids for deletion
        private Dictionary<int, int> _rowIndexToSessionId = new Dictionary<int, int>();
        private int _numberOfSessions = 10;
        private List<int> _currentHighlightedRows = new List<int>();



        public DataGridViewManager(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository, ILayoutService layoutService, IEditSessionPageContextManager editSessionPageContextManager)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
            _layoutService = layoutService;
            _editSessionPageContextManager = editSessionPageContextManager;
        }




   







        /// <summary>
        /// These methods are needed to modify the selection colour behaviour.
        ///  DisableDataGridViewSelectionHighlighting makes selection invisible by matching it to the background.
        ///  UniformDataGridViewRowColors makes all rows uniform by removing the alternating row pattern.

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



        private void ClearDataGridViewCell(DataGridViewRow row, int cellIndex)
        {
            row.Cells[cellIndex].Value = string.Empty;
        }

        private void ClearMultipleDataGridViewCells(DataGridView dataGrid, List<int> rowIndexes)
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

        private void SetDataGridViewCell(DataGridViewRow row, int cellIndex, string value)
        {
            row.Cells[cellIndex].Value = value;
        }


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



        public void ClearRowsMarkedForDeletion(DataGridView dataGrid)
        {
            List<int> rowsToDelete = GetIndexesMarkedInRowDeletionColour(dataGrid);
            List<int> sortedRowsToDelete = SortRowIndexesDescending(rowsToDelete);

            using (_layoutService.SuspendLayout(dataGrid))
            {
                ClearMultipleDataGridViewCells(dataGrid, sortedRowsToDelete);

            }
        }





    }
}
