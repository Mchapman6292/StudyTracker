namespace CodingTracker.View.Forms.Services.EditSessionPageService
{
    public interface IRowStateManager
    {
        RowState CreateDataGridRowState(int rowIndex, int sessionId);
        List<RowState> CreateListOfCreateDataGridRowInfo(List<int> rowIndexes, List<int> sessionIds);
        void UpdateMarkedForDeletionBool(RowState row, bool marked);
    }


    public class RowStateManager : IRowStateManager
    {

        public RowState CreateDataGridRowState(int rowIndex, int sessionId)
        {
            return new RowState(rowIndex, sessionId);
        }

        public List<RowState> CreateListOfCreateDataGridRowInfo(List<int> rowIndexes, List<int> sessionIds)
        {
            List<RowState> dataGridRowStates = new List<RowState>();

            throw new NotImplementedException();
        }

        // This should only be called by UpdateMarkedDeletionByRow to update the marked status of rows in the dict.
        public void UpdateMarkedForDeletionBool(RowState rowState, bool marked)
        {
            rowState.MarkedForDeletion = marked;
        }

    }
}
