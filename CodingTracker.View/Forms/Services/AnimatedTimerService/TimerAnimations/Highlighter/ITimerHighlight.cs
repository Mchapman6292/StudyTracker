using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;

namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations.Highlighter
{
    public  interface ITimerHighlight
    {
        void Draw(SKCanvas canvas, HighlightContext context);
        bool IsEnabled { get; set; }
    }

}
