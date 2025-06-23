using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public class SegmentOverlayContext
    {
        public AnimatedTimerColumn Column { get; set; }

        public TimeSpan AnimationDuration;

        public float CurrentOpacity;

        public float CurrentScale;

        public float AnimationProgress;

    }

}
