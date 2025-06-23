using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using SkiaSharp;
using System.Drawing;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts
{
    public class AnimatedTimerColumn
    {
        private SKRect _rectDrawing;

        private Color GradientColorOne = Color.FromArgb(255, 81, 195);

        private Color GradientColorTwo = Color.Turquoise;

        public List<AnimatedTimerSegment> TimerSegments;

        public int SegmentCount;

        public float width = AnimatedColumnSettings.ColumnWidth;
        public float ColumnHeight => SegmentCount * AnimatedColumnSettings.SegmentHeight;

        public SKPoint Location { get; set; }
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;

        public float TextSize = AnimatedColumnSettings.TextSize;

        
        //TODO review if needed here or in calculation class. 
        public float ScrollOffset { get; set; }

        public int CurrentValue { get; set; }
        public int PreviousValue { get; set; } = -1;
        public TimeSpan AnimationInterval { get; set; }
        public TimeSpan LastAnimationStartTime {  get; set; }
        public bool IsAnimating { get; set; }



        public const int CircleYPosition = 300;

        public ColumnUnitType ColumnType { get; set; }
        public bool EnableHighlight { get; set; } = true;

  






        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, SKPoint location, ColumnUnitType columnType)
        {
            TimerSegments = timerSegments;
            ColumnType = columnType;
            Location = location;
            SegmentCount = timerSegments.Count;
            AnimationInterval = AnimatedColumnSettings.UnitTypesToAnimationDurations[columnType];
            
        }



        private int GetMaxValueForColumnType()
        {
            switch (ColumnType)
            {
                case ColumnUnitType.SecondsSingleDigits:
                case ColumnUnitType.MinutesSingleDigits:
                case ColumnUnitType.HoursSinglesDigits:
                    return 9;
                case ColumnUnitType.SecondsLeadingDigit:
                case ColumnUnitType.MinutesLeadingDigits:
                    return 5;
                case ColumnUnitType.HoursLeadingDigits:
                    return 2;
                default:
                    return 9;
            }
        }

        public AnimatedTimerSegment GetSegmentAtPosition(float y)
        {
            float relativeY = y - Location.Y + ScrollOffset;
            int segmentIndex = (int)(relativeY / AnimatedColumnSettings.SegmentHeight);

            if (segmentIndex >= 0 && segmentIndex < TimerSegments.Count)
            {
                return TimerSegments[segmentIndex];
            }

            return null;
        }

        public float GetDistanceFromHighlight(float segmentY, float highlightY)
        {
            return Math.Abs(segmentY - highlightY);
        }


        public int GetCurrentSegmentIndex()
        {
            return TimerSegments.FindIndex(s => s.Value == CurrentValue);
        }



        public void StartAnimation(TimeSpan currentTime)
        {
            LastAnimationStartTime = currentTime;
            IsAnimating = true;
            PreviousValue = CurrentValue;
        }

        public void UpdateIsAnimating(TimeSpan currentTime)
        {
            if (IsAnimating)
            {
                var elapsed = currentTime - LastAnimationStartTime;
                if (elapsed >= AnimatedColumnSettings.AnimationDuration)
                {
                    IsAnimating = false;
                }
            }
        }






        public bool GetAnimationProgress(TimeSpan currentTime)
        {
            return currentTime - LastAnimationStartTime < AnimatedColumnSettings.AnimationDuration;
        }




    }
}
