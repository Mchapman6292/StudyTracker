using CodingTracker.Common.CodingSessions;

namespace CodingTracker.Common.BusinessInterfaces.ICodingSessionManagers
{
    public interface ICodingSessionManager
    {
        CodingSession CreateNewCodingSession(int userId);
        Task StartCodingSession(string username);
        void StartCodingSessionTimer();
        Task EndCodingSessionAsync();

        void EndCodingSessionTimer();
        string ConvertDurationSecondsToStringHHMM(int durationSeconds);
        void UpdateCodingSessionEndTimes();
        void SetCurrentCodingSession(CodingSession codingSession);
        Task SetUserIdForCurrentSessionAsync(string username, string password);
        int CalculateDurationSeconds(DateTime? startDate, DateTime? endDate);
        bool CheckIfCodingSessionActive();
    }
}