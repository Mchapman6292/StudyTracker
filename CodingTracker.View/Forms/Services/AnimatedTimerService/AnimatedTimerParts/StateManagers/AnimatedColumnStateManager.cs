using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using LiveChartsCore.SkiaSharpView;
using SkiaSharp;
using System.Timers;
using Windows.ApplicationModel.Activation;
using Windows.Networking.PushNotifications;
using static Guna.UI2.Material.Animation.AnimationManager;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {

        int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column);

        float CalculateCircleAnimationProgress(TimeSpan elapsed, float animationProgress);

        float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed);
        void UpdateColumnCurrentValue(AnimatedTimerColumn column, int newValue);
        void UpdateColumnPreviousValue(AnimatedTimerColumn column, int previousValue);
        void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet);
        void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress);


        bool NEWIsStillWithinAnimationWindow(AnimatedTimerColumn column, TimeSpan elapsed);
        void NEWUpdateIsAnimating(AnimatedTimerColumn column, bool isAnimating);
        void NEWUpdateAnimationStartTime(AnimatedTimerColumn column, TimeSpan lastAnimationStartTime);
        void NEWUpdateNextTransitionTime(AnimatedTimerColumn column, TimeSpan elapsed);


        int ExtractTimeDigitForColumn(AnimatedTimerColumn column, TimeSpan elapsed);


        AnimatedTimerSegment NEWFindNewTimeSegmentByTimeDigit(AnimatedTimerColumn column, int timeDigit);
        void UpdateFocusedSegment(AnimatedTimerColumn column, AnimatedTimerSegment segment);
        void NEWUpdateCurrentAnimationEndTime(AnimatedTimerColumn column, TimeSpan elapsed);

   
        void UpdateNormalizedColumnAnimationProgress(AnimatedTimerColumn column, float animationProgress);








        public void WORKINGUpdateAnimationProgress(AnimatedTimerColumn column, float animationProgress);

        public void WORKINGUpdateNormalizedAnimationProgress(AnimatedTimerColumn column, float normalizedProgress);

        int WORKINGCalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType);
    }

    public class AnimatedColumnStateManager : IAnimatedColumnStateManager
    {
        private readonly IApplicationLogger _appLogger;





        public AnimatedColumnStateManager(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }




        private void LogColumn(List<AnimatedTimerColumn> columns)
        {
            var column = columns.FirstOrDefault();

            _appLogger.Debug($"Transition time set to {column.NextTransitionTime}.");
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






   
        

   

        public void UpdateNormalizedColumnAnimationProgress(AnimatedTimerColumn column, float animationProgress)
        {
            column.NormalizedAnimationProgress = animationProgress;
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



        public SKPoint TESTCalculateNewColumnLocationDuringAnimation(AnimatedTimerColumn column)
        {
            float x = column.Location.X;
            float moveDistance = column.NormalizedAnimationProgress * AnimatedColumnSettings.SegmentHeight;
            float newY = column.Location.Y - moveDistance;  // Negative to move UP
            return new SKPoint(x, newY);
        }




























        #region WorkingMethods

        public int WORKINGCalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType)
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



        private bool WORKINGShouldColumnAnimate(TimeSpan elapsed, AnimatedTimerColumn column)
        {
            float animationDurationFloat = AnimatedColumnSettings.AnimationDurationFloat;

            switch (column.ColumnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    double secondsCycle = elapsed.TotalSeconds % 1.0;
                    return secondsCycle >= (1.0 - animationDurationFloat);
                case ColumnUnitType.SecondsLeadingDigit:
                    double tenSecondsCycle = elapsed.TotalSeconds % 10.0;
                    return tenSecondsCycle >= (10.0 - animationDurationFloat);
                case ColumnUnitType.MinutesSingleDigits:
                    double minutesCycle = elapsed.TotalSeconds % 60.0;
                    return minutesCycle >= (60.0 - animationDurationFloat);
                case ColumnUnitType.MinutesLeadingDigits:
                    double tenMinutesCycle = elapsed.TotalSeconds % 600.0;
                    return tenMinutesCycle >= (600.0 - animationDurationFloat);
                case ColumnUnitType.HoursSinglesDigits:
                    double hoursCycle = elapsed.TotalSeconds % 3600.0;
                    return hoursCycle >= (3600.0 - animationDurationFloat);
                case ColumnUnitType.HoursLeadingDigits:
                    double tenHoursCycle = elapsed.TotalSeconds % 36000.0;
                    return tenHoursCycle >= (36000.0 - animationDurationFloat);
                default:
                    return false;
            }
        }



        public float WORKINGCalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            ColumnUnitType columnType = column.ColumnType;

            float animationDurationFloat = AnimatedColumnSettings.AnimationDurationFloat;

            switch (columnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    double secondsCycle = elapsed.TotalSeconds % 1.0;
                    return secondsCycle >= 0.7 ? (float)((secondsCycle - 0.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.SecondsLeadingDigit:
                    double tenSecondsCycle = elapsed.TotalSeconds % 10.0;
                    return tenSecondsCycle >= 9.7 ? (float)((tenSecondsCycle - 9.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.MinutesSingleDigits:
                    double minutesCycle = elapsed.TotalSeconds % 60.0;
                    return minutesCycle >= 59.7 ? (float)((minutesCycle - 59.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.MinutesLeadingDigits:
                    double tenMinutesCycle = elapsed.TotalSeconds % 600.0;
                    return tenMinutesCycle >= 599.7 ? (float)((tenMinutesCycle - 599.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.HoursSinglesDigits:
                    double hoursCycle = elapsed.TotalSeconds % 3600.0;
                    return hoursCycle >= 3599.7 ? (float)((hoursCycle - 3599.7) / animationDurationFloat) : 0.0f;
                case ColumnUnitType.HoursLeadingDigits:
                    double tenHoursCycle = elapsed.TotalSeconds % 36000.0;
                    return tenHoursCycle >= 35999.7 ? (float)((tenHoursCycle - 35999.7) / animationDurationFloat) : 0.0f;
                default:
                    return 0.0f;
            }
        }
        


        public void WORKINGSetFocusedSegmentByValue(AnimatedTimerColumn column, int newValue)
        {
            var focusedSegment = column.TimerSegments.FirstOrDefault(s => s.Value == newValue);
        }




        public float WORKINGCalculateNormalizedProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;

            return (animationProgress - 0.5f) / 0.5f;
        }



        public void WORKINGUpdateAnimationProgress(AnimatedTimerColumn column, float animationProgress)
        {
            column.AnimationProgress = animationProgress;
        }


        public void WORKINGUpdateNormalizedAnimationProgress(AnimatedTimerColumn column, float normalizedProgress)
        {
            column.NormalizedAnimationProgress = normalizedProgress;
        }







    }
}



#endregion