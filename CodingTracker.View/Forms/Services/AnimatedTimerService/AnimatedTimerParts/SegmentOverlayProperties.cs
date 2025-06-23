using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.AnimatedTimerParts
{
    public class SegmentOverlayProperties
    {
        // Not needed if using SkRect etc. 
        public AnimatedTimerSegment TargetSegment { get; set; }

        public TimeSpan AnimationDuration;

        public bool IsAnimating { get; set; }

        public float CurrentOpacity;

        public float CurrentScale;










    }
}
