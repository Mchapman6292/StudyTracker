using CodingTracker.Common.CodingSessions;

namespace CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers
{
    public interface ICodingSessionManager
    {
        int ReturnCurrentUserIdPlaceholder();
        void SetCurrentUserIdPlaceholder(int userId);
        void StartCodingSession(DateTime startTime, int? sessionGoalSeconds, bool goalSet);
        Task EndCodingSessionAsync();
        Task EndCodingSessionWithTimerFinished();
        void UpdateISCodingSessionActive(bool active);
        void UpdateIsSessionTimerActive(bool active);
        int? ReturnGoalSeconds();
        void SetDurationHHMM(string durationHHMM);
        void SetCurrentSessionGoalReached(bool? goalReached);
        bool? ReturnCurrentSessionGoalReached();

        void UpdateCodingSessionNoGoalSet();
        void UpdateCodingSessionGoalSet(int sessionGoalSeconds);

        DateTime? ReturnCurrentSessionStartTime();
        bool ReturnIsSessionTimerActive();
        void SetDurationSeconds(int durationSeconds);
        void SetCurrentSessionGoalSeconds(int? goalSeconds);
        void SetCodingSessionStartTimeAndDate(DateTime startTime);
        void SetCodingSessionEndTimeAndDate(DateTime endTime);
        Task OldStartCodingSession(string username);
        void SetCurrentSessionGoalSet(bool goalSet);
        void SetGoalHoursAndGoalMins(int goalMins, bool goalSet);
        void UpdateCodingSessionEndTimes();

        int CalculateDurationSeconds(DateTime? startDate, DateTime? endDate);
        bool ReturnIsCodingSessionActive();

        void SetStudyProject(string studyProject);

        void SetStudyNotes(string studyNotes);

        Task NEWEndCodingSessionAsync(TimeSpan? durationSeconds);

        void NEWUpdateCodingSessionTimerEnded(TimeSpan? stopWatchTimerDuration);

        Task<bool> NEWUpdateCodingSessionStudyNotesAndSaveCodingSession(string studyProject, string studyNotes);







    }
}