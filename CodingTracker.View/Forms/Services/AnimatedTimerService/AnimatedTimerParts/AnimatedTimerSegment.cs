using SkiaSharp;
using System.Drawing;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts
{
    public class AnimatedTimerSegment
    {
        public int Value { get; }
        public float SegmentWidth { get; } = AnimatedColumnSettings.SegmentWidth;
        public float SegmentHeight { get; } = AnimatedColumnSettings.SegmentHeight;

        public SKColor BackgroundColour { get; } = AnimatedColumnSettings.SegmentColor;

        public SKColor TextColour { get; set; }

        public float TextSize = AnimatedColumnSettings.TextSize;
        public bool IsCurrent { get; set; }

        public SKPoint Location { get; private set; }

        public SKPoint LocationCenterPoint { get; private set; }    


        public AnimatedTimerSegment(int value, SKPoint location)
        {
            Value = value;
            Location = location;
            LocationCenterPoint = new SKPoint(Location.X + (SegmentWidth / 2), Location.Y + (SegmentHeight / 2));
        }



        public void UpdatePosition(float x, float y)
        {
            Location = new SKPoint(x, y);
        }

        public void UpdateLocationCentrePoint()
        {
            LocationCenterPoint = new SKPoint(Location.X + (SegmentWidth / 2), Location.Y + (SegmentHeight / 2));
        }



    }
}

