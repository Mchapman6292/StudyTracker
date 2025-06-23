using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public  interface ITimerHighlight
    {
        public void Draw(SKCanvas canvas, AnimatedTimerColumn column);
        bool IsEnabled { get; set; }
    }

}
