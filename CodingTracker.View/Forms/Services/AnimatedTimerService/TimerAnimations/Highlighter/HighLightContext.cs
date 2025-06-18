using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public class HighlightContext
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float ScrollOffset { get; set; }
        public AnimatedTimerColumn Column { get; set; }
    }

}
