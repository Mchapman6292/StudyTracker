using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.Entities.CodingSessionEntities;
using System.Threading.Tasks;

namespace CodingTracker.Common.DataInterfaces.Repositories
{
    public interface ICodingSessionRepository
    {
        Task<bool> AddCodingSessionEntityAsync(CodingSessionEntity currentSession);
        Task<List<CodingSessionEntity>> GetSessionsbyIDAsync(List<int> sessionIds);

        Task<int> DeleteSessionsByIdAsync(HashSet<int> sessionIds);

        Task<List<CodingSessionEntity>> GetRecentSessionsAsync(int numberOfSessions);

        Task<List<CodingSessionEntity>> GetSessionBySessionSortCriteriaAsync(int numberOfSessions, SessionSortCriteria? sortBy);

        Task<List<CodingSessionEntity>> GetSessionsForLastDaysAsync(int numberOfDays);

        Task<List<CodingSessionEntity>> GetTodayCodingSessionsAsync();

        Task<List<CodingSessionEntity>> GetAllCodingSessionAsync();

        Task<bool> CheckTodayCodingSessionsAsync();

        Task<List<CodingSessionEntity>> GetAllCodingSessionsByDateOnlyForStartDateAsync(DateOnly date);

        Task<double> GetAverageDurationOfAllSessionsAsync();

        Task<double> GetTodaysTotalDurationAsync();
        Task<double> GetWeekTotalDurationAsync();
        Task<(double todayTotal, double weekTotal, double average)> GetLabelDurationsAsync();
    }
}
