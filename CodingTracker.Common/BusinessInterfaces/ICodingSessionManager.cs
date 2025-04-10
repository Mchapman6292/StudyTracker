using CodingTracker.Common.CodingSessions;
using CodingTracker.Common.Entities.CodingSessionEntities;

namespace CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers
{
    public interface ICodingSessionManager
    {
        void StartCodingSessionWithGoal(DateTime startTime, int sessionGoalMins);
        Task EndCodingSessionWithGoalAsync(DateTime endTime);
        void Initialize_CurrentCodingSession(int userId);
        void UpdateISCodingSessionActive(bool active);
        void UpdateIsSessionTimerActive(bool active);
        CodingSession ReturnCurrentCodingSession();
        void SetCodingSessionStartTimeAndDate( DateTime startTime);
        void SetCodingSessionEndTimeAndDate(DateTime endTime);
        Task StartCodingSession(string username);
        void StartCodingSessionTimer();
        Task EndCodingSessionAsync();
        void EndCodingSessionTimer();
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