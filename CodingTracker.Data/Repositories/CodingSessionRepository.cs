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

            _dbContext.CodingSessions.Add(currentSession);

            bool success = await _dbContext.SaveChangesAsync() > 0;

            if (!success)
            {
                _appLogger.Error($"Failed to add CodingSession with SessionId {currentSession.SessionId}.");
            }

            _appLogger.Debug($"Session added for SessionId: {currentSession.SessionId}, Duration: {currentSession.DurationHHMM}.");
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
                throw new InvalidOperationException($"No sessions found in database for {string.Join(", ", sessionIds)}.");
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

        public async Task<bool> CheckTodayCodingSessions()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _dbContext.CodingSessions
                .AnyAsync(s => s.StartDate == today || s.EndDate == today);
        }
    }
}
