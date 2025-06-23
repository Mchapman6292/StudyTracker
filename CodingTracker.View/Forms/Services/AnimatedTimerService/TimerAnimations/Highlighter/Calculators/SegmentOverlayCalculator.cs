using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators
{
    public interface ISegmentOverlayCalculator
    {
        float CalculateSegmentAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);
    }

    public class SegmentOverlayCalculator : ISegmentOverlayCalculator
    {


        public float CalculateSegmentAnimationProgress(AnimatedTimerColumn column ,TimeSpan elapsed)
        {
            float elapsedSinceLastAnimation = (float)(elapsed - column.LastAnimationStartTime).TotalMilliseconds;

            float animatioDurationFloat = (float)AnimatedColumnSettings.AnimationDuration.TotalMilliseconds;


            return (elapsedSinceLastAnimation / animatioDurationFloat) * 100;


        }


        public float CalculateOpacity(float animationProgress)
        {

        }
    }
}
