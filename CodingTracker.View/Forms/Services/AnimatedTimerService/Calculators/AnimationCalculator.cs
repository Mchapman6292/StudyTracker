using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Logging;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using System.Buffers.Text;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators
{
    public interface IAnimationCalculator
    {
        float CalculateAnimationProgress(TimeSpan elapsed);
        float CalculateColumnScrollProgress(float animationProgress);
        double CalculateSecondsUntilNextAnimationInterval(AnimatedTimerColumn column, TimeSpan elapsed);
        float CalculateEasingValue(float animationProgress);
        float CalculateYTranslation(AnimatedTimerColumn column, TimeSpan elapsed, float animationProgress);
        int CalculateTargetDigitByElapsed(TimeSpan elapsed, ColumnUnitType columnType);

        float CalculateDistanceForReset(AnimatedTimerColumn column);
        float TESTCalculateYTranslation(AnimatedTimerColumn column, TimeSpan elapsed, float animationProgress);
        float CalculateRestartAnimationProgress(TimeSpan restartTimerElapsed);

    }

    public class AnimationCalculator : IAnimationCalculator
    {

        private readonly IApplicationLogger _appLogger;
        

        public AnimationCalculator(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }




        public float CalculateAnimationProgress(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }


        public float CalculateColumnScrollProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;

            return (animationProgress - 0.5f) / 0.5f;
        }

        public double CalculateSecondsUntilNextAnimationInterval(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            // Modulo calculates how far into the current cycle, then subtract this from the interval to get a value between 0 and the animation interval
            return column.AnimationInterval.TotalSeconds - (elapsed.TotalSeconds % column.AnimationInterval.TotalSeconds); 
        }




        public float CalculateEasingValue(float animationProgress)
        {
            const float midpoint = 0.5f;


            if (animationProgress < midpoint)
            {
                // If progress less than 0.5, scale it to get a value between 0–1 and then apply cubic easing.
                float scaledAnimationProgress = animationProgress * 2;
                return (scaledAnimationProgress * scaledAnimationProgress * scaledAnimationProgress) / 2;
            }
            else
            {
                // Again we need to transform the animation value between 0.5–1.0 to a value between 0–1, 
                float scaledAnimationProgress = (animationProgress - 0.5f) * 2;

                // If we use the same approach as we did with animation < midpoint then we are left with a slowly increasing value instead of a rapidly increasing one..

                float reversedProgress = 1f - scaledAnimationProgress;    // Flip the scaled value to prepare for cubic-out easing
                float cubicEased = reversedProgress * reversedProgress * reversedProgress;  // Apply cubic function to reversed progress
                float normalized = cubicEased / 2f;   // Normalize result to keep easing curve within 0–1 bounds

      
                float result = 1f - normalized; // Invert and shift to complete the ease-out curve
                return result;
            }
        }



        public float CalculateYTranslation(AnimatedTimerColumn column, TimeSpan elapsed, float animationProgress)
        {
            float easedProgress = CalculateEasingValue(animationProgress);

            float startY;
            float yTranslation;

            // Handle when we reach the top of the column & need to scroll upwards back to start, elapsed check is to stop this occuring on the first 0 - 1 transition.
            if (column.TargetSegmentValue == 0 && column.CurrentValue == column.MaxValue && column.IsAnimating && elapsed > column.AnimationInterval)
            {
                _appLogger.Debug($"Wrap around started for column: {column.ColumnType} at {(LoggerHelpers.FormatElapsedTimeSpan(elapsed))}");

                // Wrapping from max to 0, so animate downward from max position.
                startY = column.MaxValue * AnimatedColumnSettings.SegmentHeight;
                yTranslation = startY - (easedProgress * startY);
                return yTranslation;

            }
            else
            {

                startY = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                float endY = column.TargetSegmentValue * AnimatedColumnSettings.SegmentHeight;
                float distance = endY - startY;
                yTranslation = startY + (easedProgress * distance);


            }
            return yTranslation;
        }



        public float TESTCalculateYTranslation(AnimatedTimerColumn column, TimeSpan elapsed, float animationProgress)
        {
            float easedProgress = CalculateEasingValue(animationProgress);

            float startY;
            float currentY;
            float endY;

            // Handle when we reach the top of the column & need to scroll upwards back to start, elapsed check is to stop this occuring on the first 0 - 1 transition.
            if (column.TargetSegmentValue == 0 && column.CurrentValue == column.MaxValue && column.IsAnimating && elapsed > column.AnimationInterval && !column.IsRestarting)
            {
                _appLogger.Debug($"Wrap around started for column: {column.ColumnType} at {(LoggerHelpers.FormatElapsedTimeSpan(elapsed))}");

                startY = column.MaxValue * AnimatedColumnSettings.SegmentHeight;
                endY = 0;
                currentY = startY - (easedProgress * startY); 
                return currentY;

            }

            else if (column.IsRestarting)
            {
                startY = column.YLocationAtRestart;
                endY = 0;
                float distance = endY - startY;
                currentY = startY + (easedProgress * distance);
                return currentY;
            }
            // Normal animation transition.
            else
            {
                startY = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                endY = column.TargetSegmentValue * AnimatedColumnSettings.SegmentHeight;
                float distance = endY - startY;
                currentY = startY + (easedProgress * distance);
                return currentY;
            }
        }








        public int CalculateTargetDigitByElapsed(TimeSpan elapsed, ColumnUnitType columnType)
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






        public float CalculateDistanceForReset(AnimatedTimerColumn column)
        {
            float resetYLocation = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
            float currentYlocation = resetYLocation - column.YTranslation;


            return currentYlocation;
        }


        
        public float CalculateRestartAnimationProgress(TimeSpan restartTimerElapsed)
        {
            double elapsedTotalSeconds = restartTimerElapsed.TotalSeconds;

            float restartProgress = (float)elapsedTotalSeconds;
            float testProgress = restartProgress * 1000;

            return testProgress;

        }






    }
}
