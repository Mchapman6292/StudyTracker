using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.ICodingSessionRepositories;
using CodingTracker.Common.DataInterfaces.ICodingTrackerDbContexts;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.IApplicationLoggers;
using Microsoft.EntityFrameworkCore;
using System.Text;


namespace CodingTracker.Data.Repositories.CodingSessionRepositories
{
    public class CodingSessionRepository : ICodingSessionRepository
    {

        private readonly IApplicationLogger _appLogger;
        private readonly ICodingTrackerDbContext _dbContext;


        public CodingSessionRepository(IApplicationLogger appLogger, ICodingTrackerDbContext context) 
        {
            _appLogger = appLogger;
            _dbContext = context;
        }

        public async Task<bool> AddCodingSessionEntityAsync(CodingSessionEntity currentSession)
        {
            if (await _dbContext.CodingSessions.AnyAsync(s => s.SessionId == currentSession.SessionId))
            {
                throw new InvalidOperationException($"A CodingSession with SessionId {currentSession.SessionId} already exists.");
            }

            _appLogger.Debug($"Adding new coding session with details:");
            _appLogger.Debug($"  SessionId: {currentSession.SessionId}");
            _appLogger.Debug($"  UserId: {currentSession.UserId}");
            _appLogger.Debug($"  StartDate: {currentSession.StartDate}");
            _appLogger.Debug($"  StartTime: {currentSession.StartTime}");
            _appLogger.Debug($"  EndDate: {currentSession.EndDate}");
            _appLogger.Debug($"  EndTime: {currentSession.EndTime}");
            _appLogger.Debug($"  DurationSeconds: {currentSession.DurationSeconds}");
            _appLogger.Debug($"  DurationHHMM: {currentSession.DurationHHMM}");
            _appLogger.Debug($"  GoalSet: {currentSession.GoalSet}");
            _appLogger.Debug($"  GoalSeconds: {currentSession.GoalSeconds}");
            _appLogger.Debug($"  GoalReached: {currentSession.GoalReached}");

            _dbContext.CodingSessions.Add(currentSession);
            bool success = await _dbContext.SaveChangesAsync() > 0;

            if (!success)
            {
                _appLogger.Error($"Failed to add CodingSession with SessionId {currentSession.SessionId}.");
            }
            else
            {
                _appLogger.Debug($"Session successfully added for SessionId: {currentSession.SessionId}");
            }

            return success;
        }



        public async Task<List<CodingSessionEntity>> GetSessionsbyIDAsync(List<int> sessionIds)
        {
            // Creating a list of Coding sessions would load all sessions into memory.
            // Using .Where = SELECT * FROM CodingSessions WHERE SessionId IN (1,2,3)

            return await _dbContext.CodingSessions
                    .Where(s => sessionIds.Contains(s.SessionId))
                    .ToListAsync();
        }


        public async Task<int> DeleteSessionsByIdAsync(HashSet<int> sessionIds)
        {
            _appLogger.Debug(string.Join(", ", sessionIds));

            var sessionsToDelete = await _dbContext.CodingSessions
                .Where(s => sessionIds.Contains(s.SessionId))
                .Select(s => s.SessionId)
                .ToListAsync();

            if(!sessionsToDelete.Any())
            {
                _appLogger.Error($"No session ids for deletion in {nameof(DeleteSessionsByIdAsync)}");
                return 0;
            }

            int deletedSessions = await _dbContext.CodingSessions
                .Where(s => sessionIds.Contains(s.SessionId))
                .ExecuteDeleteAsync();

            _appLogger.Info($"{deletedSessions} sessions deleted. Session IDs: {string.Join(", ", sessionsToDelete)}");

            return deletedSessions;
        }


        public async Task<List<CodingSessionEntity>> GetRecentSessionsAsync(int numberOfSessions)
        {
            return await _dbContext.CodingSessions
                    .OrderByDescending(s => s.StartDate)
                    .Take(numberOfSessions)
                    .ToListAsync();
        }

        public async Task<List<CodingSessionEntity>> GetSessionBySessionSortCriteriaAsync(int numberOfSessions, SessionSortCriteria? sortBy)
        {
            IQueryable<CodingSessionEntity> query = _dbContext.CodingSessions;

            query = sortBy switch
            {
                SessionSortCriteria.SessionId => query.OrderByDescending(s => s.SessionId),
                SessionSortCriteria.Duration => query.OrderByDescending(s => s.DurationHHMM),
                SessionSortCriteria.StartDate => query.OrderByDescending(s => s.StartDate),
                SessionSortCriteria.StartTime => query.OrderByDescending(s => s.StartDate),
                SessionSortCriteria.EndDate => query.OrderByDescending(s => s.EndDate),
                SessionSortCriteria.EndTime => query.OrderByDescending(s => s.EndDate),
                SessionSortCriteria.None => query.OrderByDescending(s => s.StartDate), 
                _ => query.OrderByDescending(s => s.StartDate)  // Fallback
            };

            return await query
                .Take(numberOfSessions)
                .ToListAsync();
        }

        public async Task<List<CodingSessionEntity>> GetSessionsForLastDaysAsync(int numberOfDays)
        {
            DateOnly targetDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-numberOfDays));
            return await _dbContext.CodingSessions
                .Where(s => s.StartDate >= targetDate || s.EndDate >= targetDate)
                .OrderBy(s => s.StartDate)
                .ToListAsync();
        }


        public async Task<List<CodingSessionEntity>> GetTodayCodingSessionsAsync()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _dbContext.CodingSessions
                .Where(s => s.StartDate == today || s.EndDate == today)
                .OrderBy(s => s.StartTime)
                .ToListAsync();
        }

        public async Task<List<CodingSessionEntity>> GetAllCodingSessionAsync()
        {
            return await _dbContext.CodingSessions
                .ToListAsync();
        }

        public async Task<bool> CheckTodayCodingSessionsAsync()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _dbContext.CodingSessions
                .AnyAsync(s => s.StartDate == today || s.EndDate == today);
        }


        public async Task<List<CodingSessionEntity>> GetAllCodingSessionsByDateOnlyForStartDateAsync(DateOnly date)
        {
            return await _dbContext.CodingSessions
                .Where(s => s.StartDate == date)
                .ToListAsync();
        }

        public async Task<double> GetAverageDurationOfAllSessionsAsync()
        {
            if(!await _dbContext.CodingSessions.AnyAsync())
            {
                return 0.0;
            }

            return await _dbContext.CodingSessions
                .Select(s => s.DurationSeconds)
                .AverageAsync();     
        }

        public async Task<double> GetTodaysTotalDurationAsync()
        {
            if (!await _dbContext.CodingSessions.AnyAsync())
            {
                return 0.0;
            }

            DateOnly todayUTC = DateOnly.FromDateTime(DateTime.UtcNow);

            return await _dbContext.CodingSessions
                .Where(s => s.StartDate == todayUTC)
                .SumAsync(s => s.DurationSeconds);
        }

        public async Task<double> GetWeekTotalDurationAsync()
        {
            if (!await _dbContext.CodingSessions.AnyAsync())
            {
                return 0.0;
            }

            DateOnly startDate = DateOnly.FromDateTime(DateTime.UtcNow).AddDays(-7);
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.UtcNow);

            return await _dbContext.CodingSessions
                .Where(s => s.StartDate >= todayDate)
                .SumAsync(s => s.DurationSeconds);
        }

    }
}
