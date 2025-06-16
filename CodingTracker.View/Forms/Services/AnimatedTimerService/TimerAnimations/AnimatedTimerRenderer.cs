using CodingTracker.View.Forms.Services.AnimatedTimerService.TimerParts;
using SkiaSharp;
using System.Windows.Media;


namespace CodingTracker.View.Forms.Services.AnimatedTimerService.TimerAnimations
{
    public interface IAnimatedTimerRenderer
    {
        void DrawAnimatedTimerColumn(SKCanvas timerCanvas, AnimatedTimerColumn timerColumn, float width, float height);
    }

    public class AnimatedTimerRenderer : IAnimatedTimerRenderer
    {

        public void DrawAnimatedTimerColumn(SKCanvas timerCanvas, AnimatedTimerColumn timerColumn, float width, float height)
        {
            var rect = SKRect.Create(width, height);

            using(var paint = new SKPaint())
            {
                paint.Color = SKColors.White; 
                timerCanvas.DrawRect(rect, paint);
            }

            
        }


        

        public void DrawAnimatedTimerSegment(SKCanvas timerCanvas, AnimatedTimerColumn timerColumn, int segmentIndex)
        {

        }
    }
}
