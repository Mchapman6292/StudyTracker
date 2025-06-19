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

        public SKPoint Location;
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;

        public float TextSize = AnimatedColumnSettings.TextSize;


        public float ScrollOffset { get; set; }
        public int CurrentValue { get; set; }

        public const int CircleYPosition = 300;

        public ColumnUnitType ColumnType { get; set; }
        public bool EnableHighlight { get; set; } = true;







        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, ColumnUnitType timeUnit, SKPoint location)
        {
            TimerSegments = timerSegments;
            ColumnType = timeUnit;
            Location = location;
            SegmentCount = timerSegments.Count;
            
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





    }
}
