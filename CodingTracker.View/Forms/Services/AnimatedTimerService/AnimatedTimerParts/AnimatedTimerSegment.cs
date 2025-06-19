using SkiaSharp;
using System.Drawing;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts
{
    public class AnimatedTimerSegment
    {
        public int Value { get; }
        public float SegmentWidth { get; } = AnimatedColumnSettings.SegmentWidth;
        public float SegmentHeight { get; } = AnimatedColumnSettings.SegmentHeight;

        public SKColor SegmentColor { get; } = SKColors.White; 
        public float TextSize  => SegmentHeight * 0.8f;

        public bool IsCurrent { get; set; }


        public AnimatedTimerSegment(int value)
        {
            Value = value;
        }



 
    }
}

