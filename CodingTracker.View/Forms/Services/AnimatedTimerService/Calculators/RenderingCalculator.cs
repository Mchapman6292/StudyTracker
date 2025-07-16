using CodingTracker.Common.LoggingInterfaces;
using CodingTracker.Logging;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.Calculators
{
    public interface IRenderingCalculator
    {
        float CalculateEasingValue(AnimatedTimerColumn column);
        float CalculateYTranslation(AnimatedTimerColumn column, TimeSpan elapsed);
    }

    public class RenderingCalculator : IRenderingCalculator
    {

        private readonly IApplicationLogger _appLogger;
        

        public RenderingCalculator(IApplicationLogger appLogger)
        {
            _appLogger = appLogger;
        }




        public float CalculateEasingValue(AnimatedTimerColumn column)
        {
            const float midpoint = 0.5f;
            float animationProgress = column.BaseAnimationProgress;

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



        public float CalculateYTranslation(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float easedProgress = CalculateEasingValue(column);

            float baseY;
            float yTranslation;

            // Handle when we reach the top of the column & need to scroll upwards back to start, elapsed check is to stop this occuring on the first 0 - 1 transition.
            if (column.TargetValue == 0 && column.CurrentValue == column.MaxValue && column.IsAnimating && elapsed > column.AnimationInterval)
            {
                _appLogger.Debug($"Wrap around started for column: {column.ColumnType} at {(LoggerHelpers.FormatElapsedTimeSpan(elapsed))}");

                // Wrapping from max to 0, so animate downward from max position.
                baseY = column.MaxValue * AnimatedColumnSettings.SegmentHeight;
                yTranslation = baseY - (easedProgress * baseY);
                return yTranslation;

            }

            else
            {

                baseY = column.CurrentValue * AnimatedColumnSettings.SegmentHeight;
                float endY = column.TargetValue * AnimatedColumnSettings.SegmentHeight;
                float distance = endY - baseY;
                yTranslation = baseY + (easedProgress * distance);


            }


            _appLogger.Debug($"YTranslation calculated : {yTranslation}");
            return yTranslation;
        }





    }
}
