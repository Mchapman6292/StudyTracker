using CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts;
using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
{
    namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerFactory
    {
        public interface IAnimatedTimerColumnFactory
        {
            AnimatedTimerColumn CreateColumn(List<int> numbers, SKPoint location);
        }

        public class AnimatedTimerColumnFactory : IAnimatedTimerColumnFactory
        {
            public AnimatedTimerColumn CreateColumn(List<int> numbers, SKPoint location)
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
}
