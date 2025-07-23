using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.LoggingHelpers
{
    public interface IAnimatedLogHelper
    {
        void LogColumn(AnimatedTimerColumn column, TimeSpan elapsed, float? initialYLocation, float? easedProgress, float? yTranslation);
        string FormatElapsedTimeSpan(TimeSpan elapsed);
    }

    public class AnimatedLogHelper : IAnimatedLogHelper
    {
        private readonly IApplicationLogger _appLogger;


        public AnimatedLogHelper(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }






        public void LogColumn(AnimatedTimerColumn column, TimeSpan elapsed, float? initialYLocation, float? easedProgress, float? yTranslation)
        {
            string logMessage = $"\n \n"
                                + $"\n-----LOGGING COLUMN {column.ColumnType} AT ELAPSED {FormatElapsedTimeSpan(elapsed)}-----"
                                + $"\n-----Current Value : {column.CurrentValue}, Target Value : {column.TargetSegmentValue}.-----"
                                + $"\n-----IsAnimating: {column.IsAnimating}.-----"
                                + $"\n-----BaseAnimationProgress: {column.BaseAnimationProgress}, ColumnScrollProgress: {column.ColumnScrollProgress}, CircleAnimationProgress: {column.CircleAnimationProgress}.-----"
                                + $"\n-----Max Value: {column.MaxValue}, TotalSegmentCount: {column.TotalSegmentCount}, TimerSegments.Count: {column.TimerSegments.Count()}.-----"
                                + $"\n------PassedFirstTransition: {column.PassedFirstTransition.ToString()}."
                                + $"\n------- IsRestarting: {column.IsRestarting}. RestartYLocation: {column.YLocationAtRestart}. ";


            if (initialYLocation != null && easedProgress != null && yTranslation != null)
            {
                logMessage +=
                $"\n---- OFFSET CALCULATION FOR COLUMN: {column.ColumnType}.-----"
                + $"\n---- InitialYLocation: {initialYLocation}, Easing Value: {easedProgress}, YTranslation: {yTranslation}.-----\n\n";
            }

            _appLogger.Info(logMessage);
        }

        public string FormatElapsedTimeSpan(TimeSpan elapsed)
        {
            return elapsed.ToString(@"mm\:ss\.fff");
        }


    }
}
