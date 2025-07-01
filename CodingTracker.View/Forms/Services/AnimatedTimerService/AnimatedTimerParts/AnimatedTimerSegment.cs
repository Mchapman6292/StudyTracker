using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
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

        public SKPoint Location { get; set; }

        public SKPoint LocationCenterPoint { get; private set; }    


        public AnimatedTimerSegment(int value, SKPoint location)
        {
            Value = value;
            Location = location;
            LocationCenterPoint = CalculateCenterPoint();
        }



        public void UpdatePosition(float x, float y)
        {
            Location = new SKPoint(x, y);
            UpdateLocationCentrePoint();
        }

        public void UpdateLocationCentrePoint()
        {
            LocationCenterPoint = new SKPoint(Location.X + (SegmentWidth / 2), Location.Y + (SegmentHeight / 2));
        }


        public SKPoint CalculateCenterPoint()
        {
           return new SKPoint(Location.X + (SegmentWidth / 2), Location.Y + (SegmentHeight / 2));


            /*
             * 
             * Old setting delete if working. 
                           float centerX = x + (timerSegment.SegmentWidth / 2f);
             float centerY = y + (timerSegment.SegmentHeight / 2f) + (timerSegment.TextSize / 3f);
            */


            
        }


        public void UpdateSegmentPositions(AnimatedTimerColumn column)
        {
            float startY = column.Location.Y - column.ScrollOffset;

            for (int i = 0; i < column.TimerSegments.Count; i++)
            {
                var segment = column.TimerSegments[i];
                float newY = startY + (i * AnimatedColumnSettings.SegmentHeight);

                segment.UpdatePosition(column.Location.X, newY);
            }
        }

    }
}

