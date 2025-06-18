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

        public float width = AnimatedColumnSettings.ColumnWidth;
        public float ColumnHeight => SegmentCount * AnimatedColumnSettings.SegmentHeight;

        public SKPoint TimerLocation;
        public bool IsVisible { get; set; } = true;
        public bool IsEnabled { get; set; } = true;



        public bool IsAnimating { get; private set; }
        public float AnimationSpeed { get; } = AnimatedColumnSettings.AnimationSpeed;
        public float ScrollOffset { get; set; }
        public int CurrentValue { get; private set; }
        public int TargetValue { get; private set; }




        public AnimatedTimerColumn(List<AnimatedTimerSegment> timerSegments, SKPoint timerLocation)
        {
            this.timerSegments = timerSegments;
            this.TimerLocation = timerLocation;
        }


        public void SetTargetValue(int newTarget)
        {
            TargetValue = newTarget;
        }


        public void UpdateCurrentValue()
        {
            CurrentValue = TargetValue;
        }

        public void StartScrollToValue(int newValue)
        {
            if (newValue != CurrentValue && !IsAnimating)
            {
                TargetValue = newValue;
                IsAnimating = true;
            }
        }

        public void UpdateAnimation()
        {
            if (!IsAnimating) return;


            ScrollOffset += AnimationSpeed;

            if (ScrollOffset >= AnimatedColumnSettings.SegmentHeight)
            {
                ScrollOffset = 0;
                CurrentValue = TargetValue;
                IsAnimating = false;
            }
        }

        public float GetAdjustedY()
        {
            return TimerLocation.Y - ScrollOffset;
        }

        public void Reset()
        {
            ScrollOffset = 0;
            CurrentValue = 0;
            TargetValue = 0;
            IsAnimating = false;
        }



        public int GetCurrentSegmentIndex()
        {
            return timerSegments.FindIndex(s => s.SegmentNumber == CurrentValue);
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
