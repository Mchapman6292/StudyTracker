using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using System.Timers;
using Windows.Networking.PushNotifications;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {

        void UpdateAnimationState(AnimatedTimerColumn column, TimeSpan elapsed);
        int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column);
        float CalculateScrollOffset(AnimatedTimerColumn column, float animationProgress);
        bool IsWithinAnimationInterval(AnimatedTimerColumn column, TimeSpan elapsed);
        float CalculateCircleAnimationProgress(TimeSpan elapsed, float animationProgress);
        float GetColumnAnimationProgress(TimeSpan elapsed);
        float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed);
        void UpdateColumnCurrentValue(AnimatedTimerColumn column, int newValue);
        void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet);
        void UpdateAnimationPogress(AnimatedTimerColumn column, float animationProgress);
        void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress);

        float TESTGetColumnAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);
    }

    public class AnimatedColumnStateManager : IAnimatedColumnStateManager
    {
        private readonly IApplicationLogger _appLogger;





        public AnimatedColumnStateManager(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }



        public void UpdateColumnTimesOnTick()
        {

        }





        public void UpdateAnimationState(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            // Handle SecondsSingleDigitsColumn which is always highlighting.

            _appLogger.Debug($"=== UpdateAnimationState START for {column.ColumnType} ===");
            _appLogger.Debug($"Elapsed: {elapsed.TotalSeconds:F3}s, NextTransition: {column.NextTransitionTime.TotalSeconds:F3}s");
            _appLogger.Debug($"IsAnimating: {column.IsAnimating}, CurrentValue: {column.CurrentValue}, PreviousValue: {column.PreviousValue}");


            if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                column.IsAnimating = true;
                int totalSeconds = (int)elapsed.TotalSeconds;
                column.PreviousValue = column.CurrentValue;
                column.CurrentValue = CalculateColumnValue(elapsed, column);
                return;
            }

            if (!column.IsAnimating && elapsed >= column.NextTransitionTime)
            {
                _appLogger.Debug($"Starting animation - Elapsed ({elapsed.TotalSeconds:F3}s) >= NextTransition ({column.NextTransitionTime.TotalSeconds:F3}s)");

                column.IsAnimating = true;
                column.LastAnimationStartTime = elapsed;
                column.PreviousValue = column.CurrentValue;
                column.CurrentValue = CalculateColumnValue(elapsed, column);
                UpdateNextTransitionTime(column);

                _appLogger.Debug($"Animation started at {column.LastAnimationStartTime.TotalSeconds:F3}s");
                _appLogger.Debug($"Value changing from {column.PreviousValue} to {column.CurrentValue}");

                TimeSpan oldNextTransition = column.NextTransitionTime;
                UpdateNextTransitionTime(column);

                _appLogger.Debug($"NextTransitionTime updated from {oldNextTransition.TotalSeconds:F3}s to {column.NextTransitionTime.TotalSeconds:F3}s");

            }

            // If the column is animating check if the difference between elapsed and lastanimationStartTime is greater than the animated duration.
            if (column.IsAnimating)
            {
                if ((elapsed - column.LastAnimationStartTime) >= AnimatedColumnSettings.AnimationDuration)
                {
                    column.IsAnimating = false;
                }
            }
            _appLogger.Debug($"=== UpdateAnimationState END - IsAnimating: {column.IsAnimating} ===\n");
        }





        public bool IsWithinAnimationInterval(AnimatedTimerColumn column ,TimeSpan elapsed)
        {
            return elapsed >= column.NextTransitionTime;
        }

        public int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            int totalSeconds = (int)elapsed.TotalSeconds;
            int minutes = totalSeconds / 60;
            int hours = totalSeconds / 3600;

            int segmentCount = column.TotalSegmentCount;

            switch (column.ColumnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    return totalSeconds % segmentCount;
                case ColumnUnitType.SecondsLeadingDigit:
                    var result = (totalSeconds / 10) % segmentCount;
                    _appLogger.Debug($"ColumnValue calculated: {result}.");
                    return result;
                case ColumnUnitType.MinutesSingleDigits:
                    return minutes % 10;
                case ColumnUnitType.MinutesLeadingDigits:
                    return (minutes / 10) % segmentCount;
                case ColumnUnitType.HoursSinglesDigits:
                    return hours % 10;
                case ColumnUnitType.HoursLeadingDigits:
                    return (hours / 10) % segmentCount;
                default:
                    return 0;
            }
        }

        private void UpdateNextTransitionTime(AnimatedTimerColumn column)
        {
            var previous = column.NextTransitionTime;
            var next = column.NextTransitionTime += column.AnimationInterval;

            column.NextTransitionTime += column.AnimationInterval;

            _appLogger.Debug($"old transition time: {previous}, next/new time:{next}.");
        }






        // Value to determine the progress between one Segment and the next. 
        public float CalculateScrollOffset(AnimatedTimerColumn column, float animationProgress)
        {
            float baseOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            float easedProgress = CalculateEasingValue(animationProgress);
            return baseOffset + (easedProgress * AnimatedColumnSettings.SegmentHeight);
        }


        private float CalculateEasingValue(float t)
        {
            return t < 0.5f
                ? 4f * t * t * t
                : 1f - MathF.Pow(-2f * t + 2f, 3f) / 2f;

        }


        public float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float circleAnimationProgress = column.CircleAnimationProgress;
            return Single.Lerp(AnimatedColumnSettings.MaxRadius, AnimatedColumnSettings.MinRadius, circleAnimationProgress);
        }




        public float CalculateCircleAnimationProgress(TimeSpan elapsed, float animationProgress)
        {
            float circleAnimationRatio = AnimatedColumnSettings.CircleAnimationDurationRatio;
            float result;

            if (animationProgress > circleAnimationRatio)
            {
                result = 1f;
            }
            else
            {
                result = animationProgress / circleAnimationRatio;
            }

            _appLogger.Debug($"Circle progress: {animationProgress:F3} -> {result:F3}");

            return result;
        }





        public float GetColumnAnimationProgress(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }

        public float TESTGetColumnAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float intervalSeconds = (float)column.AnimationInterval.TotalSeconds;
            float elapsedSeconds = (float)elapsed.TotalSeconds;
            float progress = elapsedSeconds % intervalSeconds;

            _appLogger.Debug($"{column.ColumnType} Progress: {progress:F3} (elapsed {elapsedSeconds:F3}s % interval {intervalSeconds:F3}s)");

            return progress;
        }


        public void UpdateColumnCurrentValue(AnimatedTimerColumn column ,int newValue)
        {
            column.CurrentValue = newValue;
        }


        public void UpdateScrollOffset(AnimatedTimerColumn column ,float scrollOffSet)
        {
            column.ScrollOffset = scrollOffSet;
        }

        public void UpdateAnimationPogress(AnimatedTimerColumn column, float animationProgress)
        {
            column.ColumnAnimationProgress = animationProgress;
        }

        public void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress)
        {
            column.CircleAnimationProgress = circleAnimationProgress;
        }
    }
}
