using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.LoggingInterfaces;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;

namespace CodingTracker.Business.MainPageService.PanelColourAssigners
{
    public enum SessionColor
    {
        Blue,        // For 0 minutes
        Coral,       // For less than 60 minutes
        Rose,        // For 1 to less than 2 hours
        Amber,       // For 2 to less than 3 hours
        Emerald,     // For 3 hours and more
        Slate        // For errors/null   
    }

    public interface IPanelColourAssigner
    {
        Task<List<(Color StartColor, Color EndColor)>> AssignGradientColorsToSessionsInLast28Days();
        (Color StartColor, Color EndColor) GetSessionGradientColors(SessionColor color);
        SessionColor DetermineSessionColor(double? sessionDurationSeconds);
        List<DateTime> GetDatesPrevious28days();
    }

    public class PanelColourAssigner : IPanelColourAssigner
    {
        private readonly IApplicationLogger _appLogger;
        private readonly List<(DateTime Day, double TotalDurationMinutes)> _dailyDurations;
        private readonly List<SessionColor> _sessionColors;
        private readonly ICodingSessionRepository _codingSessionRepository;

        public PanelColourAssigner(IApplicationLogger appLogger, ICodingSessionRepository codingSessionRepository)
        {
            _appLogger = appLogger;
            _codingSessionRepository = codingSessionRepository;
        }

        public async Task<List<(Color StartColor, Color EndColor)>> AssignGradientColorsToSessionsInLast28Days()
        {
            {
                _appLogger.Info($"Starting {nameof(AssignGradientColorsToSessionsInLast28Days)}");

                var recentSessions = await _codingSessionRepository.GetRecentSessionsAsync(28);

                _appLogger.Debug($"Sessions returned by ReadFromCodingSessionsTable for {nameof(AssignGradientColorsToSessionsInLast28Days)}: {recentSessions}.");

                List<(Color StartColor, Color EndColor)> sessionGradients = new List<(Color StartColor, Color EndColor)>();
                foreach (var session in recentSessions)
                {
                    double totalDurationSeconds = session.DurationSeconds;
                    DateOnly sessionDate = session.StartDateUTC;

                    SessionColor colorEnum = DetermineSessionColor(totalDurationSeconds);
                    (Color StartColor, Color EndColor) gradientColors = GetSessionGradientColors(colorEnum);
                    sessionGradients.Add(gradientColors);

                }
                return sessionGradients;
            }
        }

        public SessionColor DetermineSessionColor(double? sessionDurationSeconds)
        {
            using (new Activity(nameof(DetermineSessionColor))) { }
            if (!sessionDurationSeconds.HasValue || sessionDurationSeconds <= 0)
            {
                return SessionColor.Emerald;
            }
            else if (sessionDurationSeconds < 3600)
            {
                return SessionColor.Coral;
            }
            else if (sessionDurationSeconds < 7200)
            {
                return SessionColor.Rose;
            }
            else if (sessionDurationSeconds < 10800)
            {
                return SessionColor.Amber;
            }
            else
            {
                return SessionColor.Emerald;
            }
        }

        public (Color StartColor, Color EndColor) GetSessionGradientColors(SessionColor color)
        {
            var activity = new Activity(nameof(GetSessionGradientColors)).Start();
            var stopwatch = Stopwatch.StartNew();
            _appLogger.Debug($"Starting {nameof(GetSessionGradientColors)} ceID: {activity.TraceId}");
            try
            {
                (Color StartColor, Color EndColor) result;
                switch (color)
                {
                    case SessionColor.Blue:
                        result = (Color.FromArgb(86, 180, 211), Color.FromArgb(64, 116, 199));
                        break;
                    case SessionColor.Coral:
                        result = (Color.FromArgb(255, 138, 138), Color.FromArgb(235, 107, 107));
                        break;
                    case SessionColor.Rose:
                        result = (Color.FromArgb(247, 99, 119), Color.FromArgb(226, 54, 78));
                        break;
                    case SessionColor.Amber:
                        result = (Color.FromArgb(255, 209, 138), Color.FromArgb(255, 190, 92));
                        break;
                    case SessionColor.Emerald:
                        result = (Color.FromArgb(72, 219, 143), Color.FromArgb(46, 204, 113));
                        break;
                    case SessionColor.Slate:
                        result = (Color.FromArgb(65, 65, 85), Color.FromArgb(45, 45, 65));
                        break;
                    default:
                        result = (Color.FromArgb(86, 180, 211), Color.FromArgb(64, 116, 199));
                        break;
                }
                stopwatch.Stop();
                return result;
            }
            finally
            {
                stopwatch.Stop();
                activity.Stop();
            }
        }

        public List<DateTime> GetDatesPrevious28days() // Potential mismatch with sql lite db dates?
        {
            using (var activity = new Activity(nameof(GetDatesPrevious28days)).Start())
            {
                var stopwatch = Stopwatch.StartNew();
                _appLogger.Debug($"Getting dates for the previous 28 days. TraceID: {activity.TraceId}");

                List<DateTime> dates = new List<DateTime>();
                DateTime today = DateTime.Today;

                for (int i = 1; i <= 29; i++)
                {
                    dates.Add(today.AddDays(-i));
                }

                stopwatch.Stop();
                _appLogger.Info($"Retrieved dates for the previous 28 days. Count: {dates.Count}, Execution Time: {stopwatch.ElapsedMilliseconds}ms, Trace ID: {activity.TraceId}");

                return dates;
            }
        }
    }
}