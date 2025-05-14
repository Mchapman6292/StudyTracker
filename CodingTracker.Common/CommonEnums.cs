// Ignore Spelling: Enums

namespace CodingTracker.Common.CommonEnums
{

    // Used by GetSessionBySessionSortCriteriaAsync in CodingSessionRepository to retrieve sessions sorted by enum.
    public enum SessionSortCriteria
    {
        None,
        SessionId,
        Duration,
        StartDate,
        StartTime,
        EndDate,
        EndTime,
        StudyProject
    }

    // Main page label enums. 
    public enum MainPageLabels
    {
        TodayTotalLabel,
        WeekTotalLabel,
        AverageSessionLabel
    }


}
