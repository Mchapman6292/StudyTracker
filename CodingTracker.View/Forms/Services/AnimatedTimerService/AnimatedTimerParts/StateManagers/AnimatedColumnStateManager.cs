using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.View.FormManagement;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts.StateManagers
{
    public interface IAnimatedColumnStateManager
    {

        int CalculateColumnValue(TimeSpan elapsed, AnimatedTimerColumn column);



        float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed);
        void UpdateColumnPreviousValue(AnimatedTimerColumn column, int previousValue);
        void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet);

        void NEWUpdateIsAnimating(AnimatedTimerColumn column, bool isAnimating);
 



        AnimatedTimerSegment NEWFindNewTimeSegmentByTimeDigit(AnimatedTimerColumn column, int timeDigit);
        void UpdateFocusedSegment(AnimatedTimerColumn column, AnimatedTimerSegment segment);



        float TESTCalculateCircleAnimationProgress(AnimatedTimerColumn column);

        //
        float CalculateCircleAnimationProgress(AnimatedTimerColumn column);
        void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress);

        //


        void UpdatedNumberBlurringStartAnimationActive(AnimatedTimerColumn column, bool blurred);


        void WORKINGUpdateAnimationProgress(AnimatedTimerColumn column, float animationProgress);

        void WORKINGUpdateColumnScrollProgress(AnimatedTimerColumn column, float normalizedProgress);

        int WORKINGCalculateColumnValue(TimeSpan elapsed, ColumnUnitType columnType);
        float WORKINGCalculateEasingValue(AnimatedTimerColumn column, TimerAnimationType animationType);
    }

    public class AnimatedColumnStateManager : IAnimatedColumnStateManager
    {
        private readonly IApplicationLogger _appLogger;





        public AnimatedColumnStateManager(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
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






   
        

        public float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            return Single.Lerp(AnimatedColumnSettings.MaxRadius, AnimatedColumnSettings.MinRadius, column.CircleAnimationProgress);
        }



        public float TESTCalculateCircleAnimationProgress(AnimatedTimerColumn column)
        {
            return column.BaseAnimationProgress / AnimatedColumnSettings.CircleAnimationDurationRatio;
        }


        public float CalculateCircleAnimationProgress(AnimatedTimerColumn column)
        {
            float circleAnimationRatio = AnimatedColumnSettings.CircleAnimationDurationRatio;
            float result;

            if (column.BaseAnimationProgress > circleAnimationRatio)
            {
                result = 1f;
            }
            else
            {
                result = column.BaseAnimationProgress / circleAnimationRatio;
            }
            return Math.Min(1.0f, result);
        }





  
        public void UpdateColumnPreviousValue(AnimatedTimerColumn column, int previousValue)
        {
            column.CurrentValue = previousValue;
        }


        public void UpdateScrollOffset(AnimatedTimerColumn column, float scrollOffSet)
        {
            column.YTranslation = scrollOffSet;
        }







        public void NEWUpdateIsAnimating(AnimatedTimerColumn column, bool isAnimating)
        {
            column.IsAnimating = isAnimating;
 
        }

        public AnimatedTimerSegment NEWFindNewTimeSegmentByTimeDigit(AnimatedTimerColumn column, int timeDigit)
        {
            return column.TimerSegments.FirstOrDefault(x => x.Value == timeDigit);

          
        }


        public void UpdateFocusedSegment(AnimatedTimerColumn column, AnimatedTimerSegment segment)
        {
            column.FocusedSegment = segment;
    
        }


        public void UpdateColumnLocation(AnimatedTimerColumn column, SKPoint newLocation)
        {
            column.Location = newLocation;
        }






 


        public void UpdateCircleAnimationProgress(AnimatedTimerColumn column, float circleAnimationProgress)
        {
            column.CircleAnimationProgress = circleAnimationProgress;
        }



        public void UpdatedNumberBlurringStartAnimationActive(AnimatedTimerColumn column, bool blurred)
        {
            column.NumberBlurringStartAnimationActive = blurred;
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
            float animationDurationFloat = AnimatedColumnSettings.ScrollAnimationDurationRatio;

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
            column.BaseAnimationProgress = animationProgress;
        }


        public void WORKINGUpdateColumnScrollProgress(AnimatedTimerColumn column, float scolumnScrollProgress)
        {
            column.ColumnScrollProgress = scolumnScrollProgress;
        }







        public float WORKINGCalculateEasingValue(AnimatedTimerColumn column, TimerAnimationType animationType)
        {
            // Use different progress values to calculate the easing.
            float animationProgress = animationType switch
            {
                TimerAnimationType.CircleAnimation => column.CircleAnimationProgress,
                TimerAnimationType.ColumnScroll => column.ColumnScrollProgress,
                _ => throw new ArgumentException($"Unsupported animation type: {animationType}")
            };

   
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


        /*
        public float CalculateEasingValue(AnimatedTimerColumn column, TimerAnimationType animationType)
        {
            const float midpoint = 0.5f;
            float animationProgress = column.BaseAnimationProgress; 

            if(animationProgress < midpoint)
            {
                // If progress less than 0.5 scale it to get a value between 0-1 and then apply cubic easing.
                float scaledAnimationProgress = animationProgress * 2;
                return (scaledAnimationProgress * scaledAnimationProgress * scaledAnimationProgress) / 2;
            }
            else
            {
                // Again we need to transform the animation value between 0.5-0.1 to a value between 0 & 1.
                // Subtract 0.5 then scale it.

                float scaledAnimationProgress = (animationProgress - 0.5f ) * 2;
                
                // If we use the same approach as we did with animation < midpoint then 

            }

        }
        */


        public void HandleNumberBlurringStartAnimationActive(AnimatedTimerColumn column, TimeSpan elapsed)
        {

        }




    }
}



#endregion