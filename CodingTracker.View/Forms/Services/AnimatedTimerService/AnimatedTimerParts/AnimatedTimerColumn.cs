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

        public List<AnimatedTimerSegment> timerSegments;
        public int SegmentCount => timerSegments.Count;

        public float width = TimerMeasurements.ColumnWidth;
        public float ColumnHeight => SegmentCount * TimerMeasurements.SegmentHeight;




        public SKPoint TimerLocation;
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;




        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, SKPoint timerLocation)
        {
            this.timerSegments = timerSegments;
            this.TimerLocation = timerLocation;
        }


        public static AnimatedTimerColumn CreateTimerColumn(List<int> numbers, SKPoint location)
        {
            var segments = new List<AnimatedTimerSegment>();
            foreach (int number in numbers)
            {
                segments.Add(new AnimatedTimerSegment(number));
            }
            return new AnimatedTimerColumn(segments, location);
        }




    }
}
