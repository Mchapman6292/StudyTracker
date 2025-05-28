using CodingTracker.Common.DataInterfaces.Repositories;
using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormService.ColourServices;
using System.Diagnostics;
using System.Drawing;
using System.Numerics;


namespace CodingTracker.Business.MainPageService.PanelColourAssigners
{
    public enum SessionColor
    {
        Slate,
        Blue,
        Rose,
        Amber,
        Coral,
        Emerald
    }






    public interface IPanelColourAssigner
    {
        Task<List<(Color StartColor, Color EndColor)>> AssignGradientColorsToSessionsInLast28Days();
        (Color StartColor, Color EndColor) GetSessionGradientColours(SessionColor color);
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
                    (Color StartColor, Color EndColor) gradientColors = GetUpdatedSessionGradientColors(totalDurationSeconds);
                    sessionGradients.Add(gradientColors);

                }
                return sessionGradients;
            }
        }

        public SessionColor DetermineSessionColor(double? sessionDurationSeconds)
        {
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


        public (Color gradientOneColor, Color gradientTwoColor) GetUpdatedSessionGradientColors(double? durationSeconds)
        {
            if (!durationSeconds.HasValue || durationSeconds <= 0)
            {
                return (ColorService.SessionSlateStart, ColorService.SessionSlateEnd);
            }
            else if (durationSeconds > 0 && durationSeconds <= 1800)
            {
                return (ColorService.SessionBlueStart, ColorService.SessionBlueEnd);
            }
            else if (durationSeconds > 1800 && durationSeconds <= 3600)
            {
                return (ColorService.SessionRoseStart, ColorService.SessionRoseEnd);
            }
            else if (durationSeconds > 3600 && durationSeconds <= 5400)
            {
                return (ColorService.SessionAmberStart, ColorService.SessionAmberEnd);
            }
            else if (durationSeconds > 5400 && durationSeconds <= 7200)
            {
                return (ColorService.SessionCoralStart, ColorService.SessionCoralEnd);
            }
            else // > 7200
            {
                return (ColorService.SessionEmeraldStart, ColorService.SessionEmeraldEnd);
            }
        }



        public (Color StartColor, Color EndColor) GetSessionGradientColours(SessionColor color)
        {
            (Color StartColor, Color EndColor) result;
            switch (color)
            {
                case SessionColor.Blue:
                    result = (Color.FromArgb(70, 71, 117), Color.FromArgb(45, 46, 80));
                    break;
                case SessionColor.Coral:
                    result = (Color.FromArgb(255, 81, 195), Color.FromArgb(220, 60, 170));
                    break;
                case SessionColor.Rose:
                    result = (Color.FromArgb(255, 100, 180), Color.FromArgb(255, 81, 195));
                    break;
                case SessionColor.Amber:
                    result = (Color.FromArgb(168, 228, 255), Color.FromArgb(130, 200, 255));
                    break;
                case SessionColor.Emerald:
                    result = (Color.FromArgb(100, 220, 220), Color.FromArgb(168, 228, 255));
                    break;
                case SessionColor.Slate:
                    result = (Color.FromArgb(32, 33, 36), Color.FromArgb(25, 24, 40));
                    break;
                default:
                    result = (Color.FromArgb(70, 71, 117), Color.FromArgb(45, 46, 80));
                    break;
            }
            return result;
        }







        public List<DateTime> GetDatesPrevious28days()
        {
            List<DateTime> dates = new List<DateTime>();
            DateTime today = DateTime.Today;

            for (int i = 1; i <= 29; i++)
            {
                dates.Add(today.AddDays(-i));
            }

            return dates;
            }
        }



 

   }