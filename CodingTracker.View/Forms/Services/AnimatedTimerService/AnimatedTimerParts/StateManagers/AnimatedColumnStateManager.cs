using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.SkiaSharpView;
using System.Timers;
using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {

        void UpdateAnimationState(AnimatedTimerColumn column, TimeSpan elapsed);
        int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column);
        float CalculateScrollOffset(AnimatedTimerColumn column, float animationProgress, bool isAnimating);

        bool IsElapsedGreaterThanNextTransitionTime(AnimatedTimerColumn column, TimeSpan elapsed);
        float CalculateCircleAnimationProgress(TimeSpan elapsed, float animationProgress);
        float GetColumnAnimationProgress(TimeSpan elapsed);
        float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed);
        void UpdateColumnCurrentValue(AnimatedTimerColumn column, int newValue);
        void UpdateColumnPreviousValue(AnimatedTimerColumn column, int previousValue);
        void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet);
        void UpdateAnimationPogress(AnimatedTimerColumn column, float animationProgress);
        void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress);

        float TESTGetColumnAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);


        bool NEWIsStillWithinAnimationWindow(AnimatedTimerColumn column, TimeSpan elapsed);
        void NEWUpdateIsAnimating(AnimatedTimerColumn column, bool isAnimating);
        void NEWUpdateAnimationStartTime(AnimatedTimerColumn column, TimeSpan lastAnimationStartTime);
        void NEWUpdateNextTransitionTime(AnimatedTimerColumn column, TimeSpan elapsed);


        int ExtractTimeDigitForColumn(AnimatedTimerColumn column, TimeSpan elapsed);


        AnimatedTimerSegment NEWFindNewTimeSegmentByTimeDigit(AnimatedTimerColumn column, int timeDigit);
        void UpdateFocusedSegment(AnimatedTimerColumn column, AnimatedTimerSegment segment);
        void NEWUpdateCurrentAnimationEndTime(AnimatedTimerColumn column, TimeSpan elapsed);
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
                NEWUpdateAnimationStartTime(column, elapsed);
                column.PreviousValue = column.CurrentValue;
                column.CurrentValue = CalculateColumnValue(elapsed, column);
                UpdateNextTransitionTime(column);

                _appLogger.Debug($"Animation started at {column.AnimationStartTime.TotalSeconds:F3}s");
                _appLogger.Debug($"Value changing from {column.PreviousValue} to {column.CurrentValue}");

                TimeSpan oldNextTransition = column.NextTransitionTime;
                UpdateNextTransitionTime(column);

                _appLogger.Debug($"NextTransitionTime updated from {oldNextTransition.TotalSeconds:F3}s to {column.NextTransitionTime.TotalSeconds:F3}s");

            }

            // If the column is animating check if the difference between elapsed and lastanimationStartTime is greater than the animated duration.
            if (column.IsAnimating)
            {
                var difference = (elapsed - column.AnimationStartTime);
                if ((elapsed - column.AnimationStartTime) >= AnimatedColumnSettings.AnimationDuration)
                {
                    _appLogger.Debug($"");
                    column.IsAnimating = false;
                }
            }
            _appLogger.Debug($"=== UpdateAnimationState END - IsAnimating: {column.IsAnimating} ===\n");
        }







        public bool IsElapsedGreaterThanNextTransitionTime(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            bool isElapsed = elapsed >= column.NextTransitionTime;
            if (isElapsed)
            {
                _appLogger.Debug($" \n  --- ELAPSED GREATER THAN NEXT TRANSITION TIME, ANIMATION STARTING  --- . elapsed: {elapsed}, NextTransitionTIme: {column.NextTransitionTime}.");
            }
            return isElapsed;        
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
        public float CalculateScrollOffset(AnimatedTimerColumn column, float animationProgress, bool isAnimating)
        {
            if(!isAnimating)
            {
                // Return scroll off set = same position if we are not animating.
                return column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            }

            float baseOffset = column.PreviousValue * AnimatedColumnSettings.SegmentHeight;
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


            return Math.Min(1.0f, result);
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

            _appLogger.Debug($"Animation progress calculated: {progress} elapsed time: {elapsed}, AnimationEndTime {column.CurrentAnimationEndTime}.");

            return progress;
        }


        public void UpdateColumnCurrentValue(AnimatedTimerColumn column, int newValue)
        {
            column.CurrentValue = newValue;
        }

        public void UpdateColumnPreviousValue(AnimatedTimerColumn column, int previousValue)
        {
            column.PreviousValue = previousValue;   
        }


        public void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet)
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

        public void NEWUpdateAnimationStartTime(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            var old = column.AnimationStartTime;
            column.AnimationStartTime = elapsed;
            _appLogger.Debug($"Last animation startTime updated to {column.AnimationStartTime}. Previous value : {old}.");
        }




        // Probably not needed
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

            column.NextTransitionTime = column.AnimationStartTime + column.AnimationInterval;

            _appLogger.Debug($"Next trasnsitionT time updated to {column.NextTransitionTime}. Previous value : {old}.");

        }

        /// Calculates the digit value to display for the specified column based on the elapsed time.
        ///  Uses `% 10` to extract the single (units) digit and `/ 10` to extract the leading (tens) digit
        ///  Then use this to find the correct TimerSegment and update the focused segment to it
        public int ExtractTimeDigitForColumn(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            ColumnUnitType columnType = column.ColumnType;

            switch (columnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    return elapsed.Seconds % 10;
                case ColumnUnitType.SecondsLeadingDigit:
                    return elapsed.Seconds / 10;

                case ColumnUnitType.MinutesSingleDigits:
                    return elapsed.Minutes % 10;
                case ColumnUnitType.MinutesLeadingDigits:
                    return elapsed.Minutes / 10;

                case ColumnUnitType.HoursSinglesDigits:
                    return elapsed.Hours % 10;
                case ColumnUnitType.HoursLeadingDigits:
                    return elapsed.Hours / 10;

                default:
                    _appLogger.Fatal($"{nameof(ExtractTimeDigitForColumn)} returned default value, this should never happen.");
                    throw new InvalidOperationException("Unhandled ColumnUnitType in ExtractTimeDigitForColumn.");


            }
        }

        public AnimatedTimerSegment NEWFindNewTimeSegmentByTimeDigit(AnimatedTimerColumn column, int timeDigit)
        {
            return column.TimerSegments.FirstOrDefault(x => x.Value == timeDigit);

          
        }


        public void UpdateFocusedSegment(AnimatedTimerColumn column, AnimatedTimerSegment segment)
        {
            column.FocusedSegment = segment;
    
        }

        public void NEWUpdateCurrentAnimationEndTime(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            column.CurrentAnimationEndTime = elapsed + AnimatedColumnSettings.AnimationDuration;
            _appLogger.Debug($"Current animation end time updated to: {column.CurrentAnimationEndTime}");
        }





       
    }
}



