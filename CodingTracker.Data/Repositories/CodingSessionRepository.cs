﻿using CodingTracker.Common.CommonEnums;
using CodingTracker.Common.DataInterfaces.DbContextService;
using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.Entities.CodingSessionEntities;
using CodingTracker.Common.LoggingInterfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using System;


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
            _appLogger.Debug($"  StartDateLocal: {currentSession.StartDateUTC}");
            _appLogger.Debug($"  StartTimeLocal: {currentSession.StartTimeUTC}");
            _appLogger.Debug($"  EndDateLocal: {currentSession.EndDateUTC}");
            _appLogger.Debug($"  EndTimeLocal: {currentSession.EndTimeUTC}");
            _appLogger.Debug($"  DurationSeconds: {currentSession.DurationSeconds}");
            _appLogger.Debug($"  DurationHHMM: {currentSession.DurationHHMM}");
            _appLogger.Debug($"  GoalSet: {currentSession.GoalSet}");
            _appLogger.Debug($"  GoalSeconds: {currentSession.GoalSeconds}");
            _appLogger.Debug($"  GoalReached: {currentSession.GoalReached}");
            _appLogger.Debug($"  StudyProject: {currentSession.StudyProject}");
            _appLogger.Debug($"  StudyNotes: {currentSession.StudyNotes}"); 

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

            if (!sessionsToDelete.Any())
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
                        .OrderByDescending(s => s.StartDateUTC)
                        .Take(numberOfSessions)
                        .ToListAsync();
            }

        public async Task<List<CodingSessionEntity>> GetSessionBySessionSortCriteriaAsync(int numberOfSessions, SessionSortCriteria? sortBy)
        {
            IQueryable<CodingSessionEntity> query = _dbContext.CodingSessions;

            query = sortBy switch
            {
                SessionSortCriteria.StudyProject => query.OrderByDescending(s => s.StudyProject),
                SessionSortCriteria.Duration => query.OrderByDescending(s => s.DurationHHMM),
                SessionSortCriteria.StartDate => query.OrderByDescending(s => s.StartDateUTC),
                SessionSortCriteria.StartTime => query.OrderByDescending(s => s.StartDateUTC),
                SessionSortCriteria.EndDate => query.OrderByDescending(s => s.EndDateUTC),
                SessionSortCriteria.EndTime => query.OrderByDescending(s => s.EndDateUTC),
                SessionSortCriteria.None => query.OrderByDescending(s => s.StartDateUTC),
                _ => query.OrderByDescending(s => s.StartDateUTC)  // Fallback
            };

            return await query
                .Take(numberOfSessions)
                .ToListAsync();
        }

        public async Task<List<CodingSessionEntity>> GetSessionsForLastDaysAsync(int numberOfDays)
        {
            DateOnly targetDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-numberOfDays));
            return await _dbContext.CodingSessions
                .Where(s => s.StartDateUTC >= targetDate || s.EndDateUTC >= targetDate)
                .OrderBy(s => s.StartDateUTC)
                .ToListAsync();
        }


        public async Task<List<CodingSessionEntity>> GetTodayCodingSessionsAsync()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.UtcNow);
            return await _dbContext.CodingSessions
                .Where(s => s.StartDateUTC == today || s.EndDateUTC == today)
                .OrderBy(s => s.StartTimeUTC)
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
                .AnyAsync(s => s.StartDateUTC == today || s.EndDateUTC == today);
        }


        public async Task<List<CodingSessionEntity>> GetAllCodingSessionsByDateOnlyForStartDateAsync(DateOnly date)
        {
            return await _dbContext.CodingSessions
                .Where(s => s.StartDateUTC == date)
                .ToListAsync();
        }

        public async Task<double> GetAverageDurationOfAllSessionsAsync()
        {
            if (!await _dbContext.CodingSessions.AnyAsync())
            {
                return 0.0;
            }

            return await _dbContext.CodingSessions
                .Select(s => s.DurationSeconds)
                .AverageAsync();
        }

        // Issue is this is being called several times, need to change this to a transaction method

        public async Task<double> GetTodaysTotalDurationAsync()
        {
            if (!await _dbContext.CodingSessions.AnyAsync())
            {
                return 0.0;
            }

            DateOnly todayUTC = DateOnly.FromDateTime(DateTime.UtcNow);

            return await _dbContext.CodingSessions
                .Where(s => s.StartDateUTC == todayUTC)
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
                .Where(s => s.StartDateUTC >= todayDate)
                .SumAsync(s => s.DurationSeconds);
        }


        // This needs to be made under one db call as calling all the individual methods together causes a concurrency exception. 
        public async Task<(double todayTotal, double weekTotal, double average)> GetLabelDurationsAsync()
        {
            var todayUTC = DateOnly.FromDateTime(DateTime.UtcNow);
            var weekStart = todayUTC.AddDays(-6);

            var sessions = await _dbContext.CodingSessions
                .AsNoTracking()
                .Select(s => new
                {
                    s.StartDateUTC,
                    s.DurationSeconds
                })
                .ToListAsync();

            var todayTotal = sessions
                .Where(s => s.StartDateUTC >= todayUTC)
                .Sum(s => (double?)s.DurationSeconds) ?? 0.0;

            var weekTotal = sessions
                .Where(s => s.StartDateUTC >= weekStart)
                .Sum(s => (double?)s.DurationSeconds) ?? 0.0;

            var average = sessions
                .Sum(s => (double?)s.DurationSeconds) ?? 0.0;

            return (todayTotal, weekTotal, average);
        }

        //Used for DonutChart on main page, add default values of zero so the pie chart shows a small section to indicate 0 counts of that rating.
        public async Task<Dictionary<int , int>> GetStarRatingAndCount()
        {
            // Remove OrderBy from the database query - we'll handle ordering in memory
            Dictionary<int, int> starRatings = await _dbContext.CodingSessions
                .GroupBy(s => s.SessionStarRating)
                .ToDictionaryAsync(g => g.Key, g => g.Count());

            var starRatingsWithZero = new Dictionary<int, int>();
            for (int i = 1; i <= 5; i++)
            {
                starRatingsWithZero[i] = starRatings.GetValueOrDefault(i, 0);
            }
            return starRatingsWithZero;
        }

        // Returns list of session durations beginning with 28 days previous date, adds 0 value for durationSeconds if no entry on that date. 
        public async Task<List<int>> GetLast28DayDurationSecondsWithDefaultZeroValues()
        {
            DateOnly todayDate = DateOnly.FromDateTime(DateTime.UtcNow);
            DateOnly startDate = todayDate.AddDays(-27);

            var sessionData = await _dbContext.CodingSessions
                .Where(s => s.StartDateUTC >= startDate && s.StartDateUTC <= todayDate)
                .GroupBy(s => s.StartDateUTC)
                .Select(group => new { Date = group.Key, TotalDuration = group.Sum(s => s.DurationSeconds) })
                .ToListAsync();

            var sessionDict = sessionData.ToDictionary(x => x.Date, x => x.TotalDuration);

            var result = new List<int>();
            for (int i = 27; i >= 0; i--)
            {
                var currentDate = todayDate.AddDays(-i);
                result.Add(sessionDict.TryGetValue(currentDate, out var duration) ? duration : 0);
            }

            return result;
        }

        // returns CodingSessionEntity with date onyl if 
        public async Task<Dictionary<DateOnly, List<CodingSessionEntity>>> GetSessionsGroupedByDateLastSevenDays()
        {
            DateTime yesterday = DateTime.UtcNow.AddDays(-1);
            DateTime startDate = yesterday.AddDays(-7);

            var actualSessions = await _dbContext.CodingSessions
                              .Where(s => s.StartTimeUTC > startDate && s.StartTimeUTC < yesterday)
                              .OrderBy(s => s.StartDateUTC)
                              .ToListAsync();

            return actualSessions
                .GroupBy(s => s.StartDateUTC)
                .ToDictionary(g => g.Key, g => g.ToList());
        }


    }
}
