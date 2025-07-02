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
        bool IsTimeToAnimate(AnimatedTimerColumn column, TimeSpan elapsed);
        float CalculateCircleAnimationProgress(TimeSpan elapsed, float animationProgress);
        float GetColumnAnimationProgress(TimeSpan elapsed);
        float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed);
        void UpdateColumnCurrentValue(AnimatedTimerColumn column, int newValue);
        void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet);
        void UpdateAnimationPogress(AnimatedTimerColumn column, float animationProgress);
        void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress);

        float TESTGetColumnAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);


        bool NEWIsStillWithinAnimationWindow(AnimatedTimerColumn column, TimeSpan elapsed);
        void NEWUpdateIsAnimating(AnimatedTimerColumn column, bool isAnimating);
        void NEWUpdateLastAnimationStartTime(AnimatedTimerColumn column, TimeSpan lastAnimationStartTime);
        void NEWUpdateNextTransitionTime(AnimatedTimerColumn column, TimeSpan elapsed);
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


        private void LogColumn(List<AnimatedTimerColumn> columns)
        {
            var column = columns.FirstOrDefault();

            _appLogger.Debug($"Transition time set to {column.NextTransitionTime}.");
        }



        public void UpdateAnimationState(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            // Handle SecondsSingleDigitsColumn which is always highlighting.

            /*
            _appLogger.Debug($"Elapsed: {elapsed.TotalSeconds:F3}s, NextTransition: {column.NextTransitionTime.TotalSeconds:F3}s");
            _appLogger.Debug($"IsAnimating: {column.IsAnimating}, CurrentValue: {column.CurrentValue}, PreviousValue: {column.PreviousValue}");
            */


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
                NEWUpdateLastAnimationStartTime(column, elapsed);
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
                var difference = (elapsed - column.LastAnimationStartTime);
                if ((elapsed - column.LastAnimationStartTime) >= AnimatedColumnSettings.AnimationDuration)
                {
                    _appLogger.Debug($"");
                    column.IsAnimating = false;
                }
            }
            _appLogger.Debug($"=== UpdateAnimationState END - IsAnimating: {column.IsAnimating} ===\n");
        }





     

        public bool IsTimeToAnimate(AnimatedTimerColumn column ,TimeSpan elapsed)
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

            _appLogger.Debug($"Transition time updated by {column.NextTransitionTime} + {column.AnimationInterval} = {column.NextTransitionTime}.");
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

        public void NEWUpdateLastAnimationStartTime(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            var old = column.LastAnimationStartTime;
            column.LastAnimationStartTime = elapsed;
            _appLogger.Debug($"PREVIOUS LASTANIMATIONSTARTTIME time: {old}, new LASTANIMATIONSTARTTIME time: {column.NextTransitionTime}");
        }



        public bool NEWIsStillWithinAnimationWindow(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            var endDurationTime = column.NextTransitionTime + AnimatedColumnSettings.AnimationDuration;

            if (elapsed >= endDurationTime)
            {
                return false;
            }
            return true;
        }

        public void NEWUpdateIsAnimating(AnimatedTimerColumn column, bool isAnimating)
        {
            column.IsAnimating = isAnimating;
        }

        public void NEWUpdateNextTransitionTime(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            var old = column.NextTransitionTime;

            column.NextTransitionTime = column.LastAnimationStartTime + column.AnimationInterval;

            _appLogger.Debug($"PREVIOUS NEXT TRANSITION TIME transition time: {old}, next transition time: {column.NextTransitionTime}");

        }

        public void NEWCalculateColumnValue(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            // Get the count of all segments with a value.
            int count = column.TimerSegments.Count(segment => segment.Value != 0);
        }
    }
}
