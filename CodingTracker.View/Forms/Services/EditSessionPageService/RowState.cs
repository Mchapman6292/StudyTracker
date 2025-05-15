namespace CodingTracker.View.Forms.Services.EditSessionPageService
{
    public class RowState
    {
        public int RowIndex { get; }
        public int SessionId { get; }
        public bool MarkedForDeletion { get; set; } = false;

        public RowState(int rowIndex, int sessionId)
        {
            RowIndex = rowIndex;
            SessionId = sessionId;
        }
    }
}
