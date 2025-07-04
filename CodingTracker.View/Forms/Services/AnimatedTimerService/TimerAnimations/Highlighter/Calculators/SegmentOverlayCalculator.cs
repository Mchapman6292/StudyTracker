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
        float TESTCalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);

        float CalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);

    }

    public class SegmentOverlayCalculator : ISegmentOverlayCalculator
    {
        private const float _baseRadius = 20f;
        private const float _minRadiusScale = 0.5f;
        



        public float CalculateAnimationProgress(TimeSpan elapsed)
        {
            return (float)(elapsed.TotalSeconds % 1.0);
        }


        public float CalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
        {
            ColumnUnitType columnType = column.ColumnType;

            switch (columnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                    return (float)(elapsed.TotalSeconds % 1.0);
                case ColumnUnitType.SecondsLeadingDigit:
                    return(float)((elapsed.TotalSeconds / 10) % 1);
                case ColumnUnitType.MinutesSingleDigits:
                    return(float)((elapsed.Minutes / 60) % 1);


                case ColumnUnitType.MinutesLeadingDigits:
                    return elapsed.Minutes / 10;

                case ColumnUnitType.HoursSinglesDigits:
                    return elapsed.Hours % 10;
                case ColumnUnitType.HoursLeadingDigits:
                    return elapsed.Hours / 10;


                default: return 0.0f;


            }
        }

        public float TESTCalculateAnimationProgress(AnimatedTimerColumn column,TimeSpan elapsed)
        { 

            TimeSpan interval = AnimatedColumnSettings.UnitTypesToAnimationDurations[column.ColumnType];

       
            double cycleProgress = elapsed.TotalSeconds % interval.TotalSeconds;
            return (float)(cycleProgress / interval.TotalSeconds);
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









    }
}
