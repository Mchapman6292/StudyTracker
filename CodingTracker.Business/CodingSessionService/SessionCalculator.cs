using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.IApplicationLoggers;


namespace CodingTracker.Business.CodingSessionService.SessionCalculators
{
    public interface ISessionCalculator
    {
        Task<double> CalculateLastSevenDaysAvgInSeconds();
        Task<double> GetTodayTotalSession();
        Task<double> CalculateTotalAvg();
        double CalculateDurationSeconds();
    }


    public class SessionCalculator : ISessionCalculator
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ICodingSessionRepository _codingSessionRepository;

        public SessionCalculator(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
        }

        public async Task<double> CalculateLastSevenDaysAvgInSeconds()
        {
            int previousDays = 7;

            List<CodingSessionEntity> sessions = await _codingSessionRepository.GetSessionsForLastDaysAsync(previousDays);

            if (!sessions.Any())
            {
                return 0;
            }

            var sessionsByDate = sessions
                .GroupBy(s => s.StartDate)
                .Select(g => new { Date = g.Key, TotalSeconds = g.Sum(s => s.DurationSeconds) })
                .ToList();

            double totalSeconds = sessionsByDate.Sum(s => s.TotalSeconds);
            int daysWithSessions = sessionsByDate.Count;

            double averageSeconds = totalSeconds / 7; // Still divide by 7 for a true 7-day average


            return averageSeconds;
        }




        public async Task<double> GetTodayTotalSession()
        {
            var sessions = await _codingSessionRepository.GetTodayCodingSessionsAsync();

            if (!sessions.Any())
            {
                return 0;
            }

            double totalDurationSeconds = sessions.Sum(s => s.DurationSeconds);

            return totalDurationSeconds;
        }

        public async Task<double> CalculateTotalAvg()
        {
            var allSessions = await _codingSessionRepository.GetAllCodingSessionAsync();

            if (!allSessions.Any())
            {
                return 0;
            }

            double totalSeconds = allSessions.Sum(s => s.DurationSeconds);
            int sessionCount = allSessions.Count();
            double averageSeconds = totalSeconds / sessionCount;

            return averageSeconds;
        }




        public double CalculateDurationSeconds()
        {
            throw new NotImplementedException();
        }
    }
}
