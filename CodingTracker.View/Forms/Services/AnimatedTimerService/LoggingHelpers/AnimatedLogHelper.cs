using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers
{
    public interface IAnimatedLogHelper
    {
        void LogColumn(AnimatedTimerColumn column, TimeSpan elapsed, ColumnAnimationType columnAnimationType, float? initialYLocation, float? easedProgress, string? message);
        string FormatElapsedTimeSpan(TimeSpan elapsed);
        void LogColumnDuringRestart(AnimatedTimerColumn column, TimeSpan restartTimerElapsed, float restartAnimationProgress, float currentYPosition);
    }

    public class AnimatedLogHelper : IAnimatedLogHelper
    {
        private readonly IApplicationLogger _appLogger;


        public AnimatedLogHelper(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }






        public void LogColumn(AnimatedTimerColumn column, TimeSpan elapsed, ColumnAnimationType columnAnimationType ,float? initialYLocation, float? easedProgress, string? message)
        {
            string logMessage = $"\n \n"
                                + $"\n-----LOGGING COLUMN {column.ColumnType} AT ELAPSED {FormatElapsedTimeSpan(elapsed)}, Animation type: {columnAnimationType} -----"
                                + $"\n-----Current Value : {column.ActiveDigit}, Target Value : {column.TargetDigit}.-----"
                                + $"\n-----IsStandardAnimationOccuring: {column.IsStandardAnimationOccuring}.-----"
                                + $"\n-----BaseAnimationProgress: {column.BaseAnimationProgress}, ColumnScrollProgress: {column.ColumnScrollProgress}, CircleAnimationProgress: {column.CircleAnimationProgress}.-----"
                                + $"\n-----Max Value: {column.MaxValue}, TotalSegmentCount: {column.TotalSegmentCount}, TimerSegments.Count: {column.TimerSegments.Count()}.-----"
                                + $"\n------PassedFirstTransition: {column.PassedFirstTransition.ToString()}."
                                + $"\n------- IsRestarting: {column.IsRestarting}. RestartYLocation: {column.YLocationAtRestart}. "
                                 +$"\n-------YTranslation: {column.YTranslation}.-----";

            if (message != null)
            {
                logMessage += $"----{message}----";
            }

            if (initialYLocation != null && easedProgress != null )
            {
                logMessage +=
                $"\n---- OFFSET CALCULATION FOR COLUMN: {column.ColumnType}.-----"
                + $"\n---- InitialYLocation: {initialYLocation}, Easing Value: {easedProgress}.";
            }

            _appLogger.Info(logMessage);
        }

        public void LogColumnDuringRestart(AnimatedTimerColumn column, TimeSpan restartTimerElapsed, float restartAnimationProgress, float currentYPosition)
        {

            AnimatedTimerSegment targetSegment = column.TimerSegments.FirstOrDefault(s => s.Value == 0);

            SKPoint segmentLocation = targetSegment.Location;


            string logMessage = $"[RESTART] {column.ColumnType} @ {FormatElapsedTimeSpan(restartTimerElapsed)} | " +
                                  $"Progress: {restartAnimationProgress:F3} | " +
                                  $"YTrans: {column.YTranslation:F2} (from {column.YLocationAtRestart:F2}) | " +
                                  $"Pos: {currentYPosition:F2} | " +
                                  $"Val: {column.ActiveDigit}→{column.TargetDigit}"
                                + $"TartgetSegment Location: Y: {segmentLocation.Y}, X: {segmentLocation.X}.";

            _appLogger.Debug(logMessage);
        }


        public string FormatElapsedTimeSpan(TimeSpan elapsed)
        {
            return elapsed.ToString(@"mm\:ss\.fff");
        }


        

    }
}
