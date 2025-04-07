using CodingTracker.Common.CodingSessions;

namespace CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers
{
    public interface ICodingSessionManager
    {
        void Initialize_CurrentCodingSession(int userId);
        void UpdateISCodingSessionActive(bool active);
        void UpdateIsSessionTimerActive(bool active);
        Task StartCodingSession(string username);
        void StartCodingSessionTimer();
        Task EndCodingSessionAsync();
        void EndCodingSessionTimer();
        void SetCurrentSessionGoalSet(bool goalSet);
        void SetGoalHoursAndGoalMins(int goalMins, bool goalSet);
        string ConvertDurationSecondsToStringHHMM(int durationSeconds);
        void UpdateCodingSessionEndTimes();
        void SetCurrentCodingSession(CodingSession codingSession);
        Task SetUserIdForCurrentSessionAsync(string username, string password);
        int GetCurrentUserId();
        int CalculateDurationSeconds(DateTime? startDate, DateTime? endDate);
        bool ReturnIsCodingSessionActive();
    }
}