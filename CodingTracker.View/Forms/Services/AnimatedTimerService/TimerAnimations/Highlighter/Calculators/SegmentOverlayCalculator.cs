using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using System.Security.Policy;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators
{
    public interface ISegmentOverlayCalculator
    {
        float CalculateAnimationProgress(TimeSpan elapsed);
        float CalculateNormalizedProgress(float animationProgress);
        float CalculateNormalizedRadius(AnimatedTimerColumn column);

    }

    public class SegmentOverlayCalculator : ISegmentOverlayCalculator
    {
        private const float _baseRadius = 20f;
        private const float _minRadiusScale = 0.5f;


        public float CalculateAnimationProgress(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }

        public float CalculateNormalizedProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;

            return (animationProgress - 0.5f) / 0.5f;
        }


        private float CalculateRadiusMultiplier(float normalizedProgress)
        {
            return AnimatedColumnSettings.minRadiusScale + (normalizedProgress * (1.0f - AnimatedColumnSettings.minRadiusScale));
        }

        public float CalculateNormalizedRadius(AnimatedTimerColumn column)
        {
            float radiusMultiplier = CalculateRadiusMultiplier(column.NormalizedProgress);

            return column.OverlayRadius * radiusMultiplier;
        }



        public float CalculateSegmentAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float elapsedSinceLastAnimation = (float)(elapsed - column.LastAnimationStartTime).TotalMilliseconds;

            float animatioDurationFloat = (float)AnimatedColumnSettings.AnimationDuration.TotalMilliseconds;


            return (elapsedSinceLastAnimation / animatioDurationFloat) * 100;
        }








    }
}
