using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
{
    namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
    {
        public interface IAnimatedTimerColumnFactory
        {
            AnimatedTimerColumn CreateColumnWithSegments(int[] segmentNumbers, SKPoint location, ColumnUnitType timeUnit);
        }

        public class AnimatedTimerColumnFactory : IAnimatedTimerColumnFactory
        {
            public AnimatedTimerColumn CreateColumnWithSegments(int[] segmentNumbers, SKPoint location, ColumnUnitType timeUnit)
            {
                int segCount = 0;

                var segments = new List<AnimatedTimerSegment>();
                for(int i = 0; i < segmentNumbers.Length; i++)
                {
                    segments.Add(new AnimatedTimerSegment(segmentNumbers[i]));
                    segCount++;
                }
                AnimatedTimerColumn newColumn = new AnimatedTimerColumn(segments, timeUnit, location);
                newColumn.TimerSegments = segments;
                return newColumn;
            }
        }
    }
}
