using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {
        int CalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType);
        float CalculateScrollOffset(AnimatedTimerColumn column, float animationProgress);
        bool IsWithinAnimationInterval(AnimatedTimerColumn column, TimeSpan elapsed);
    }

    public class AnimatedColumnStateManager : IAnimatedColumnStateManager
    {
        private readonly IApplicationLogger _appLogger;
        private readonly ISegmentOverlayCalculator _segmentOverlayCalulcator;


        public AnimatedColumnStateManager(IApplicationLogger appLogger, ISegmentOverlayCalculator segmentOverlayCalulcator)
        {
            _appLogger = appLogger;
            _segmentOverlayCalulcator = segmentOverlayCalulcator;
        }



        public bool IsWithinAnimationInterval(AnimatedTimerColumn column ,TimeSpan elapsed)
        {
            double secondsUntilChange = column.AnimationInterval.TotalSeconds - (elapsed.TotalSeconds / column.AnimationInterval.TotalSeconds);
            return secondsUntilChange <= 1.0;
        }

        public int CalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType)
        {
            int totalSeconds = (int)elapsed.TotalSeconds;
            int minutes = totalSeconds / 60;
            int hours = totalSeconds / 3600;

            switch (columnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    return totalSeconds % 10;
                case ColumnUnitType.SecondsLeadingDigit:
                    return (totalSeconds / 10) % 6;
                case ColumnUnitType.MinutesSingleDigits:
                    return minutes % 10;
                case ColumnUnitType.MinutesLeadingDigits:
                    return (minutes / 10) % 6;
                case ColumnUnitType.HoursSinglesDigits:
                    return hours % 10;
                case ColumnUnitType.HoursLeadingDigits:
                    return (hours / 10) % 10;
                default:
                    return 0;
            }
        }

        private void UpdateNextTransitionTime(AnimatedTimerColumn column)
        {
            column.NextTransitionTime = column.NextTransitionTime + column.AnimationInterval;
        }



        public void UpdateAnimationState(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            // Handle SecondsSingleDigitsColumn which is always highlighting.

            if (column.ColumnType == ColumnUnitType.SecondsSingleDigits)
            {
                column.IsAnimating = true;
                int totalSeconds = (int)elapsed.TotalSeconds;
                column.PreviousValue = column.CurrentValue;
                column.CurrentValue = CalculateColumnValue(elapsed, column.ColumnType);
                return;
            }

            if (!column.IsAnimating && elapsed >= column.NextTransitionTime)
            {
                column.IsAnimating = true;
                column.LastAnimationStartTime = elapsed;
                column.PreviousValue = column.CurrentValue;
                column.CurrentValue = CalculateColumnValue(elapsed, column.ColumnType);
                UpdateNextTransitionTime(column);
            }
            
            // If the column is animating check if the difference between elapsed and lastanimationStartTime is greater than the animated duration.
            if(column.IsAnimating)
            {
                if((elapsed - column.LastAnimationStartTime) >= AnimatedColumnSettings.AnimationDuration)
                {
                    column.IsAnimating = false;
                }
            }
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



    }
}
