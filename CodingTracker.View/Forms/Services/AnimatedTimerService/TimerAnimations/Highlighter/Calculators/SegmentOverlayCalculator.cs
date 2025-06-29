using CodingTracker.Common.Utilities;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Security.Policy;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators
{
    public interface ISegmentOverlayCalculator
    {
        float CalculateAnimationProgress(TimeSpan elapsed);
        float CalculateNormalizedProgress(float animationProgress);
        float CalculateNormalizedRadius(AnimatedTimerColumn column);
        float CalculateInvertedNormalizedProgress(float animationProgress);





        float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed);
        float CalculateCircleAnimationOpacity(AnimatedTimerColumn column, TimeSpan elapsed);

    }

    public class SegmentOverlayCalculator : ISegmentOverlayCalculator
    {
        private const float _baseRadius = 20f;
        private const float _minRadiusScale = 0.5f;

        
        



        public float CalculateNormalizedProgress(float animationProgress)
        {
            if (animationProgress < 0.5f)
                return 0f;

            return (animationProgress - 0.5f) / 0.5f;
        }


        public float CalculateInvertedNormalizedProgress(float animationProgress)
        {
            if(animationProgress < 0.3f)
            {
                return 1f;           
            }
            return 1f - ((animationProgress - 0.3f) / 0.3f);
        }


        private float CalculateRadiusMultiplier(float normalizedProgress)
        {
            return AnimatedColumnSettings.minRadiusScale + (normalizedProgress * (1.0f - AnimatedColumnSettings.minRadiusScale));
        }

        public float CalculateNormalizedRadius(AnimatedTimerColumn column)
        {
            float radiusMultiplier = CalculateRadiusMultiplier(column.NormalizedProgress);

            float initial = column.OverlayRadius * radiusMultiplier;

            float updatedInitial = (initial / 2) + initial;

            return updatedInitial;

        }



    



        public float CalculateSegmentAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float elapsedSinceLastAnimation = (float)(elapsed - column.LastAnimationStartTime).TotalMilliseconds;

            float animatioDurationFloat = (float)AnimatedColumnSettings.AnimationDuration.TotalMilliseconds;


            return (elapsedSinceLastAnimation / animatioDurationFloat) * 100;
        }










        public float CalculateAnimationProgress(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }


        public float CalculateCircleAnimationRadius(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float circleAnimationProgress = column.GetCircleHighlightAnimationProgress(elapsed);
            return Single.Lerp(AnimatedColumnSettings.MaxRadius, AnimatedColumnSettings.MinRadius, circleAnimationProgress);
        }

        public float CalculateCircleAnimationOpacity(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            float circleAnimationProgress = column.GetCircleHighlightAnimationProgress(elapsed);
            return Single.Lerp(1.0f, 0.0f, circleAnimationProgress);
        }





    }
}
