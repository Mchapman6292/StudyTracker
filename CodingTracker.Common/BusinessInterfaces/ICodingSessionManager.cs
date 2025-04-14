using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.Entities.CodingSessionEntities;

namespace CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers
{
    public interface ICodingSessionManager
    {

        void StartCodingSession(DateTime startTime, int? sessionGoalMins, bool goalSet);
        Task EndCodingSessionAsync();
        void Initialize_CurrentCodingSession(int userId);
        void UpdateISCodingSessionActive(bool active);
        void UpdateIsSessionTimerActive(bool active);
        int? ReturnGoalSeconds();
        void SetDurationHHMM(string durationHHMM);
        void SetCurrentSessionGoalReached(bool? goalReached);
        bool? ReturnCurrentSessionGoalReached();
        DateTime? ReturnCurrentSessionStartTime();
        void SetDurationSeconds(int? durationSeconds);
        void SetCurrentSessionGoalSeconds(int? goalMinutes);
        CodingSession ReturnCurrentCodingSession();
        void SetCodingSessionStartTimeAndDate( DateTime startTime);
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