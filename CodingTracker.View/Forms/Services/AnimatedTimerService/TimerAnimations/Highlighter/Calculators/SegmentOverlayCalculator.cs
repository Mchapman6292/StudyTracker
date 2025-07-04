using CodingTracker.Common.Utilities;
using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Security.Policy;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter.Calculators
{
    public interface ISegmentOverlayCalculator
    {
        float CalculateNormalizedProgress(float animationProgress);
        float CalculateNormalizedRadius(AnimatedTimerColumn column);
        float TESTCalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);

        float CalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed);

    }

    public class SegmentOverlayCalculator : ISegmentOverlayCalculator
    {
        private const float _baseRadius = 20f;
        private const float _minRadiusScale = 0.5f;
        





        public float CalculateAnimationProgress(AnimatedTimerColumn column, TimeSpan elapsed)
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
