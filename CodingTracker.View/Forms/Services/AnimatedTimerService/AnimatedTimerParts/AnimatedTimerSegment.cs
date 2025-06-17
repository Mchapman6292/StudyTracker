using SkiaSharp;
using System.Drawing;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts
{
    public class AnimatedTimerSegment
    {
        public int SegmentNumber { get; }
        public float SegmentWidth { get; } = TimerMeasurements.SegmentWidth;
        public float SegmentHeight { get; } = TimerMeasurements.SegmentHeight;

        public SKColor SegmentColor { get; } = SKColors.White; 
        public float TextSize  => SegmentHeight * 0.8f;





        public AnimatedTimerSegment(int segmentNumber)
        {
            this.SegmentNumber = segmentNumber;
        }


 
    }
}

