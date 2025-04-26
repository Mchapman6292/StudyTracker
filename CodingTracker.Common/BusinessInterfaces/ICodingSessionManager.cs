using CodingTracker.Common.CodingSessions;

namespace CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers
{
    public interface ICodingSessionManager
    {

        void StartCodingSession(DateTime startTime, double? sessionGoalSeconds, bool goalSet);
        Task EndCodingSessionAsync();
        Task EndCodingSessionWithTimerFinished();
        void Initialize_CurrentCodingSession(int userId);
        void UpdateISCodingSessionActive(bool active);
        void UpdateIsSessionTimerActive(bool active);
        double? ReturnGoalSeconds();
        void SetDurationHHMM(string durationHHMM);
        void SetCurrentSessionGoalReached(bool? goalReached);
        bool? ReturnCurrentSessionGoalReached();

        void UpdateCodingSessionNoGoalSet();
        void UpdateCodingSessionGoalSet(int sessionGoalSeconds);

        DateTime? ReturnCurrentSessionStartTime();
        void SetDurationSeconds(double? durationSeconds);
        void SetCurrentSessionGoalSeconds(double? goalSeconds);
        CodingSession ReturnCurrentCodingSession();
        void SetCodingSessionStartTimeAndDate(DateTime startTime);
        void SetCodingSessionEndTimeAndDate(DateTime endTime);
        Task OldStartCodingSession(string username);
        void SetCurrentSessionGoalSet(bool goalSet);
        void SetGoalHoursAndGoalMins(int goalMins, bool goalSet);
        void UpdateCodingSessionEndTimes();
        void SetCurrentCodingSession(CodingSession codingSession);
        Task SetUserIdForCurrentSessionAsync(string username, string password);
        int GetCurrentUserId();
        int CalculateDurationSeconds(DateTime? startDate, DateTime? endDate);
        bool ReturnIsCodingSessionActive();
    }
}