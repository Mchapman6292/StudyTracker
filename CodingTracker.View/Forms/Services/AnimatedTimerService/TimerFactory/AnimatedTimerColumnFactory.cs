using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
{
    namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
    {
        public interface IAnimatedTimerColumnFactory
        {
            AnimatedTimerColumn CreateColumn(int[] numbers, SKPoint location);
        }

        public class AnimatedTimerColumnFactory : IAnimatedTimerColumnFactory
        {
            public AnimatedTimerColumn CreateColumn(int[] segmentNumbers, SKPoint location)
            {
                int segCount = 0;

                var segments = new List<AnimatedTimerSegment>();
                for(int segmentNumber = 0; segmentNumber < segmentNumbers.Length; segmentNumber++)
                {
                    segments.Add(new AnimatedTimerSegment(segmentNumber));
                    segCount++;
                }
                return new AnimatedTimerColumn(segments);
            }
        }
    }
}
