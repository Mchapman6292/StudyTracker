
namespace CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers
{
    public interface ICodingSessionManager
    {
        void InitializeCodingSessionAndSetGoal(int sessionGoalSeconds, bool codingGoalSet);
        void StartCodingSessionAndUpdateActiveBooleans(DateTime startTime);
        void UpdateCodingSessionTimerEnded(TimeSpan? stopWatchTimerDuration);
        Task<bool> NEWUpdateCodingSessionStudyNotesAndSaveCodingSession(string studyProject, string studyNotes);


        void UpdateISCodingSessionActive(bool active);
        void UpdateIsSessionTimerActive(bool active);
        bool ReturnIsSessionTimerActive();
        bool ReturnIsCodingSessionActive();


        void SetCurrentUserIdPlaceholder(int userId);
        void SetStudyProject(string studyProject);
        void SetStudyNotes(string studyNotes);
        void SetCodingSessionStartTimeAndDate(DateTime startTime);
        void SetCodingSessionEndTimeAndDate(DateTime endTime);
        void SetDurationSeconds(int durationSeconds);
        void SetDurationHHMM(string durationHHMM);
        void SetCurrentSessionGoalSet(bool goalSet);
        void SetCurrentSessionGoalReached(bool? goalReached);
        void SetCurrentSessionGoalSeconds(int? goalSeconds);
        void SetGoalHoursAndGoalMins(int goalMins, bool goalSet);


        int ReturnCurrentUserIdPlaceholder();
        DateTime? ReturnCurrentSessionStartTime();
        int? ReturnGoalSeconds();
        bool? ReturnCurrentSessionGoalReached();
    }
}