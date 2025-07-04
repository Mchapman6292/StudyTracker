using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System.Timers;
using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {

        int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column);
        float CalculateVerticalOffset(AnimatedTimerColumn column, bool isAnimating);

        bool IsElapsedGreaterThanNextTransitionTime(AnimatedTimerColumn column, TimeSpan elapsed);
        float CalculateCircleAnimationProgress(TimeSpan elapsed, float animationProgress);

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

        float NEWCalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);
        float CalculateEasingForVertialOffSet(float animationProgress);
        void UpdateNormalizedColumnAnimationProgress(AnimatedTimerColumn column, float animationProgress);
        SKPoint CalculateNewColumnLocationDuringAnimation(AnimatedTimerColumn column);
        void UpdateColumnLocation(AnimatedTimerColumn column, SKPoint newLocation);
        SKPoint TESTCalculateNewColumnLocationDuringAnimation(AnimatedTimerColumn column);



        float TESTCalculateAnimationProgress(TimeSpan elapsed);
        float TESTCalculateNormalizedProgress(float animationProgress);
        float TESTCalculateScrollOffset(AnimatedTimerColumn column, float animationProgress);
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


        /*
        public void UpdateAnimationState(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            // Handle SecondsSingleDigitsColumn which is always highlighting.

            /*
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
        */




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
        public float CalculateVerticalOffset(AnimatedTimerColumn column, bool isAnimating)
        {
            if (!isAnimating)
            {
                return column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            }

            float startY = column.Location.Y;
            float endY = column.CurrentValue * AnimatedColumnSettings.SegmentHeight; ;

   

            _appLogger.Debug($"StartOffSet calculated: {startY}, EndOffSet calculated: {endY}");


            float easedProgress = CalculateEasingForVertialOffSet(column.ColumnAnimationProgress);

            var result = Single.Lerp(startY, endY, easedProgress);


            _appLogger.Debug($"Offset calculated: {result}.");

            return result;
        }




        public float CalculateEasingForVertialOffSet(float animationProgress)
        {
            const float midpoint = 0.5f;

            if (animationProgress < midpoint)
            {
                float scaledProgress = animationProgress;
                float easeInValue = 4f * scaledProgress * scaledProgress * scaledProgress;
                return easeInValue;
            }
            else
            {

                float adjustedProgress = -2f * animationProgress + 2f; // Maps 0.5→1 to 1→0

                // Cubic ease-out: 1 - ((1-t)^3)
                float powerOfThree = MathF.Pow(adjustedProgress, 3f);
                float normalizedAnimationProgress = 1f - (powerOfThree / 2f);

                return normalizedAnimationProgress;
            }
        }




     

        public void UpdateNormalizedColumnAnimationProgress(AnimatedTimerColumn column, float animationProgress)
        {
            column.NormalizedColumnAnimationProgress = animationProgress;
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


        public void UpdateColumnLocation(AnimatedTimerColumn column, SKPoint newLocation)
        {
            column.Location = newLocation;
        }


        public SKPoint CalculateNewColumnLocationDuringAnimation(AnimatedTimerColumn column)
        {
            float x = column.Location.X;
            float newY = column.Location.Y - column.ScrollOffset;

           

            return new SKPoint(x, newY);
        }


        public SKPoint TESTCalculateNewColumnLocationDuringAnimation(AnimatedTimerColumn column)
        {
            float x = column.Location.X;
            float moveDistance = column.NormalizedColumnAnimationProgress * AnimatedColumnSettings.SegmentHeight;
            float newY = column.Location.Y - moveDistance;  // Negative to move UP
            return new SKPoint(x, newY);
        }









        public float TESTCalculateAnimationProgress(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }

        public float TESTCalculateNormalizedProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;
            return (animationProgress - 0.5f) / 0.5f;
        }


        public float TESTCalculateScrollOffset(AnimatedTimerColumn column, float animationProgress)
        {
            float baseOffset = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            float additionalScroll = animationProgress * AnimatedColumnSettings.SegmentHeight;

            return baseOffset + additionalScroll;
        }





        public float TESTGetColumnAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float intervalSeconds = (float)column.AnimationInterval.TotalSeconds;
            float elapsedSeconds = (float)elapsed.TotalSeconds;
            float progress = elapsedSeconds % intervalSeconds;

            _appLogger.Debug($"Animation progress calculated: {progress} elapsed time: {elapsed}, AnimationEndTime {column.CurrentAnimationEndTime}.");

            return progress;
        }


        public float NEWCalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            TimeSpan totalDuration = column.CurrentAnimationEndTime - column.AnimationStartTime;

            TimeSpan timeSinceStart = elapsed - column.AnimationStartTime;

            float progress = (float)(timeSinceStart.TotalMilliseconds / totalDuration.TotalMilliseconds);

            _appLogger.Debug($"Calculating animation progress time since start: {timeSinceStart.TotalMilliseconds}, expected animation end time: {column.CurrentAnimationEndTime}.");

            var result = Math.Clamp(progress, 0f, 1f);

            _appLogger.Debug($"AnimationProgress calculated: {result}");

            return result;
        }


    }
}



