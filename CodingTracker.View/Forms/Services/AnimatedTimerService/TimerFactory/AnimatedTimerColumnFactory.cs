using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;


namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
{
    public interface IAnimatedTimerColumnFactory
    {
        AnimatedTimerColumn CreateColumnWithSegments(int[] segmentNumbers, SKPoint location, ColumnUnitType timeUnit);
    }




    public class AnimatedTimerColumnFactory : IAnimatedTimerColumnFactory
    {
        public AnimatedTimerColumn CreateColumnWithSegments(int[] segmentNumbers, SKPoint location, ColumnUnitType columnType)
        {
     
            var segments = new List<AnimatedTimerSegment>();
            for(int newSegment = 0; newSegment < segmentNumbers.Length; newSegment++)
            {
                float yPosition = location.Y + (newSegment * AnimatedColumnSettings.SegmentHeight);
                SKPoint segmentLocation = new SKPoint(location.X, yPosition);

                segments.Add(new AnimatedTimerSegment(segmentNumbers[newSegment], segmentLocation));

      
            }
            AnimatedTimerColumn newColumn = new AnimatedTimerColumn(segments, location, columnType);
            newColumn.TimerSegments = segments;
            return newColumn;
        }
    }
}

